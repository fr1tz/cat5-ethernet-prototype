//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - gameconnection.cs
// Sstuff for the objects that represent client connections
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------

function GameConnection::control(%this, %shapebase)
{
	%oldcontrol = %this.getControlObject();
	%newcontrol = %shapebase;
	
	if(%newcontrol.isTransformed)
		%newControl = %newcontrol.transformObj;

	%currTagged = %oldcontrol.getCurrTagged();
	if(%currTagged)
		%newcontrol.setCurrTagged(%currTagged);
	else
	{
		%newcontrol.setCurrTagged(0);
		%newcontrol.setCurrTaggedPos(%oldcontrol.getCurrTaggedPos());
	}
	
	%this.setControlObject(%newcontrol);
}

//------------------------------------------------------------------------------

// *** callback function: called by script code in "common"
function GameConnection::onReadyToAskForCookies(%this)
{
	%this.requestCookie("ROTC_HudColor");
	%this.requestCookie("ROTC_HudMenuTMode");
	%this.requestCookie("ROTC_Handicap");
}

// *** callback function: called by script code in "common"
function GameConnection::onClientLoadMission(%this)
{
	%this.loadingMission = true;
	%this.updateQuickbar();
	serverCmdMainMenu(%this);
}


// *** callback function: called by script code in "common"
function GameConnection::onClientEnterGame(%this)
{
	%this.loadingMission = false;
	%this.ingame = true;

	commandToClient(%this, 'SyncClock', $Sim::Time - $Game::StartTime);

	// Handicap
	%this.setHandicap(%this.getCookie("ROTC_Handicap"));
	
	// ScriptObject used to store raw statistics...
	%this.stats                    = new ScriptObject();
	%this.stats.joinTime           = $Sim::Time;
	%this.stats.dmgDealtApplied    = new Array();
	%this.stats.dmgDealtCaused     = new Array();
	%this.stats.dmgReceivedApplied = new Array();
	%this.stats.dmgReceivedCaused  = new Array();
	%this.stats.healthLost         = new Array();
	%this.stats.healthRegained     = new Array();
	%this.stats.fired              = new Array();
	// ScriptObject used to store processed statistics...
	%this.pstats = new ScriptObject();
	
	// "simple control (tm)" info...
	%this.simpleControl = new Array();
	MissionCleanup.add(%this.simpleControl);
	
	// HUD color...
	%this.hudColor = %this.getCookie("ROTC_HudColor");

	// HUD Backgrounds...
	for(%i = 1; %i <= 3; %i++)
	{
		%this.hudBackgroundBitmap[%i] = "";
		%this.hudBackgroundColor[%i] = "";
		%this.hudBackgroundRepeat[%i] = "";
		%this.hudBackgroundAlphaDt[%i] = "";
	}

	// HUD Warnings...
	for(%i = 1; %i <= 6; %i++)
	{
		%this.hudWarningString[%i] = "";
		%this.hudWarningVisible[%i] = "";
	}
	%this.updateHudWarningsThread();

	// HUD Menus...
	%this.setHudMenuL("*", " ", 1, 0);
	%this.setHudMenuR("*", " ", 1, 0);
	%this.setHudMenuT("*", " ", 1, 0);
	
	// Initial mode for Top HUD Menu...
	%this.topHudMenu = %this.getCookie("ROTC_HudMenuTMode");
	if(%this.topHudMenu $= "")
		%this.topHudMenu = "newbiehelp";
	%this.initialTopHudMenu = %this.topHudMenu;
	%this.updateTopHudMenuThread();
 	
	//
	// setup observer camera object...
	//
	
	%this.camera = new Camera() {
		dataBlock = ObserverCamera;
	};
	MissionCleanup.add(%this.camera);
	%this.camera.scopeToClient(%this);
		
	%this.camera.setTransform(pickObserverPoint(%this));
	%this.camera.setVelocity("0 0 0");

	//
	// inventory...
	//
	%this.defaultLoadout();
	%this.updateLoadout();
	
	//
	// join observer team...
	//
	
	%this.joinTeam(0);

	// Start sky color thread.
	%this.updateSkyColor();

	%this.updateQuickbar();
	if(%this.menu $= "mainmenu")
		serverCmdMainMenu(%this);

	// Start thread to process player stats...
	%this.processPlayerStats();	
}

// *** callback function: called by script code in "common"
function GameConnection::onClientLeaveGame(%this)
{
	%this.team.numPlayers--;

	%this.clearFullControl();
	%this.clearSimpleControl();
 
	if(isObject(%this.stats))
	{
		%this.stats.dmgDealtApplied.delete();
		%this.stats.dmgDealtCaused.delete();
		%this.stats.dmgReceivedApplied.delete();
		%this.stats.dmgReceivedCaused.delete();
		%this.stats.healthLost.delete();
		%this.stats.healthRegained.delete();
		%this.stats.fired.delete();
		%this.stats.delete();
	}

	if(isObject(%this.pstats))
		%this.pstats.delete();

	if(isObject(%this.simpleControl))
		%this.simpleControl.delete();
	
	if(isObject(%this.thirdEye))
		%this.thirdEye.delete();

	if(isObject(%this.camera))
		%this.camera.delete();
		
	if(isObject(%this.player))
		%this.player.delete();
		
	%count = ClientGroup.getCount();
	for(%cl= 0; %cl < %count; %cl++)
		ClientGroup.getObject(%cl).updateQuickbar();		
}

//------------------------------------------------------------------------------

