//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

// called by script
function sAuthStart()
{
   if(!isObject(sAimsAuthConn))
   {
      new TCPObject(sAimsAuthConn);
      sAimsAuthConn.reset();
   }
}

// called by script
function sAuthStop()
{
   if(isObject(sAimsAuthConn))
      sAimsAuthConn.delete();
}

// called by script
function sAimsAuthConn::reset(%this)
{
   %this.ready = false;

	%count = ClientGroup.getCount();
	for(%idx = 0; %idx < %count; %idx++)
   {
		%client = ClientGroup.getObject(%idx);
      if(%client.authStage == 3)
         %client.authStage = 2;
   }

   %this.connect($Pref::AIMS::Server[0] @ ":28003");
}

// called by script
function sAimsAuthConn::processAuth(%this)
{
   if(!%this.ready)
      return;

	%count = ClientGroup.getCount();
	for(%idx = 0; %idx < %count; %idx++)
   {
		%client = ClientGroup.getObject(%idx);
      if(%client.authStage == 2)
      {
         %this.send(
            %client SPC
            %client.authPlayerId SPC
            %client.authHash SPC
            $Pref::Server::Name SPC
            %client.authServerName SPC
            %client.authClientTime SPC
            %client.authServerTime SPC
            %client.authClientRand SPC
            %client.authServerRand @ "\n"
         );
         %client.authStage = 3;
      }
   }
}

// called by engine
function sAimsAuthConn::onLine(%this, %line)
{
	//error("sAimsAuthConn::onLine()");

   if(%line $= "ready")
   {
      %this.ready = true;
      %this.processAuth();
      return;
   }

   %client = getWord(%line, 0);
   if(!isObject(%client) || %client.authStage != 3)
   {
      error("sAimsAuthConn::onLine(): received invalid line:" SPC %line);
      return;
   }

   %msg = getWords(%line, 1);
   if(getWord(%msg, 0) $= "player:")
   {
      %client.authPlayerName = getWords(%msg, 1);
      %oldname = %client.name;
      %client.nameBase = %client.authPlayerName;
      %client.name = addTaggedString(%client.authPlayerName);
      messageAll(
         'MsgClientAuth',
         '\c2%1 has been authenticated as %2 from the AIMS player database (aims.wasted.ch)',
         %oldname,
         %client.name
      );
      if(isObject(%client.player))
         %client.player.getDataBlock().updateShapeName(%client.player);
   }
   else
   {
      messageClient(%client,
         'MsgClientAuth',
         '\c2Failed to authenticate you as %1: %2',
         %client.authPlayerId,
         %msg
      );
   }
   %client.authStage = 0;
}

// called by engine
function sAimsAuthConn::onConnected(%this)
{
   //error("sAimsAuthConn::onConnected()");
	%this.send("playerdb/auth.1\n");
}

// called by engine
function sAimsAuthConn::onConnectFailed(%this)
{
	error("sAimsAuthConn: Connection to" SPC $Pref::AIMS::Server[0] @ ":28003 failed!");
   %this.onDisconnect();
}

// called by engine
function sAimsAuthConn::onDisconnect(%this)
{
   error("sAimsAuthConn::onDisconnect()");
   error("sAimsAuthConn: Trying to reconnect in 5 seconds");
   %this.schedule(5000, "reset");
}

