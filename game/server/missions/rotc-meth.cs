//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//*******************************************************
// Mission init script for ROTC: mEthMatch missions
//*******************************************************

exec("./rotc-types.cs");

$Server::MissionFile = strreplace($Server::MissionFile, 
	"/rotc-methmatch/", "/cat5/");

exec("./rotc-eth.cs");

$Server::MissionFile = strreplace($Server::MissionFile, 
	"/cat5/", "/rotc-methmatch/");

$Game::GameType = $Game::mEthMatch;
$Game::GameTypeString = "ROTC: mEthMatch";





		