function GameConnection::defaultLoadout(%this)
{
	for(%i = 1; %i <= 9; %i++)
		this.loadout[%i] = "";

	if($Game::GameType == $Game::Ethernet)
	{
		%this.loadout[1] = $CatEquipment::Blaster;
		%this.loadout[2] = $CatEquipment::SniperRifle;
		%this.loadout[3] = $CatEquipment::Etherboard;
		%this.loadout[4] = $CatEquipment::Damper;
		%this.loadout[5] = $CatEquipment::VAMP;
		%this.loadout[6] = $CatEquipment::Anchor;
		%this.loadout[7] = $CatEquipment::Grenade;
		%this.loadout[8] = $CatEquipment::Bounce;
		%this.loadout[9] = $CatEquipment::RepelDisc;
		%this.loadout[10] = $CatEquipment::ExplosiveDisc;
	}
	else if($Game::GameType == $Game::TeamJoust)
	{
		%this.loadout[1] = $CatEquipment::Blaster;
		%this.loadout[2] = $CatEquipment::BattleRifle;
		%this.loadout[3] = $CatEquipment::GrenadeLauncher;
		%this.loadout[4] = $CatEquipment::Damper;
		%this.loadout[5] = $CatEquipment::VAMP;
		%this.loadout[6] = $CatEquipment::Stabilizer;
		%this.loadout[7] = $CatEquipment::Grenade;
		%this.loadout[8] = $CatEquipment::Permaboard;
		%this.loadout[9] = $CatEquipment::SlasherDisc;
	}
	else if($Game::GameType == $Game::TeamDragRace)
	{
		%this.loadout[1] = $CatEquipment::Blaster;
		%this.loadout[2] = $CatEquipment::BattleRifle;
		%this.loadout[3] = $CatEquipment::GrenadeLauncher;
		%this.loadout[4] = $CatEquipment::Damper;
		%this.loadout[5] = $CatEquipment::VAMP;
		%this.loadout[6] = $CatEquipment::Stabilizer;
		%this.loadout[7] = $CatEquipment::Grenade;
		%this.loadout[8] = $CatEquipment::SlasherDisc;
	}
	else if($Game::GameType == $Game::GridWars)
	{
		%this.loadout[1] = $CatEquipment::BattleRifle;
		%this.loadout[2] = $CatEquipment::SniperRifle;
		%this.loadout[3] = $CatEquipment::Etherboard;
		%this.loadout[4] = $CatEquipment::Grenade;
		%this.loadout[5] = $CatEquipment::Bounce;
		%this.loadout[6] = $CatEquipment::RepelDisc;
		%this.loadout[7] = $CatEquipment::ExplosiveDisc;
	}
	else
	{
		%this.loadout[1] = $CatEquipment::Blaster;
		%this.loadout[2] = $CatEquipment::BattleRifle;
		%this.loadout[3] = $CatEquipment::Etherboard;
		%this.loadout[4] = $CatEquipment::Damper;
		%this.loadout[5] = $CatEquipment::VAMP;
		%this.loadout[6] = $CatEquipment::Stabilizer;
		%this.loadout[7] = $CatEquipment::Grenade;
		%this.loadout[8] = $CatEquipment::SlasherDisc;
	}
}

function GameConnection::updateLoadout(%this)
{
	%this.numWeapons = 0;
	%this.hasDamper = false;
	%this.hasAnchor = false;
	%this.hasStabilizer = false;
	%this.hasSlasherDisc = false;
	%this.hasRepelDisc = false;
	%this.hasExplosiveDisc = false;
	%this.hasGrenade = false;
	%this.hasBounce = false;
	%this.hasEtherboard = false;	
	%this.hasPermaboard = false;
	%this.numVAMPs = 0;	
	%this.numRegenerators = 0;
	for(%i = 1; %i <= 9; %i++)
	{
		if(%this.loadout[%i] $= "")
			continue;

		if(%this.loadout[%i] == $CatEquipment::Damper)
		{
			%this.hasDamper = true;
		}
		else if(%this.loadout[%i] == $CatEquipment::Anchor)
		{
			%this.hasAnchor = true;
		}
		else if(%this.loadout[%i] == $CatEquipment::Stabilizer)
		{
			%this.hasStabilizer = true;
		}
		else if(%this.loadout[%i] == $CatEquipment::SlasherDisc)
		{
			%this.hasSlasherDisc = true;
		}
		else if(%this.loadout[%i] == $CatEquipment::RepelDisc)
		{
			%this.hasRepelDisc = true;
		}
		else if(%this.loadout[%i] == $CatEquipment::ExplosiveDisc)
		{
			%this.hasExplosiveDisc = true;
		}
		else if(%this.loadout[%i] == $CatEquipment::Grenade)
		{
			%this.hasGrenade = true;
		}
		else if(%this.loadout[%i] == $CatEquipment::Bounce)
		{
			%this.hasBounce = true;
		}
		else if(%this.loadout[%i] == $CatEquipment::Etherboard)
		{
			%this.hasEtherboard = true;
		}
		else if(%this.loadout[%i] == $CatEquipment::Permaboard)
		{
			%this.hasPermaboard = true;
		}
		else if(%this.loadout[%i] == $CatEquipment::VAMP)
		{
			%this.numVAMPs++;
		}
		else if(%this.loadout[%i] == $CatEquipment::Regeneration)
		{
			%this.numRegenerators++;
		}
		else if(%this.loadout[%i] < $CatEquipment::SlasherDisc)
		{
			%this.weapons[%this.numWeapons] = %this.loadout[%i];
			%this.numWeapons++;
		}
	}
}

