//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Cat5 - tacticalZones.cs
// Script-stuff for tactical zones
//------------------------------------------------------------------------------

//
// the following dynamic fields can be added to the TacticalZone *objects*...
//	 initialOwner:  team id set on reset
//	 initiallyProtected: protected on reset?
//

function TacticalZoneData::create(%data)
{
	// The mission editor invokes this method when it wants to create
	// an object of the given datablock type.  For the mission editor
	%obj = new TacticalZone() {
		dataBlock = %data;
		initialOwner = 0;
		initiallyProtected = false;
	};
	return %obj;
}

//------------------------------------------------------------------------------
// Territory Zones

function TerritoryZone_find(%name)
{
	%group = nameToID("TerritoryZones");

	if (%group != -1)
	{
		%count = %group.getCount();
		if(%count != 0)
		{
				for (%i = 0; %i < %count; %i++)
				{
					%zone = %group.getObject(%i);
					if(%zone.getName() $= %name)
						return %zone;
				}
		}
		else
			error("TerritoryZones_call():" SPC
				"no TacticalZones found in TerritoryZones group!");
	}
	else
		error("TerritoryZones_call(): missing TerritoryZones group!");

	return -1;
}

//------------------------------------------------------------------------------

function TerritoryZones_enableRepair(%shape)
{
	if(%shape.isCAT)
		return;

	if(%shape.getTeamId() == $Team1.teamId)
	{
		if(!$Team1.repairObjects.isMember(%shape))
			$Team1.repairObjects.add(%shape);
	}
	else if(%shape.getTeamId() == $Team2.teamId)
	{
		if(!$Team2.repairObjects.isMember(%shape))
			$Team2.repairObjects.add(%shape);
	}
}

function TerritoryZones_disableRepair(%shape)
{
	if(%shape.isCAT)
		return;

	if(%shape.getTeamId() == $Team1.teamId)
	{
		if($Team1.repairObjects.isMember(%shape))
			$Team1.repairObjects.remove(%shape);
	}
	else if(%shape.getTeamId() == $Team2.teamId)
	{
		if($Team2.repairObjects.isMember(%shape))
			$Team2.repairObjects.remove(%shape);
	}
	
	%shape.setRepairRate(0);
}

//------------------------------------------------------------------------------

function TerritoryZones_repairTick()
{
	cancel($TerritoryZones_repairTickThread);
 
    if(!isObject($Team1.repairObjects) || !isObject($Team2.repairObjects))
        return;

	%count = $Team1.repairObjects.getCount();
	if(%count != 0)
	{
		%repair = $Team1.repairSpeed / %count;
		for (%i = 0; %i < %count; %i++)
		{
			%obj = $Team1.repairObjects.getObject(%i);
			%obj.setRepairRate(%repair);
		}
	}

	%count = $Team2.repairObjects.getCount();
	if(%count != 0)
	{
		%repair = $Team2.repairSpeed / %count;
		for (%i = 0; %i < %count; %i++)
		{
			%obj = $Team2.repairObjects.getObject(%i);
			%obj.setRepairRate(%repair);
		}
	}

	$TerritoryZones_repairTickThread =
		schedule(100, 0, "TerritoryZones_repairTick");
}

//-----------------------------------------------------------------------------

// to reset all the territory zones...
function TerritoryZones_reset()
{
	%group = nameToID("TerritoryZones");

	if (%group != -1)
	{
		%count = %group.getCount();
		if (%count != 0)
		{
				for (%i = 0; %i < %count; %i++)
				{
					%zone = %group.getObject(%i);
					%zone.getDataBlock().reset(%zone);
				}
				
				TerritoryZones_repairTick();
		}
		else
			error("TerritoryZones_reset():" SPC
				"no TacticalZones found in TerritoryZones group!");
	}
	else
		error("TerritoryZones_reset(): missing TerritoryZones group!");
}

//-----------------------------------------------------------------------------

function TerritoryZones_call(%func)
{
	%group = nameToID("TerritoryZones");

	if (%group != -1)
	{
		%count = %group.getCount();
		if (%count != 0)
		{
				for (%i = 0; %i < %count; %i++)
				{
					%zone = %group.getObject(%i);
					call(%func, %zone);
				}
		}
		else
			error("TerritoryZones_call():" SPC
				"no TacticalZones found in TerritoryZones group!");
	}
	else
		error("TerritoryZones_call(): missing TerritoryZones group!");
}

//-----------------------------------------------------------------------------
// Territory Zone Sounds

datablock AudioProfile(OwnZonesIncreasedSound)
{
	filename = "share/sounds/cat5/ditty1upshort.wav";
	description = AudioCritical2D;
	preload = true;
};

datablock AudioProfile(EnemyZonesIncreasedSound)
{
	filename = "share/sounds/cat5/ditty1downshort.wav";
	description = AudioCritical2D;
	preload = true;
};

datablock AudioProfile(ZoneAttackedSound)
{
	filename = "share/sounds/cat5/alert1.wav";
	description = AudioCritical2D;
	preload = true;
};

