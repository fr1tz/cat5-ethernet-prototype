//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------

// Variables used by client scripts & code.  The ones marked with (c)
// are accessed from code.  Variables preceeded by Pref:: are client
// preferences and stored automatically in the ~/client/prefs.cs file
// in between sessions.
//
//	 (c) Client::MissionEnvironmentFile      Mission .env file name
//	 ( ) Client::Password                    Password for server join

//	 (?) Pref::Player::CurrentFOV
//	 (?) Pref::Player::DefaultFov
//	 ( ) Pref::Input::KeyboardTurnSpeed

//	 (c) pref::Master[n]					  List of master servers
//	 (c) pref::Net::RegionMask	  
//	 (c) pref::Client::ServerFavoriteCount
//	 (c) pref::Client::ServerFavorite[FavoriteCount]
//	 .. Many more prefs... need to finish this off

// Moves, not finished with this either...
//	 (c) firstPerson
//	 $mv*Action...

//-----------------------------------------------------------------------------
// These are variables used to control the shell scripts and
// can be overriden by mods:

//-----------------------------------------------------------------------------

function clientLoadMenu()
{
   disconnect();
	createServer("Menu", "game/arenas/cat5-ethernet/menu.mis");
   %conn = new GameConnection(ServerConnection);
   RootGroup.add(ServerConnection);
	%conn.setConnectArgs($GameNameString, $GameVersionString, $Pref::Player::Name);
	%conn.setJoinPassword($Client::Password);
   %conn.connectLocal();
   onConnectionInitiated();
}

function initClient()
{
	echo("\n--------- Initializing Ethernet: Client ---------");
	
	// Make sure this variable reflects the correct state.
	$Server::Dedicated = false;

	// Game information used to query the master server
	$Client::GameTypeQuery = $GameNameString;
	$Client::MissionTypeQuery = "Any";

	// The common module provides basic client/server functionality
	initBaseClient();
	initBaseServer();
	
	// Client-side Audio Descriptions must be loaded before 
	// the GUI profiles because of the GUI sound effects.
	exec("./scripts/audioDescriptions.cs");	
 
	// Our GUI profiles need to be created before initCanvas is called
	// and creates default profiles for essential ones that don't exist.
   exec("./ui/shell/profiles.cs");
	exec("./ui/hud/profiles.cs");

	// InitCanvas starts up the graphics system.
	// The canvas needs to be constructed before the gui scripts are
	// run because many of the controls assume the canvas exists at
	// load time.
	initCanvas("Cat5 (" @ $GameVersionString @ ")");

	// execute the UI scripts
	exec("./ui/init.cs");

	// execute the game client scripts
	exec("./scripts/cmds.cs");
	exec("./scripts/client.cs");
	exec("./scripts/missionDownload.cs");
	exec("./scripts/serverConnection.cs");
	exec("./scripts/game.cs");
	exec("./scripts/misc.cs");
 	exec("./scripts/mumble.cs");
 	exec("./scripts/remap.list.cs");
 	exec("./scripts/remap.functions.cs");

	// default key bindings
	exec("./scripts/actionmap.global.cs");
	exec("./scripts/actionmap.standard.cs");
	exec("./scripts/actionmap.aircraft.cs");
	exec("./scripts/actionmap.cmdrscreen.cs");

	// user-defined key bindings...
	exec("./config.cs");

	// Really shouldn't be starting the networking unless we are
	// going to connect to a remote server, or host a multi-player
	// game.
	setNetPort(0);

	// Copy saved script prefs into C++ code.
	setShadowDetailLevel( $Pref::shadows );
	setDefaultFov( $Pref::Player::DefaultFov );
	setZoomSpeed( $Pref::Player::ZoomSpeed );

   // Set default cursor.
   Canvas.setCursor(DefaultCursor);

   // Select splash screen bitmap according to resolution aspect ratio.
   %res = getRes();
   if(getWord(%res,0) / getWord(%res, 1) > 1.5)
      ShellTorqueSplash.setBitmap("common/ui/torquesplash_16x9");
   else
      ShellTorqueSplash.setBitmap("common/ui/torquesplash_4x3");

   // Mute sound until the splash screen is done.
   alxListenerf(AL_GAIN_LINEAR, 0);

   if($JoinGameAddress !$= "")
      connect($JoinGameAddress, "", $Pref::Player::Name);
   else
      clientLoadMenu();
}

//-----------------------------------------------------------------------------