function GameConnection::displayInventory(%this, %obj)
{
	%iconname[$CatEquipment::Blaster] = "blaster";
	%iconname[$CatEquipment::BattleRifle] = "rifle";
	%iconname[$CatEquipment::SniperRifle] = "sniper";
	%iconname[$CatEquipment::MiniGun] = "minigun";
	%iconname[$CatEquipment::RepelGun] = "grenadelauncher";
	%iconname[$CatEquipment::GrenadeLauncher] = "grenadelauncher";
	%iconname[$CatEquipment::SlasherDisc] = "slasherdisc";
	%iconname[$CatEquipment::RepelDisc] = "repeldisc";
	%iconname[$CatEquipment::ExplosiveDisc] = "explosivedisc";
	%iconname[$CatEquipment::VAMP] = "vamp";
	%iconname[$CatEquipment::Anchor] = "anchor";
	%iconname[$CatEquipment::Stabilizer] = "stabilizer";
	%iconname[$CatEquipment::Permaboard] = "permaboard";
	%iconname[$CatEquipment::Grenade] = "grenade";
	%iconname[$CatEquipment::Bounce] = "bounce";
	%iconname[$CatEquipment::Etherboard] = "etherboard";
	%iconname[$CatEquipment::Regeneration] = "regen";	

	%fixed = false;
	if($Game::GameType == $Game::mEthMatch)
		%fixed = true;

	if(%this.inventoryMode $= "showicon")
	{
		%numDiscs = %obj.numDiscs;
		%this.setHudMenuL(0, "<bitmap:share/hud/rotc/icon.disc.png><sbreak>", %numDiscs, 1);
		for(%i = 1; %i < 10; %i++)
			%this.setHudMenuL(%i, "", 1, 0);
	}
	else if(%this.inventoryMode $= "show")
	{
		for(%i = 1; %i <= 3; %i++)
			%icon[%i] = %iconname[%this.loadout[%i]];
	
		%this.setHudMenuL(0, "<font:NovaSquare:12>", 1, 1);			
		
		%this.setHudMenuL(1, "Slot #1:\n", 1, 1);
		%this.setHudMenuL(2, "<bitmap:share/hud/rotc/icon." @ %icon[1] @ ".50x15>", 1, 1);
		if(%fixed)
			%this.setHudMenuL(3, "<sbreak>(FIXED)", 1, 1);
		else
			%this.setHudMenuL(3, "<sbreak>(Press @bind35 to change)", 1, 1);
		
		%this.setHudMenuL(4, "\n\n\n\n\n\Slot #2:\n", 1, 1);
		%this.setHudMenuL(5, "<bitmap:share/hud/rotc/icon." @ %icon[2] @ ".50x15>", 1, 1);
		if(%fixed)
			%this.setHudMenuL(6, "<sbreak>(FIXED)", 1, 1);
		else
			%this.setHudMenuL(6, "<sbreak>(Press @bind36 to change)", 1, 1);
		
		%this.setHudMenuL(7, "\n\n\n\n\n\Slot #3:\n", 1, 1);
		%this.setHudMenuL(8, "<bitmap:share/hud/rotc/icon." @ %icon[3] @ ".50x15>", 1, 1);
		if(%fixed)
			%this.setHudMenuL(9, "<sbreak>(FIXED)", 1, 1);
		else
			%this.setHudMenuL(9, "<sbreak>(Press @bind37 to change)", 1, 1);
	}
	else if(%this.inventoryMode $= "select")
	{
		%item[1] = $CatEquipment::Blaster;
		%item[2] = $CatEquipment::BattleRifle;
		%item[3] = $CatEquipment::SniperRifle;
		%item[4] = $CatEquipment::Minigun;
		if($Game::GameType == $Game::Ethernet)
			%item[5] = $CatEquipment::RepelGun;
		else
			%item[5] = $CatEquipment::GrenadeLauncher;
		%item[6] = $CatEquipment::Etherboard;
		%item[7] = $CatEquipment::Regeneration;

		%itemname[1] = "Blaster";
		%itemname[2] = "Battle Rifle";
		%itemname[3] = "Sniper ROFL";
		%itemname[4] = "Minigun";
		%itemname[5] = $Game::GameType == $Game::Ethernet ? "Bubblegun" : "Gren. Launcher";
		%itemname[6] = "Etherboard";
		%itemname[7] = "Regeneration";

		%numItems = $Game::GameType == $Game::Ethernet ? 7 : 5;

		%this.setHudMenuL(0, "<font:NovaSquare:12>Select slot #" @ %obj.inventoryMode[1] @ ":\n\n", 1, 1);
		for(%i = 1; %i <= %numItems; %i++)
			%this.setHudMenuL(%i, "@bind" @ (%i < 6 ? 34 : 41) + %i @ ": " @ %itemname[%i]  @  "\n" @
				"   <bitmap:share/hud/rotc/icon." @ %iconname[%item[%i]] @ ".50x15>" @ "<sbreak>", 1, 1);
		for(%i = %numItems + 1; %i <= 9; %i++)
			%this.setHudMenuL(%i, "", 1, 0);	
	}

	for(%i = 4; %i < 10; %i++)
	{
		%this.setHudMenuR(0, "<just:right>", 1, 1);
		%icon = %iconname[%this.loadout[%i]];
		if(%icon $= "")
		{
			%this.setHudMenuR(%i, "", 1, 0);
		}
		else
		{
			%icon = "<bitmap:share/hud/rotc/icon." @ %icon @ ".20x20>";
			%this.setHudMenuR(%i, %icon @ "<sbreak>", 1, 1);
		}
	}
}

function GameConnection::changeInventory(%this, %nr)
{
	if($Game::GameType == $Game::mEthMatch)
		return;

	if(%this.inventoryMode $= "show")
	{
		if(%nr < 1 || %nr > 3)
			return;

		%this.inventoryMode = "select";
		%this.inventoryMode[1] = %nr;
		%this.displayInventory();
	}
	else if(%this.inventoryMode $= "select")
	{
		if($Game::GameType == $Game::TeamJoust
		|| $Game::GameType == $Game::TeamDragRace)
		{
			if(%nr < 1 || %nr > 5)
				return;

			switch(%nr)
			{
				case 1: %equipment = $CatEquipment::Blaster;
				case 2: %equipment = $CatEquipment::BattleRifle;
				case 3: %equipment = $CatEquipment::SniperRifle;
				case 4: %equipment = $CatEquipment::MiniGun;
				case 5: %equipment = $CatEquipment::GrenadeLauncher;
			}
		}
		else
		{
			if(%nr < 1 || %nr > 7)
				return;

			switch(%nr)
			{
				case 1: %equipment = $CatEquipment::Blaster;
				case 2: %equipment = $CatEquipment::BattleRifle;
				case 3: %equipment = $CatEquipment::SniperRifle;
				case 4: %equipment = $CatEquipment::MiniGun;
				case 5: %equipment = $CatEquipment::RepelGun;
				case 6: %equipment = $CatEquipment::Etherboard;
				case 7: %equipment = $CatEquipment::Regeneration;
			}
		}

		%this.loadout[%this.inventoryMode[1]] = %equipment;
		%this.updateLoadout();

		%this.inventoryMode = "show";
		%this.displayInventory(0);
	}
}

//------------------------------------------------------------------------------