//-----------------------------------------------------------------------------
// Territory Zone

datablock TacticalZoneData(TerritoryZone)
{
   allowColorization = true;

	category = "Tactical Zones"; // for the mission editor

	// The period is value is used to control how often the console
	// onTick callback is called while there are any objects
	// in the zone.  The default value is 100 MS.
	tickPeriodMS = 200;

	colorChangeTimeMS = 200;

	colors[0]  = "0.5 0.5 0.5 0.5"; // default
	colors[1]  = "1.0 1.0 1.0 0.9"; // flash

    texture = "share/textures/cat5/zone.grid";
};

function TerritoryZone::onAdd(%this, %zone)
{
	%zone.teamId = 0;

	%zone.zNumReds = 0;
	%zone.zNumBlues = 0;
}

function TerritoryZone::onEnter(%this,%zone,%obj)
{
	//echo("entered territory zone");
	
	if(!%obj.getType() & $TypeMasks::ShapeBaseObjectType)
		return;

	if($Game::GameType == $Game::TeamJoust)
	{
		if(%obj.isCAT && %zone.initialOwner != 0 && %obj.getTeamId() != %zone.getTeamId())
        {
			%team = %obj.getTeamId() == 1 ? $Team1 : $Team2;
			%val = 1 - %obj.getDamagePercent();
			%val = %val / %team.numPlayersOnRoundStart;
			%team.score += %val;
			%zone.zValue -= %val;
			%zone.setColor(1, 8 + %zone.getTeamId(), %zone.zValue);
			%zone.zBlocked = true;
			%obj.kill();
		}
	}
	else
		%this.onTick(%zone);
	
	%obj.getDataBlock().updateZone(%obj, %zone);
}

function TerritoryZone::onLeave(%this,%zone,%obj)
{
	// note that this function does not get called immediately when an
	// object leaves the zone but when the zone registers the object's
	// absence when the zone ticks.

	//echo("left territory zone");
	
	if(!%obj.getType() & $TypeMasks::ShapeBaseObjectType)
		return;
		
	%this.onTick(%zone);

	%obj.getDataBlock().updateZone(%obj, 0);
}

function TerritoryZone::onTick(%this, %zone)
{
	if($Game::GameType == $Game::TeamJoust)
		return;

	%zone.zNumReds = 0;
	%zone.zNumBlues = 0;
	
	for(%i = 0; %i < %zone.getNumObjects(); %i++)
	{
		%obj = %zone.getObject(%i);
		if(%obj.getType() & $TypeMasks::PlayerObjectType
		&& %obj.isCAT)
		{
            if(%zone.zProtected && %obj.getTeamId() != %zone.getTeamId())
                %obj.kill();
            else if(%obj.getTeamId() == $Team1.teamId)
				%zone.zNumReds++;
			else if(%obj.getTeamId() == $Team2.teamId)
				%zone.zNumBlues++;
		}
	}

	%this.updateOwner(%zone);
}

function TerritoryZone::updateOwner(%this, %zone)
{
	%numConnections = 0;
	%connectedToRed = false;
	%connectedToBlue = false;

	for(%i = 0; %i < 4; %i++)
	{
		%z = %zone.zNeighbour[%i];
		if(isObject(%z))
		{
			%numConnections++;
			if(%z.getTeamId() == 1)
				%connectedToRed = true;
			if(%z.getTeamId() == 2)
				%connectedToBlue = true;
		}
	}

	if(%zone.zNumReds > 0 && %zone.zNumBlues == 0)
	{
		%this.setZoneOwner(%zone, 1);
	}
	else if(%zone.zNumBlues > 0 && %zone.zNumReds == 0)
	{
		%this.setZoneOwner(%zone, 2);
	}
	else if(%zone.zNumReds > 0 && %zone.zNumBlues > 0)
	{
		%this.setZoneOwner(%zone, 0);		
	}
//	else if(%numConnections == 1)
//	{
//		if(%zone.getTeamId() != 1 && %connectedToRed) // blue end zone
//		{
//			if(%zone.zNumReds > 0)
//				%this.setZoneOwner(%zone, 0);
//			else if(%zone.zNumBlues > 0)
//				%this.setZoneOwner(%zone, 2);
//		}
//		else if(%zone.getTeamId() != 2 && %connectedToBlue) // red end zone
//		{
//			if(%zone.zNumBlues > 0)
//				%this.setZoneOwner(%zone, 0);
//			else if(%zone.zNumReds > 0)
//				%this.setZoneOwner(%zone, 1);
//		}
//	}

	%zone.zBlocked = false;

	%color = 1;
	if(!%zone.zHasNeighbour)
	{
		%color = 13;
	}
	else if(%zone.getTeamId() == 2 && %zone.zNumReds != 0)
	{
		%zone.zBlocked = true;
		%color = 5;
	}
	else if(%zone.getTeamId() == 1 && %zone.zNumBlues != 0)
	{
		%zone.zBlocked = true;
		%color = 4;
	}
	else if(%zone.getTeamId() == 2)
		%color = 3;
	else if(%zone.getTeamId() == 1)
		%color = 2;

	//%zone.setColor(%color, %color, 1);
   //%zone.setColor(%color, %color, 1);

	if(%color != %zone.zColor)
   {
      %zone.flash(1, 1, 1);
		//%zone.flash(%color + 5, %color + 5, 1);
   }

	%zone.zColor = %color;
}

