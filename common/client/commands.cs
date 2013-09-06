//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Authentication

function clientCmdAuthChallenge(%alg, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6)
{
   //error("clientCmdAuthChallenge():" SPC %alg);

   if(%alg !$= "aims/playerdb/auth.1")
      return;

   if(!$Pref::AIMS::Authenticate)
      return;

   if($Pref::AIMS::PlayerName $= "")
      return;

   if($Pref::AIMS::Password $= "")
      return;

   %servername = %arg1;
   %servertime = %arg2;
   %serverrand = %arg3;

   %clienttime = getTime();
   %clientrand = getRandom(999999);

   %hash = sha256(
      %servername SPC
      ServerConnection.getAddress() SPC
      %clienttime SPC
      %servertime SPC
      %clientrand SPC
      %serverrand SPC
      $Pref::AIMS::Password
   );

   commandToServer('AuthResponse', "aims/playerdb/auth.1",
      %hash,
      $Pref::AIMS::PlayerName,
      ServerConnection.getAddress(),
      %clienttime,
      %clientrand
   );
}

//-----------------------------------------------------------------------------
// Cookies

function clientCmdCookie(%name, %value)
{
	echo("Got cookie!");
	echo("Name:" SPC %name);
	echo("Value:" SPC %value);

   // Make sure the server can't interject code into our prefs.cs file!!!
   %legal = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_";
   %len = strlen(%name);
   for(%i = 0; %i < %len; %i++)
   {
      %c = getSubStr(%name, %i, 1);

      if(strpos(%legal, %c) < 0)
      {
         error("Illegal cookie name! Contains" SPC %c);
         return;
      }
   }

	$Pref::Cookie_[%name] = %value;
}

function clientCmdCookieRequest(%name)
{
	%value = $Pref::Cookie_[%name];
   if(%value !$= "")
   	commandToServer('Cookie', %name, %value);
}