function GameConnection::updateHudColors(%this)
{
	if(getFieldCount(%this.hudColor) == 2)
	{
		commandToClient(%this,'SetHudColor', getField(%this.hudColor,0), getField(%this.hudColor,1));			
	}
	else if(%this.hudColor $= "fr1tz")
	{
		commandToClient(%this,'SetHudColor', "200 200 200", "255 0 255");	
	}
	else if(%this.hudColor $= "kurrata")
	{
		commandToClient(%this,'SetHudColor', "0 150 0", "255 50 255");	
	}
	else if(%this.hudColor $= "c&c")
	{
		commandToClient(%this,'SetHudColor', "63 151 48", "202 180 130");	
	}	
	else if(%this.hudColor $= "cga1dark")
	{
		commandToClient(%this,'SetHudColor', "170 0 170", "0 170 170");	
	}
	else if(%this.hudColor $= "cga1light")
	{
		commandToClient(%this,'SetHudColor', "255 80 255", "85 255 255");	
	}	
	else
	{
		%teamId = %this.team.teamId;
	
		if(%teamId == 0)
			commandToClient(%this,'SetHudColor', "150 150 150", "255 255 255");
		else if(%teamId == 1)
			commandToClient(%this,'SetHudColor', "255 0 0", "255 200 200");
		else if(%teamId == 2)
			commandToClient(%this,'SetHudColor', "0 100 255", "200 200 255");	
	}	
}

//------------------------------------------------------------------------------

function GameConnection::joinTeam(%this, %teamId)
{
	if (%teamid > 2 || %teamid < 0)
		return false;

	if( %this.team != 0 && %teamId == %this.team.teamId)
		return false;

	// remove from old team...
	if(%this.team == $Team0)
		$Team0.numPlayers--;
	else if(%this.team == $Team1)
		$Team1.numPlayers--;
	else if(%this.team == $Team2)
		$Team2.numPlayers--;

	// add client to new team...
	if(%teamId == 0)
	{
		%this.team = $Team0;
		$Team0.numPlayers++;
		
		%this.setNewbieHelp("You are in observer mode. Click on the links near the top" SPC
			"of the arena window to join a team. Press @bind01 if the arena window is not visible.");		
   
      %this.setLoadingBarText("Use the links below to join red or blue!");
	}
	else
   {
      if(%teamId == 1)
   	{
   		%this.team = $Team1;
   		$Team1.numPlayers++;
   	}
   	else if(%teamId == 2)
   	{
   		%this.team = $Team2;
   		$Team2.numPlayers++;
   	}
    
      %this.setLoadingBarText("Press @bind01 to play.");
   }

	%this.updateHudColors();

	// full and simple control cleanup...
	%this.clearFullControl();
	%this.clearSimpleControl();

	// notify all clients of team change...
	MessageAll('MsgClientJoinTeam', '\c2%1 joined the %2.',
		%this.name,
		%this.team.name,
		%this.team.teamId,
		%this,
		%this.sendGuid,
		%this.score,
		%this.isAiControlled(),
		%this.isAdmin,
		%this.isSuperAdmin);

	%this.spawnPlayer();
	
	%count = ClientGroup.getCount();
	for(%cl= 0; %cl < %count; %cl++)
		ClientGroup.getObject(%cl).updateQuickbar();

	return true;
}

function GameConnection::spawnPlayer(%this)
{
	// remove existing player...
	if(%this.player > 0)
		%this.player.delete();

	// observers have no players...
	if( %this.team == $Team0 )
	{
		%this.setControlObject(%this.camera);
		return;
	}

	%spawnSphere = pickSpawnSphere(%this.team.teamId);

	%data = %this.getEtherformDataBlock();
	%obj = new Etherform() {
		dataBlock = %data;
		client = %this;
		teamId = %this.team.teamId;
	};

	// player setup...
	%obj.setTransform(%spawnSphere.getTransform());
	%obj.setCurrTagged(0);
	%obj.setCurrTaggedPos("0 0 0");

	// update the client's observer camera to start with the player...
	%this.camera.setMode("Observer");
	%this.camera.setTransform(%this.player.getEyeTransform());

	// give the client control of the player...
	%this.player = %obj;
	%this.setControlObject(%obj);
}

//-----------------------------------------------------------------------------

function GameConnection::beepMsg(%this, %reason)
{
	//MessageClient(%this, 'MsgBeep', '\c0Fail: %1', %reason);
	//bottomPrint(%this, %reason, 3, 1 );
   %this.setHudWarning(2, %reason, true);
   if(%this.clearBeepMsgThread !$= "")
      cancel(%this.clearBeepMsgThread);
   %this.clearBeepMsgThread = %this.schedule(4000, "setHudWarning", 2, "", false);
	%this.play2D(BeepMessageSound);
}