function TerritoryZone::setZoneOwner(%this, %zone, %teamId)
{
	%oldTeamId = %zone.getTeamId();
	
	if(%teamId == %oldTeamId)
		return;
		
	if(%oldTeamId == 1)
		$Team1.numTerritoryZones--;
	else if(%oldTeamId == 2)
		$Team2.numTerritoryZones--;
		
	%zone.setTeamId(%teamId);
	
	if(%teamId == 1)
		$Team1.numTerritoryZones++;
	else if(%teamId == 2)
		$Team2.numTerritoryZones++;

	for(%idx = 0; %idx < ClientGroup.getCount(); %idx++)
	{
		%client = ClientGroup.getObject(%idx);
	
		%sound = 0;

		if(%client.team != $Team0)
		{
         if(%teamId == 0 && %oldTeamId == %client.team.teamId)
				%sound = ZoneAttackedSound;
			else if(%client.team.teamId == %teamId)
				%sound = OwnZonesIncreasedSound;
			else if(%teamId != 0)
				%sound = EnemyZonesIncreasedSound;
		}

		if(isObject(%sound))
			%client.play2D(%sound);
	}
	
	for(%i = 0; %i < %zone.getNumObjects(); %i++)
	{
		%obj = %zone.getObject(%i);
		if(%obj.getType() & $TypeMasks::ShapeBaseObjectType)
			%obj.getDataBlock().updateZone(%obj, 0);
	}
		
	echo("Number of zones:" SPC
		$Team1.numTerritoryZones SPC "red /" SPC
		$Team2.numTerritoryZones SPC "blue");
		
	checkRoundEnd();
}

function TerritoryZone::reset(%this, %zone)
{
	if($Game::GameType == $Game::TeamJoust)
	{
		if($Game::TeamJoustState == 0)
		{
			%zone.setTeamId(%zone.initialOwner);
			if(%zone.initiallyProtected)
				%zone.isProtected = true;

			if(%zone.getTeamId() == 0 || %zone.initiallyProtected)
				%color = 7;
			else
				%color = 1 + %zone.getTeamId();
			%zone.setColor(%color, %color, 1);
		}
		else if($Game::TeamJoustState == 1)
		{
			if(%zone.getTeamId() == 0 || %zone.initiallyProtected)
				%zone.setColor(12, 12, 1);
		}
		else if($Game::TeamJoustState == 2)
		{
			if(%zone.getTeamId() == 0 || %zone.initiallyProtected)
				%zone.setColor(11, 11, 1);
		}
		else if($Game::TeamJoustState == 3)
		{
			if(%zone.getTeamId() == 0 || %zone.initiallyProtected)
			{
				if(%zone.getTeamId() == 0)
					%color = 13;
				else if(%zone.initiallyProtected)
					%color = 3 + %zone.getTeamId();
				%zone.setColor(%color, %color, 1);
			}
		}

		if($Game::TeamJoustState < 4)
		{
	  		if(%zone.getTeamId() == 0 || %zone.initiallyProtected)
	  			%zone.flash(15, 15, 1);
		}
	}
	else
	{
		%zone.zHasNeighbour = false;
		for(%i = 0; %i < 4; %i++)
		{	
			%z = TerritoryZone_find(%zone.connection[%i]);
			if(isObject(%z))
			{
				%zone.zNeighbour[%i] = %z;
				%zone.zHasNeighbour = true;
			}
			else
				%zone.zNeighbour[%i] = -1;
		}
	
		if( %zone.initialOwner != 0 )
			%this.setZoneOwner(%zone, %zone.initialOwner);
		else
			%this.setZoneOwner(%zone, 0);
	
		%zone.zProtected = %zone.initiallyProtected;
	
		%this.updateOwner(%zone);
	}
}


//-----------------------------------------------------------------------------
// AirfieldZone

datablock TacticalZoneData(AirfieldZone)
{
	category = "Tactical Zones"; // for the mission editor

	tickPeriodMS = 250;
	neutralColor			 = "0.0 0.5 0.0 0.3";
};

function AirfieldZone::onEnter(%this,%zone,%obj)
{
	echo("entered airfield-tactical zone");

	if(%obj.getDataBlock().isAircraft)
		%obj.setHoverMode();
}

function AirfieldZone::onLeave(%this,%zone,%obj)
{
	echo("left airfield-tactical zone");
	
	if(%obj.getDataBlock().isAircraft)
		%obj.setFlyMode();
}

function AirfieldZone::onTick(%this,%trigger)
{

}


