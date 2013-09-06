//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Misc. server commands avialable to clients
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Authentication

function serverCmdAuthResponse(%client, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6)
{
   //error("serverCmdAuthResponse():" SPC %client SPC
   //   %arg1 SPC %arg2 SPC %arg3 SPC %arg4 SPC %arg5 SPC %arg6);

   if(%client.authStage != 1)
      return;

   %alg = %arg1;
   if(%alg !$= "aims/playerdb/auth.1")
   {
      %client.authStage = 0;
      return;
   }

   %client.authHash = %arg2;
   %client.authPlayerId = %arg3;
   %client.authServerName = %arg4;
   %client.authClientTime = %arg5;
   %client.authClientRand = %arg6;
   %client.authStage = 2;

   sAimsAuthConn.processAuth();
}

//-----------------------------------------------------------------------------

function serverCmdSAD( %client, %password )
{
	if( %password !$= "" && %password $= $Pref::Server::AdminPassword)
	{
		%client.isAdmin = true;
		%client.isSuperAdmin = true;
		%name = getTaggedString( %client.name );
		MessageAll( 'MsgAdminForce', "\c2" @ %name @ " has become Admin by force.", %client );	
	}
}

function serverCmdSADSetPassword(%client, %password)
{
	if(%client.isSuperAdmin)
		$Pref::Server::AdminPassword = %password;
}

//----------------------------------------------------------------------------
// Server chat message handlers

function serverCmdTeamMessageSent(%client, %text)
{
	if(strlen(%text) >= $Pref::Server::MaxChatLen)
		%text = getSubStr(%text, 0, $Pref::Server::MaxChatLen);
	chatMessageTeam(%client, %client.team, '\c4[team] \c3%1: %2', %client.name, %text);
}

function serverCmdMessageSent(%client, %text)
{
	if(strlen(%text) >= $Pref::Server::MaxChatLen)
		%text = getSubStr(%text, 0, $Pref::Server::MaxChatLen);
	chatMessageAll(%client, '\c4%1: %2', %client.name, %text);
}

//----------------------------------------------------------------------------
// Server commands wrapper

function serverCmdSimpleCommand(%client, %cmd, %arg)
{
	call("serverCmd" @ %cmd, %client, %arg);
}

//-----------------------------------------------------------------------------
// Cookies

function serverCmdCookie(%client, %name, %value)
{
   //error("serverCmdCookie:" SPC %name SPC %value);
   if(!isObject(%client.cookies)) return;
   arrayChangeElement(%client.cookies, %name, %value);
}

//-----------------------------------------------------------------------------
// Recordings

function serverCmdRecordingDemo(%client, %isRecording)
{
   %client.onRecordingDemo(%isRecording);
}