function GameConnection::togglePlayerForm(%this, %forced)
{
	if(!isObject(%this.player))
		return;
		
	%tagged = %this.player.isTagged();
	%pos = %this.player.getWorldBoxCenter();

	if(%this.player.getClassName() !$= "Etherform")
	{
		// Manifestation -> Etherform
		
		if($Server::NewbieHelp)
		{
			%this.newbieHelpData_NeedsRepair = 
				(%this.player.getDamageLevel() > %this.player.getDataBlock().maxDamage*0.75);
			%this.newbieHelpData_LowEnergy = 
				(%this.player.getEnergyLevel() < 50);
		}		
	
		%data = %this.getEtherformDataBlock();
		%obj = new Etherform() {
			dataBlock = %data;
			client = %this;
			teamId = %this.team.teamId;
		};	
	}
	else
	{
		// Etherform -> Manifestation

		if(!%forced)
		{
			if(%this.player.getDamageLevel() > %this.player.getDataBlock().maxDamage*0.75)
			{
				%this.beepMsg("You need at least 25% health to manifest!");
				return;
			}
			
			if(%this.player.getEnergyLevel() < 50)
			{
				%this.beepMsg("You need at least 50% armor to manifest!");
				return;
			}

			%ownTeamId = %this.player.getTeamId();
	
			%inOwnZone = false;
			%inOwnTerritory = false;
			%inEnemyZone = false;
	
			InitContainerRadiusSearch(%pos, 0.0001, $TypeMasks::TacticalZoneObjectType);
			while((%srchObj = containerSearchNext()) != 0)
			{
				// object actually in this zone?
				%inSrchZone = false;
				for(%i = 0; %i < %srchObj.getNumObjects(); %i++)
				{
					if(%srchObj.getObject(%i) == %this.player)
					{
						%inSrchZone = true;
						break;
					}
				}
				if(!%inSrchZone)
					continue;
	
				%zoneTeamId = %srchObj.getTeamId();
				%zoneBlocked = %srchObj.zBlocked;
	
				if(%zoneTeamId != %ownTeamId && %zoneTeamId != 0)
				{
					%inEnemyZone = true;
					break;
				}
				else if(%zoneTeamId == %ownTeamId)
				{
					%inOwnZone = true;
					if(%srchObj.getDataBlock().getName() $= "TerritoryZone"
					|| %srchObj.getDataBlock().isTerritoryZone)
						%inOwnTerritory = true;
				}
			}
	
			if(%inEnemyZone)
			{
				%this.beepMsg("You can not manifest in an enemy zone!");
				return;
			}
			else if(%inOwnZone && !%inOwnTerritory)
			{
				%this.beepMsg("This is not a territory zone!");
				return;
			}
			else if(!%inOwnZone)
			{
				%this.beepMsg("You can only manifest in your team's territory zones!");
				return;
			}
			else if(%zoneBlocked)
			{
				%this.beepMsg("This zone is currently blocked!");
				return;
			}
		}
		
		// Manifestation form from nearby blueprint?
		%blueprint = 0; 
		InitContainerRadiusSearch(%pos, 10, $TypeMasks::StaticShapeObjectType);
		while((%srchObj = containerSearchNext()) != 0)
		{
			if(%srchObj.isBlueprint)
			{
				//echo("Found blueprint!");
				%blueprint = %srchObj;
				break;
			}
		}			
		
		if(%blueprint != 0)
		{
			%obj = %blueprint.getDataBlock().build(%blueprint, %this);
			
			if(!isObject(%obj))
			{
				bottomPrint(%this, "Failed to manifest!", 3, 1 );
				return;			
			}
			
			%pos = %blueprint.getTransform();
		}
		else
		{
			if($Game::GameType == $Game::GridWars)
			{
				// Manifest into infantry CAT
				if( %this.team == $Team1 )
					%data = RedInfantryCat;
				else
					%data = BlueInfantryCat;
			}
			else
			{
				// Manifest into standard CAT form
				if( %this.team == $Team1 )
					%data = RedStandardCat;
				else
					%data = BlueStandardCat;
			}

			%obj = new Player() {
				dataBlock = %data;
				client = %this;
				teamId = %this.team.teamId;
			};			
		}

		$aiTarget = %obj;
	}
	
	MissionCleanup.add(%obj);
	
	%mat = %this.player.getTransform();
	%dmg = %this.player.getDamageLevel();
	%nrg = %this.player.getEnergyLevel();
	%buf = %this.player.getDamageBufferLevel();
	%vel = %this.player.getVelocity();

	%obj.setTransform(%mat);
	%obj.setTransform(%pos);
	%obj.setDamageLevel(%dmg);
	%obj.setShieldLevel(%buf);
	
	if(%tagged)
		%obj.setTagged();

	%this.control(%obj);
	
	if(%this.player.getClassName() $= "Etherform")
	{
		// Etherform -> Manifestation
		
		// remove any z-velocity...
		%vel = getWord(%vel, 0) SPC getWord(%vel, 1) SPC "0";
	
		%this.player.delete();

		%obj.setEnergyLevel(%nrg);
		%obj.setVelocity(VectorScale(%vel, 0.25));
		
		%obj.startFade(1000,0,false);
		%obj.playAudio(0, CatSpawnSound);		
	}
	else
	{
		// Manifestation -> Etherform
	
		if(%this.player.getDamageState() $= "Enabled")
		{
			//if(%this.player.getDataBlock().damageBuffer - %buf > 1)
			//{
			//	%this.player.setDamageState("Disabled");
			//	%this.player.playDeathAnimation(0, 0);
			//}
			//else
			//{
				%this.player.setDamageState("Destroyed");
			//}
		}
		
		%obj.setEnergyLevel(%nrg - 50);
		%obj.applyImpulse(%pos, VectorScale(%vel,100));
		%obj.playAudio(0, EtherformSpawnSound);	
	}

	%this.player = %obj;
}

//-----------------------------------------------------------------------------

function GameConnection::setSkyColor(%this, %color)
{
	//error("GameConnection::setSkyColor():" SPC %this.getId() SPC %color);

	if(%this.skyColor $= %color)
		return;

	messageClient(%this, 'MsgSkyColor', "", %color);

	%this.skyColor = %color;
}

function GameConnection::updateSkyColor(%this)
{
	if(%this.skyColorThread !$= "")
		cancel(%this.skyColorThread);
	%this.skyColorThread = %this.schedule(500, "updateSkyColor");

	if(Sky.NoColorChange)
		return;

	// In certain gametypes the sky is the same for every client
	if($Game::GameType == $Game::TeamDragRace)
		return;

	%player = %this.player;
	if(!isObject(%player))
		%player = %this.camera;

	%pos = %player.getPosition();
	InitContainerRadiusSearch(%pos, 0.0001, $TypeMasks::TacticalZoneObjectType);
	%zone = containerSearchNext();

	if(%zone == 0)
	{
		%this.setSkyColor("0.2 0.2 0.2");
	}
	else if(%zone.getTeamId() == 0 && %zone.zProtected)
	{
		%this.setSkyColor("0 1 0");
	}
	else if(%zone.getTeamId() == 0 && !%zone.zHasNeighbour)
	{
		%this.setSkyColor("0 0.5 0");
	}	
	else if(%zone.getTeamId() == 0 && %zone.zNumReds == 0 && %zone.zNumBlues == 0)
	{
		%this.setSkyColor("1 1 1");
	}
	else if(%zone.getTeamId() == 1 && !%zone.zBlocked)
	{
		%this.setSkyColor("1 0 0");
	}
	else if(%zone.getTeamId() == 2 && !%zone.zBlocked)
	{
		%this.setSkyColor("0 0 1");
	}
	else
	{
		%health[1] = 0;
		%health[2] = 0;
		for(%i = 0; %i < %zone.getNumObjects(); %i++)
		{
			%obj = %zone.getObject(%i);
			if(%obj.getType() & $TypeMasks::PlayerObjectType && %obj.isCAT)
			{
				%health[%obj.teamId] += %obj.getDataBlock().maxDamage
					- %obj.getDamageLevel() + %obj.getDamageBufferLevel();
			}
		}
		if(%health[1] > %health[2])
		{
			%ratio = %health[2] / %health[1];
			%this.setSkyColor("1 0.75" SPC %ratio);
		}
		else
		{
			%ratio = %health[1] / %health[2];
			%this.setSkyColor(%ratio SPC "0.75 1");
		}
	}
}

