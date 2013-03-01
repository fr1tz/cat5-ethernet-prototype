//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//************************************************
// Mission init script for Cat5 Ethernet missions
//************************************************

exec("./cat5-types.cs");

$Game::GameType = $Game::Ethernet;
$Game::GameTypeString = "Cat5 Ethernet";

$Server::MissionDirectory = strreplace($Server::MissionFile, ".mis", "") @ "/";
$Server::MissionEnvironmentFile = $Server::MissionDirectory @ "mission.env";

function executeMissionScript()
{
   // Clear mission callback functions first.
   exec("game/server/missions/missioncallbacks.cs");
   exec($Server::MissionDirectory @ "mission.cs"); 
}

function executeEnvironmentScript()
{
	exec($Server::MissionEnvironmentFile @ ".cs"); 
}

function executeGameScripts()
{
	exec("game/server/base/exec.cs");
	exec("game/server/eth/exec.cs");
	exec("game/server/etherforms/exec.cs");
	exec("game/server/forms/standardcat/exec.cs");
	exec("game/server/forms/pumpgunner/exec.cs");
	exec("game/server/forms/shotgunner/exec.cs");
	exec("game/server/forms/minigunner/exec.cs");
	exec("game/server/forms/specialist/exec.cs");
	exec("game/server/forms/hunter/exec.cs");
	exec("game/server/weapons/weapons.cs");
	exec("game/server/weapons/pumpgun/exec.cs");
	exec("game/server/weapons/shotgun/exec.cs");
	exec("game/server/weapons/minigun/exec.cs");
	exec("game/server/weapons/bouncegun/exec.cs");
	exec("game/server/weapons/crossbow/exec.cs");
}

function loadManual()
{
	constructManual("game/server/eth/help/index.idx");
	// hack hack hack
	if($Server::Game.superblaster)
	{
		%p = getManualPage("6.1");
		%p.name = "Superblaster";
		%p.file = "game/server/weapons/blaster3/blaster3.rml";
		updateManualPage(%p);
	}
}

function loadHints()
{
   constructHints("game/server/eth/help/hints.rml");
}

function initMission()
{
	if(isObject($Server::Game))
		$Server::Game.delete();
	$Server::Game = new ScriptObject();
	$Server::Game.mutators = "";
	$Server::Game.alwaystag = -1;
	$Server::Game.nevertag  = 0;
	$Server::Game.temptag   = 1;
	$Server::Game.tagMode = $Server::Game.alwaystag;
	$Server::Game.slowpokemod = 1.0;
	$Server::Game.friendlyfire = false;
	%recognized = "";
	for(%i = 0; %i < getWordCount($Pref::Server::Mutators); %i++)
	{
		%mutator = getWord($Pref::Server::Mutators, %i);
		if(%mutator $= "ff")
		{
		   %recognized = %mutator SPC %recognized;
			$Server::Game.friendlyfire = true;
			$Server::Game.mutators = %mutator SPC $Server::Game.mutators;
		}
	}
	%recognized = trim(%recognized);
	if(getWordCount(%recognized) > 0)
	{
		// We have valid mutators...
		$Server::Game.mutators = trim($Server::Game.mutators);
		if(getWordCount(%recognized) == 1)
			%str = %recognized;
		else
			%str = "VARIANT";
		$Server::MissionType = $Server::MissionType SPC "\c4["@%str@"]\co";
	}
	executeGameScripts();
	executeMissionScript();
	executeEnvironmentScript();
	loadManual();
	loadHints();
}

function onMissionLoaded()
{
	// Called by loadMission() once the mission is finished loading.
	startGame();
}

function onMissionEnded()
{
	// Called by endMission(), right before the mission is destroyed

	if(isObject($Server::Game))
		$Server::Game.delete();

	// Normally the game should be ended first before the next
	// mission is loaded, this is here in case loadMission has been
	// called directly.  The mission will be ended if the server
	// is destroyed, so we only need to cleanup here.
	cancel($Game::Schedule);
	$Game::Running = false;
	$Game::Cycling = false;
}



		