//-----------------------------------------------------------------------------

function GameConnection::setLoadingBarText(%this, %text)
{
   if(%this.loadingBarText $= %text)
      return;
	commandToClient(%this, 'LoadingBarTxt', %text);
   %this.loadingBarText = %text;
}

//-----------------------------------------------------------------------------

function GameConnection::beginQuickbarText(%this, %update)
{
	commandToClient(%this, 'BeginQuickbarTxt', %update);
}

function GameConnection::addQuickbarText(%this, %text)
{
	%l = strlen(%text); %n = 0;
	while(%n < %l)
	{
		%chunk = getSubStr(%text, %n, 255);
		commandToClient(%this, 'AddQuickbarTxt', %chunk);
		%n += 255;
	}	
}

function GameConnection::endQuickbarText(%this)
{
	commandToClient(%this, 'EndQuickbarTxt');
}

function GameConnection::beginMenuText(%this, %update)
{
	commandToClient(%this, 'BeginMenuTxt', %update);
}

function GameConnection::addMenuText(%this, %text)
{
	%l = strlen(%text); %n = 0;
	while(%n < %l)
	{
		%chunk = getSubStr(%text, %n, 255);
		commandToClient(%this, 'AddMenuTxt', %chunk);
		%n += 255;
	}	
}

function GameConnection::endMenuText(%this)
{
	commandToClient(%this, 'EndMenuTxt');
}

//-----------------------------------------------------------------------------

function GameConnection::setHudBackground(%this, %slot, %bitmap, %color,
	%repeat, %alpha, %alphaDt)
{
	if(%this.hudBackgroundBitmap[%slot] $= %bitmap)
		%bitmap = "";
	else
		%this.hudBackgroundBitmap[%slot] = %bitmap;	

	if(%this.hudBackgroundColor[%slot] $= %color)
		%color = "";
	else
		%this.hudBackgroundColor[%slot] = %color;

	if(%this.hudBackgroundRepeat[%slot] $= %repeat)
		%repeat = "";
	else
		%this.hudBackgroundRepeat[%slot] = %repeat;		

	if(%this.hudBackgroundAlphaDt[%slot] $= %alphaDt)
		%alphaDt = "";
	else
		%this.hudBackgroundAlphaDt[%slot] = %alphaDt;	

	commandToClient(%this, 'SetHudBackground', %slot, %bitmap, %color, 
		%repeat, %alpha, %alphaDt);
}

//-----------------------------------------------------------------------------

function GameConnection::setHudWarning(%this, %slot, %text, %visibility)
{
	if(%this.hudWarningString[%slot] $= %text)
		%text = "";
	else
		%this.hudWarningString[%slot] = %text;	

	if(%this.hudWarningVisible[%slot] $= %visibility)
		%visibility = "";
	else
		%this.hudWarningVisible[%slot] = %visibility;	

	if(%text $= "" && %visibility $= "")
		return;

	//error("Sending MsgWarning to" SPC %this SPC ": [" SPC %slot SPC "][" SPC %text SPC "][" SPC %visibility SPC "]");
	messageClient(%this, 'MsgWarning', "", %slot, %text, %visibility);
}

function GameConnection::updateHudWarningsThread(%this)
{
	cancel(%this.updateHudWarningsThread);
	%this.updateHudWarningsThread = %this.schedule(100,"updateHudWarningsThread");

	%player = %this.player;
	if(!isObject(%player))
	{
		%this.setHudWarning(1, "", false);
		%this.setHudWarning(3, "", false);	
		return;
	}

	%health = %player.getDataBlock().maxDamage 
		- %player.getDamageLevel() 
		+ %player.getDamageBufferLevel();
	%health = %health / %player.getDataBlock().maxDamage;

	%this.setHudWarning(1, "[HEALTH]", %health < 0.25);
   if(%player.isCAT)
	  %this.setHudWarning(3, "[DAMPER]", %player.getEnergyPercent() < 0.5);
   else
	  %this.setHudWarning(3, "[ENERGY]", %player.getEnergyPercent() < 0.5);
}

//-----------------------------------------------------------------------------

function GameConnection::switchTopHudMenuMode(%this)
{
	if(%this.topHudMenu $= "newbiehelp")
	{
		%this.topHudMenu = "healthbalance";
	}
	else if(%this.topHudMenu $= "healthbalance")
	{
		%this.topHudMenu = "nothing";
	}
	else 
	{
		%this.topHudMenu = "newbiehelp";
	}
	
	%this.setHudMenuT("*", " ", 1, 0);
}

function byteToHex(%byte)
{
	%chars = "0123456789ABCDEF";
	
	%digit[0] = "0";
	%digit[1] = "0";
	
	if(%byte > 15)
		%digit[0] = getSubStr(%chars, %byte / 16, 1);

	%digit[1] = getSubStr(%chars, %byte % 16, 1);

	return %digit[0] @ %digit[1];
}

datablock AudioProfile(NewbieHelperSound)
{
	filename = "share/sounds/rotc/charge1.wav";
	description = AudioCritical2D;
	preload = true;
};

datablock AudioProfile(ClockTickSound)
{
	filename = "share/sounds/rotc/charge3.wav";
	description = AudioCritical2D;
	preload = true;
};

function GameConnection::updateTopHudMenuThread(%this)
{
	cancel(%this.updateTopHudMenuThread);
	%this.updateTopHudMenuThread = %this.schedule(200,"updateTopHudMenuThread");
	
	if(%this.topHudMenu $= "invisible")
		return;
	
	%this.setHudMenuT(0, "\n<just:center><color:888888>Showing: ", 1, 1);			
	%this.setHudMenuT(2, "(@bind66 to change)\n<just:left>", 1, 1);			
	%i = 2;
	
	if(%this.topHudMenu $= "newbiehelp")
	{
		if(%this.newbieHelpAge == 0)
			%this.play2D(NewbieHelperSound);
	
		%this.newbieHelpAge++;	
		%this.setHudMenuT(1, "Newbie Helper", 1, 1);

		%color = "FFFFFF";
		%alpha = 255;		
		
		%text = %this.newbieHelpText;
		if(%this.newbieHelpAge < 6)
		{
			%text = "[ Downloading... ]";
			if(%this.newbieHelpAge % 2 == 0)
				%color = "88FF88";			
		}
		
		if(%this.newbieHelpTime > 0 && %this.newbieHelpAge > %this.newbieHelpTime)
			%alpha = 255 - (%this.newbieHelpAge-%this.newbieHelpTime)*15;
		
		if(%alpha <= 0)
		{
			%alpha = 0;
			%this.setHudMenuT(5, "", 1, 0);
			%this.setHudMenuT(8, "", 1, 0);		
		}

		%this.setHudMenuT(%i++, "<just:center>(Press @bind65 for a random hint)\n<font:NovaSquare:18><color:FFFFFF", 1, 1);
		%this.setHudMenuT(%i++, byteToHex(%alpha), 1, 1);				
		%this.setHudMenuT(%i++, ">Hint:\n<color:", 1, 1);		
		%this.setHudMenuT(%i++, %color, 1, 1);					
		%this.setHudMenuT(%i++, byteToHex(%alpha), 1, 1);				
		%this.setHudMenuT(%i++, ">" @ %text, 1, 1);
	}	
	else if(%this.topHudMenu $= "healthbalance")
	{	
		%this.setHudMenuT(1, "Health balance", 1, 1);
		%this.setHudMenuT(3, "<bitmap:share/hud/rotc/spec><sbreak>", 1, 1);
		%this.setHudMenuT(4, "<bitmap:share/hud/rotc/spacer.1x14>", $Server::GameStatus::HealthBalance::Spacers, 1);
		%this.setHudMenuT(5, "<bitmap:share/hud/rotc/marker.up>", 1, 1);			
	}
	else if(%this.topHudMenu $= "teamjoustclock")
	{		
		%this.setHudMenuT(1, "Clock", 1, 1);

	}
	else if(%this.topHudMenu $= "nothing")
	{		
		%this.setHudMenuT(1, "Nothing", 1, 1);
	}
}

//-----------------------------------------------------------------------------

function GameConnection::setNewbieHelp(%this, %msg, %time)
{
	if(%msg $= "random")
	{
		%isCAT = %time; // hackety hack hack

		%i = -1;
		if(%this.hasEtherboard)
		{
			%tip[%i++] = "When in CAT form, hold down @bind21 to use your etherboard.";
		}
		if(%this.numRegenerators > 0)
		{
			%tip[%i++] = "Every additional regeneration module doubles the rate of regeneration.";
		}
		if(%this.numWeapons > 1)
		{
			%tip[%i++] = "When in CAT form, press @bind45 to cycle through your weapons.";
		}
		if(%isCAT)
		{

		}
		%tip[%i++] = "Beware that firing your weapons draws power away from your armor.";
		%tip[%i++] = "When in CAT form, close to the ground and not etherboarding," SPC
			"you become anchored to the ground which reduces the push you get from being hit.";
		%tip[%i++] = "When in CAT form, press @bind51 to fire a B.O.U.N.C.E. that reverses the momentum" SPC
			"of nearby enemy CATs and deals some damage based on their speed.";
		%tip[%i++] = "When in CAT form, hold down and release @bind19 to throw a grenade. Press @bind46 to" SPC
			"throw a grenade with max. force.";
		%tip[%i++] = "If you've aquired a disc lock (you'll hear three short beeps) you can" SPC
			"launch a target-seeking disc: Press @bind47 for explosive disc, @bind48 for repel disc.";
		%tip[%i++] = "In CAT form, you can deflect incoming discs by keeping your crosshair" SPC
			"over the disc until it's locked and then pressing @bind17.";
		%tip[%i++] = "When in CAT form, press @bind18 to jump. Pressing @bind18 while in the air will fire" SPC
			"your CAT's jump boosters, which drains power away from your armor.";
		%tip[%i++] = "When full, your armor absorbs 50% of the damage you take. The" SPC
			"percentage decreases in a linear fashion with less armor.";
		%tip[%i++] = "In etherform you can select the equipment you'll have" SPC
				"in CAT form." SPC (%isCAT ? "" : "See the icons" SPC
				"next to the health bar to the left.") SPC "";
		%tip[%i++] = "Dark green zones can't be captured.";
		%tip[%i++] = "CATs can only capture zones that border on one of their team's zones.";
		%tip[%i++] = "Orange/cyan zones are zones that still belong to red/blue but are being blocked" SPC
			"by the presence of an enemy CAT, which prevents red/blue etherforms from manifesting there.";
		%tip[%i++] = "Damaging enemies restores your health and your armor.";
		%tip[%i++] = "Keep an eye on your armor (the bar on the right), CATs with low armor are easily destroyed.";
		%tip[%i++] = "The sky changing color is not just eyecandy. In a contested zone, the red/blue" SPC
			"values of the sky color reflect the amount of health of red/blue CATs in the zone.";
		%tip[%i++] = "Press @bind25 for arena-wide chat, @bind26 for team-chat.";			
		%tip[%i++] = "Looking for more players? - Try the global player chat (accessible" SPC
			"through the \"Client Toolbox\" window).";		
		%tip[%i++] = "To get a complete overview of the zones, press @bind30 for a bigger" SPC
			"minimap. If you want to look at the precise landscape, press @bind31.";		

		%rand = getRandom(%i);
		%msg = %tip[%rand];
		%time = (%isCAT ? 60 : 0);
	}

	%this.newbieHelpText = %msg;
	%this.newbieHelpTime = %time;
	%this.newbieHelpAge = 0;

	if(this.topHudMenu $= "newbiehelp")
		%this.updateTopHudMenuThread();
}

//-----------------------------------------------------------------------------

function GameConnection::setHudMenuL(%this, %slot, %text, %repetitions, %visible)
{
	if(%slot $= "*")
	{
		for(%i = 0; %i < 10; %i++)
		{
			if(%text !$= "") %this.hudMenuLText[%i] = %text;
			if(%repetitions !$= "") %this.hudMenuLRepetitions[%i] = %repetitions;
			if(%visible !$= "") %this.hudMenuLVisible[%i] = %visible;
		}
	}
	else
	{
		if(%this.hudMenuLText[%slot] $= %text)
			%text = "";
		else
			%this.hudMenuLText[%slot] = %text;	

		if(%this.hudMenuLRepetitions[%slot] $= %repetitions)
			%repetitions= "";
		else
			%this.hudMenuLRepetitions[%slot] = %repetitions;

		if(%this.hudMenuLVisible[%slot] $= %visible)
			%visible = "";
		else
			%this.hudMenuLVisible[%slot] = %visible;	

		if(%text $= "" && %repetitions $= "" && %visible $= "")
			return;
	}

	//error("Sending 'MsgHudMenuL' to" SPC %this SPC ": [" SPC %slot SPC "][" SPC %text SPC "][" SPC %repetitions SPC "][" SPC %visible SPC "]");
	messageClient(%this, 'MsgHudMenuL', "", %slot, %text, %repetitions, %visible);
}

function GameConnection::setHudMenuR(%this, %slot, %text, %repetitions, %visible)
{
	if(%slot $= "*")
	{
		for(%i = 0; %i < 10; %i++)
		{
			if(%text !$= "") %this.hudMenuRText[%i] = %text;
			if(%repetitions !$= "") %this.hudMenuRRepetitions[%i] = %repetitions;
			if(%visible !$= "") %this.hudMenuRVisible[%i] = %visible;
		}
	}
	else
	{
		if(%this.hudMenuRText[%slot] $= %text)
			%text = "";
		else
			%this.hudMenuRText[%slot] = %text;	

		if(%this.hudMenuRRepetitions[%slot] $= %repetitions)
			%repetitions= "";
		else
			%this.hudMenuRRepetitions[%slot] = %repetitions;

		if(%this.hudMenuRVisible[%slot] $= %visible)
			%visible = "";
		else
			%this.hudMenuRVisible[%slot] = %visible;	

		if(%text $= "" && %repetitions $= "" && %visible $= "")
			return;
	}

	//error("Sending 'MsgHudMenuR' to" SPC %this SPC ": [" SPC %slot SPC "][" SPC %text SPC "][" SPC %repetitions SPC "][" SPC %visible SPC "]");
	messageClient(%this, 'MsgHudMenuR', "", %slot, %text, %repetitions, %visible);
}

function GameConnection::setHudMenuT(%this, %slot, %text, %repetitions, %visible)
{
	if(%slot $= "*")
	{
		for(%i = 0; %i < 10; %i++)
		{
			if(%text !$= "") %this.hudMenuTText[%i] = %text;
			if(%repetitions !$= "") %this.hudMenuTRepetitions[%i] = %repetitions;
			if(%visible !$= "") %this.hudMenuTVisible[%i] = %visible;
		}
	}
	else
	{
		if(%this.hudMenuTText[%slot] $= %text)
			%text = "";
		else
			%this.hudMenuTText[%slot] = %text;	

		if(%this.hudMenuTRepetitions[%slot] $= %repetitions)
			%repetitions= "";
		else
			%this.hudMenuTRepetitions[%slot] = %repetitions;

		if(%this.hudMenuTVisible[%slot] $= %visible)
			%visible = "";
		else
			%this.hudMenuTVisible[%slot] = %visible;	

		if(%text $= "" && %repetitions $= "" && %visible $= "")
			return;
	}

	//error("Sending 'MsgHudMenuT' to" SPC %this SPC ": [" SPC %slot SPC "][" SPC %text SPC "][" SPC %repetitions SPC "][" SPC %visible SPC "]");
	messageClient(%this, 'MsgHudMenuT', "", %slot, %text, %repetitions, %visible);
}

//-----------------------------------------------------------------------------

function GameConnection::updateQuickbar(%this)
{
	%r = "<just:center><font:NovaSquare:16>";

	if(%this.loadingMission)
	{
		%joinText = "Can't join a team while arena is loading";
	}
	else
	{
		%joinText = "Join Team:\n<spush><font:NovaSquare:24>";
		if(%this.team != $Team1)
			%joinText = %joinText @ "<a:cmd JoinTeam 1>";
		%joinText = %joinText @ "Reds (" @ $Team1.numPlayers @ ")";
		if(%this.team != $Team1)
			%joinText = %joinText @ "</a>";		
		%joinText = %joinText @ "    ";		
		if(%this.team != $Team2)
			%joinText = %joinText @ "<a:cmd JoinTeam 2>";
		%joinText = %joinText @ "Blues (" @ $Team2.numPlayers @ ")";
		if(%this.team != $Team2)
			%joinText = %joinText @ "</a>";	
		%joinText = %joinText @ "    ";	
		if(%this.team != $Team0)
			%joinText = %joinText @ "<a:cmd JoinTeam 0>";
		%joinText = %joinText @ "Observers (" @ $Team0.numPlayers @ ")";
		if(%this.team != $Team0)
			%joinText = %joinText @ "</a>";			
	}
	
	%r = %r @ %joinText;	
	%r = %r @ "<spop>\n<bitmap:share/misc/ui/sep><sbreak>";	
	%r = %r @ "Show: ";
	%r = %r @ "<a:cmd MainMenu>Arena Info</a> | ";
	%r = %r @ "<a:cmd ShowPlayerList>Player List</a> | ";
	%r = %r @ "<a:cmd HowToPlay 0>Manual</a> | ";
	%r = %r @ "<a:cmd ShowSettings>Settings</a>";	

	%this.beginQuickbarText();
	%this.addQuickbarText(%r);	
	%this.endQuickbarText();
}

//-----------------------------------------------------------------------------

function GameConnection::setHandicap(%this, %handicap)
{
	if(%handicap $= "")
		%this.handicap = 1;
	else if(0 <= %handicap && %handicap <= 1)
		%this.handicap = %handicap;
	else
		%this.handicap = 1;
		
	if(isObject(%this.player))
		%this.player.getDataBlock().updateShapeName(%this.player);
}

//-----------------------------------------------------------------------------

function GameConnection::getEtherformDataBlock(%this)
{
	if(strstr(strlwr(getTaggedString(%this.name)),"nyan") != -1)
	{
		if( %this.team == $Team1 )
			return RedNyanEtherform;
		else
			return BlueNyanEtherform;
	}
	else
	{
		if( %this.team == $Team1 )
			return RedEtherform;
		else
			return BlueEtherform;
	}
}



