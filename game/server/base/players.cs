//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Cat5 - players.cs
// code shared by all player datablocks & objects
//------------------------------------------------------------------------------

function executePlayerScripts()
{
	echo(" ----- executing player scripts ----- ");
}

executePlayerScripts();

//-----------------------------------------------------------------------------

// damage applied every 50ms for entering liquid...
$DamageLava		 = 10.0;
$DamageHotLava	 = 10.0;
$DamageCrustyLava = 10.0;
$DamageProtectedMemory = 10.0;

//
// player death animation mapping...
//

$PlayerDeathAnim::TorsoFrontFallForward = 1;
$PlayerDeathAnim::TorsoFrontFallBack = 2;
$PlayerDeathAnim::TorsoBackFallForward = 3;
$PlayerDeathAnim::TorsoLeftSpinDeath = 4;
$PlayerDeathAnim::TorsoRightSpinDeath = 5;
$PlayerDeathAnim::LegsLeftGimp = 6;
$PlayerDeathAnim::LegsRightGimp = 7;
$PlayerDeathAnim::TorsoBackFallForward = 8;
$PlayerDeathAnim::HeadFrontDirect = 9;
$PlayerDeathAnim::HeadBackFallForward = 10;
$PlayerDeathAnim::ExplosionBlowBack = 11;

//
// player shape fx slots...
//

$PlayerShapeFxSlot::GridConnection  = 0;
$PlayerShapeFxSlot::Shield = 1;
$PlayerShapeFxSlot::Barrier = 2;
$PlayerShapeFxSlot::Misc = 3;

//-----------------------------------------------------------------------------

// The mission editor invokes this method when it wants to create
// an object of the given datablock type.  For the mission editor
function PlayerData::create(%data)
{
	if(%data.isBot)
	{
		%obj = new AIPlayer() {
			dataBlock = %data;
		};
	}
	else
	{
		%obj = new Player() {
			dataBlock = %data;
		};
	}

	return %obj;
}

//-----------------------------------------------------------------------------

// callback function: called by engine
function PlayerData::onAdd(%this,%obj)
{
	Parent::onAdd(%this,%obj);
	
	%client = %obj.client;
	if(!isObject(%client))
		return;

	// Clear right HUD menu
	%obj.client.setHudMenuR("*", " ", 1, 0);

	%client.inventoryMode = "showicon";
	
	%obj.isCAT = true;
	%obj.getTeamObject().numCATs++;

	// Vehicle timeout
	%obj.mountVehicle = true;

	//
	// weapon management...
	//
	%obj.specialWeapon = %obj.client ? %obj.client.specialWeapon : 0;
	if(%obj.getTeamId() == 1)
	{
		%obj.mountImage(RedDiscImage, 1, -1, true);
		if(%client.hasGrenade)
			%obj.mountImage(RedGrenade2Image, 2, -1, true);
	}
	else
	{
		%obj.mountImage(BlueDiscImage, 1, -1, true);
		if(%client.hasGrenade)
			%obj.mountImage(BlueGrenade2Image, 2, -1, true);
	}
	%obj.useWeapon(1);

	//
	// disc management...
	//
	%obj.setDiscs(0);
	%obj.attackingDiscs = 0;  // Number of discs that are attacking the player

	//
	// grenade management...
	//
	%obj.setGrenadeAmmo(1);
    %obj.setGrenadeAmmoDt(0.125);

	%obj.updateRegeneration();
	%obj.updateGridConnection();
	%obj.updateAccuracy();
	%obj.updatePose();

   %obj.nonSnipingBodyPose = $PlayerBodyPose::Marching;
	%obj.setBodyPose(%obj.nonSnipingBodyPose);
	if(%client.hasPermaboard)
		%obj.setBodyPose($PlayerBodyPose::Sliding);
  
   // No more limited sliding for now.
   // %obj.sliding = 0.5;
   // %obj.updateSliding();

	if($Server::NewbieHelp && isObject(%obj.client))
	{
		%client = %obj.client;

		if(!%client.newbieHelpData_HasManifested)
		{
			%client.setNewbieHelp("You are now in CAT form. You can switch back to" SPC
				"etherform at any time by pressing @bind34 again.", 40);
		}		
		else
		{
			%client.setNewbieHelp("random", true);
		}
			
		%client.newbieHelpData_HasManifested = true;			
			
		if(%client.newbieHelpData_NeedsRepair)
			%client.newbieHelpData_HasRepaired = true;			
	}
}

// callback function: called by engine
function PlayerData::onRemove(%this, %obj)
{
	Parent::onRemove(%this,%obj);

	if(%obj.isCAT)
		%obj.getTeamObject().numCATs--;

	if(%obj.client.player == %obj)
		%obj.client.player = 0;
		
	checkRoundEnd();
}

// callback function: called by engine
function PlayerData::onNewDataBlock(%this,%obj)
{
	Parent::onNewDataBlock(%this,%obj);
}


//----------------------------------------------------------------------------

// callback function: called by engine when player get's mounted
function PlayerData::onMount(%this,%obj,%vehicle,%node)
{
//	CommandToClient(%obj.client, 'PopActionMap', moveMap);
//	CommandToClient(%obj.client, 'PushActionMap', $Vehicle::moveMaps[%node]);
//	CommandToClient(%obj.client,'HideCommandMenuServer');

//	%obj.setTransform(%vehicle.getDataBlock().mountPointTransform[%node]);
//	%obj.lastWeapon = %obj.getMountedImage($SLOT_WEAPON);
//	%obj.unmountImage($SLOT_WEAPON);

	%vehicledata = %vehicle.getDatablock();

	if(%vehicledata.isAircraft && %node == 0)
		CommandToClient(%obj.client, 'PushActionMap', "AircraftMoveMap");

	%obj.setActionThread(%vehicledata.mountPose[%node],true,true);
	%vehicledata.onPlayerMounted(%vehicle,%obj,%node);
}

// callback function: called by engine when player get's unmounted
function PlayerData::onUnmount( %this, %obj, %vehicle, %node )
{
//	%obj.mountImage(%obj.lastWeapon, $WeaponSlot);
	%vehicledata = %vehicle.getDatablock();

	if(%vehicledata.isAircraft && %node == 0)
		CommandToClient(%obj.client, 'PopActionMap', "AircraftMoveMap");
}

// callback function: called by engine when player triggers
// trigger #2 while being mounted...
function PlayerData::doDismount(%this, %obj, %forced)
{
	// nothing here since we need trigger #2 for other purposes -mag
}

//----------------------------------------------------------------------------

// callback function: called by engine
function PlayerData::onCollision(%this,%obj,%col,%vec,%vecLen)
{
	Parent::onCollision(%this,%obj,%col,%vec,%vecLen);

	if (%obj.getState() $= "Dead")
		return;

	// Try and pickup all items
	if (%col.getClassName() $= "Item")
		%obj.pickup(%col);

	// Mount vehicles
//	%this = %col.getDataBlock();
//	if ((%this.className $= WheeledVehicleData) && %obj.mountVehicle &&
//			%obj.getState() $= "Move" && %col.mountable) {

//		// Only mount drivers for now.
//		%node = 0;
//		%col.mountObject(%obj,%node);
//		%obj.mVehicle = %col;
//	}
}

// callback function: called by engine
function PlayerData::onImpact(%this, %obj, %col, %vec, %vecLen)
{
	%obj.damage(0, VectorAdd(%obj.getPosition(),%vec),
		%vecLen * %this.speedDamageScale, $DamageType::Force);
}


//----------------------------------------------------------------------------

// called by ShapeBase script code...
function PlayerData::getBleed(%this, %obj, %dmg, %src)
{
	if(%dmg > 60)
		return $PlayerBleedHeavy[%obj.getTeamId()];
	else if(%dmg > 30)
		return $PlayerBleedMedium[%obj.getTeamId()];
	else if(%dmg > 15)
		return $PlayerBleedLight[%obj.getTeamId()];
	else if(%dmg > 0)
		return $PlayerBleedSting[%obj.getTeamId()];
	else
	{
		if(isObject(%src) && %src.getDataBlock().bypassDamageBuffer)
			return  $PlayerBleedBarrier[%obj.getTeamId()];
		else
			return $PlayerBleedBuffer[%obj.getTeamId()];
	}
}

function PlayerData::damage(%this, %obj, %sourceObject, %pos, %damage, %damageType)
{
	if (%obj.getState() $= "Dead")
		return;
		
	// if the player is transformed, some damage is ignored and some is
	// redirected the the player's transform-object...
	if(%obj.isTransformed)
	{
		if(%damageType != $DamageType::Splash)
			%obj.transformObj.damage(%sourceObject, %pos, %damage, %damageType);
			
		return;
	}

	%damageDealt = Parent::damage(%this, %obj, %sourceObject, %pos, %damage, %damageType);

	//echo("damageDealt:" SPC %damageDealt);

	// Has the player died?
	%totalDamage = %obj.getDamageLevel();
	if(%totalDamage >= %this.maxDamage)
	{
      if(%obj.getClassName() $= "AiPlayer")
         sMissionCallback_onBotDestroyed(%this, %obj);

      // Stop talking...
      %obj.stopAudio(0);

		// blow up when damage is high...
		//if(%damage > 2 * %this.maxDamage)
      //{
			%obj.setDamageState("Destroyed");
         createExplosion(PumpgunnerDeathExplosion, %obj.getPosition(), "0 0 1");
      //}
		//else
		//	%obj.setDamageState("Disabled");

		%obj.playDeathCry();
		%obj.setDamageFlash(0.75);

		// release the weapon triggers
		%obj.setImageTrigger(0, false);
		%obj.setImageTrigger(1, false);
		
        %obj.getDataBlock().updateZone(%obj, 0);
		%obj.unmountImage(3); // remove light
  
		// detonate a possible teleport grenade
		if(isObject(%obj.teleportGrenade))
		{
			%obj.teleportGrenade.detonate();
			%obj.teleportGrenade = 0;
		}
		
		// deactivate potential regen modules...
		%obj.setRepairRate(0);
		
		// disable shape FXs
		%obj.shapeFxSetActive(0, false, false);
		%obj.shapeFxSetActive(1, false, false);
		%obj.shapeFxSetActive(2, false, false);
  
        // play clown sound if the player was humiliated...
        // DON'T DO THIS, CL'OWNING HAPPENS WAY TO OFTEN SO THIS
        // BECOMES ANNOYING REALLY QUICKLY
        //if(%sourceObject.getClassName() $= "NortDisc")
        //{
        //    serverPlay2D(ClownSound);
        //	MessageAll('MsgDiscKill', '\c2%1 was clowned by \c2%2.',
        //		%obj.client.name, %sourceObject.client.name);
        //}
	}

	%location = "Body";

	// Deal with client callbacks here because we don't have this
	// information in the onDamage or onDisable methods
	%client = %obj.client;
	%sourceClient = %sourceObject ? %sourceObject.client : 0;
	
	// player died?
	if(%obj.getState() $= "Dead")
	{
		%damageLoc = %obj.getDamageLocation(%pos);
		%obj.playDeathAnimation(%damageLoc, %damageType);
	
		if(%client)
		{
			//%client.onDeath(%sourceObject, %sourceClient, %damageType, %location);
			%client.showReceivedDamageInfo(true);
			%client.togglePlayerForm();
		}
		else if(%obj.getControllingClient())
		{
			%obj.getControllingClient().toggleFullControl(false);
		}
	}
}

// *** Callback function:
// Invoked by ShapeBase code whenever the object's damage level changes
function PlayerData::onDamage(%this, %obj, %delta)
{
	Parent::onDamage(%this, %obj, %delta);

	// [mag] moved most stuff into PlayerData::damage()
	
	if(%obj.getState() !$= "Dead")
	{
		// Damage flash
		if(isObject(%obj.client))
		{
			%client = %obj.client;
			if(%client.damageScreenMode == 1)
			{
				%time = getSimTime();
				if(%client.dmgFlashTime !$= "")
				{
					%elapsed = %time - %client.dmgFlashTime;
					%client.dmgFlash -= %elapsed / 10000;
					if(%client.dmgFlash < 0)
						%client.dmgFlash = 0;
				}
				%client.dmgFlash += (%delta / %this.maxDamage)*2;
				if(%client.dmgFlash > 1)
					%client.dmgFlash = 1;
				else if(%client.dmgFlash < 0)
					%client.dmgFlash = 0;
				%client.setHudBackground(
					1,
					"share/textures/cat5/screen.damage",
					"255 0 0",
					false,
					%client.dmgFlash * 255,
					-10
				);
				%client.dmgFlashTime = %time;
			}
		}

		// If the pain is excessive, let's hear about it.
		if (%delta > 10)
			%obj.playPain();
	}
}

function PlayerData::updateShieldFx(%this, %obj)
{
	%shieldVal = %obj.getDamageBufferLevel() / %this.damageBuffer;

	if(%shieldVal > 1)
	{
		%time = (%shieldVal - 1) * 5000;
		%texture = 3;
		%fadeDt = 0;
	}
	else
	{
		%time = (1 - %shieldVal) * 5000;
		%texture = 4;
		%fadeDt = 1/%time + 0.05;
	}

	%slot = $PlayerShapeFxSlot::Shield;
	%obj.shapeFxSetTexture(%slot, %texture);
	%obj.shapeFxSetColor(%slot, 0);
	%obj.shapeFxSetBalloon(%slot, 1.4, 0);
	%obj.shapeFxSetFade(%slot, %shieldVal, %fadeDt);
  	%obj.shapeFxSetActive(%slot, true, false);
	cancel(%obj.shieldFxThread);
	%obj.shieldFxThread = %obj.schedule(%time, "shapeFxSetActive", %slot, false, false);
}


// *** Callback function:
// Invoked by ShapeBase code when object's damageState was set to 'Disabled'
function PlayerData::onDisabled(%this,%obj,%prevState)
{
	if(%prevState $= "Enabled")
	{
		// schedule corpse removal to keep the place clean
		//%obj.setOverrideTexture("base/data/textures/deathTexture"); [mag] TODO: re-implement this?
		%obj.startFade(4000, 0, true);
		%obj.schedule(4500, "delete");
	}
}

// *** Callback function:
// Invoked by ShapeBase code when object's damageState was set to 'Destroyed'
function PlayerData::onDestroyed(%this, %obj, %prevState)
{
	if(%prevState $= "Enabled")
	{
		// delete the player object after it had time to send
		// some important updates over the net...
		%obj.schedule(250, "delete");
	}
}

//-----------------------------------------------------------------------------

function PlayerData::onEnterLiquid(%this, %obj, %coverage, %type)
{
    // CATs don't like water ;-)
    %obj.kill();

//	switch(%type)
//	{
//		case 0: //Water
//		case 1: //Ocean Water
//		case 2: //River Water
//		case 3: //Stagnant Water
//		case 4: //Lava
//			%obj.setDamageDt($DamageLava, "Lava");
//		case 5: //Hot Lava
//			%obj.setDamageDt($DamageHotLava, "Lava");
//		case 6: //Crusty Lava
//			%obj.setDamageDt($DamageCrustyLava, "Lava");
//		case 7: //Quick Sand
//		case 8: //Protected Memory
//			%obj.setDamageDt($DamageProtectedMemory, "ProtectedMemory");
//	}
}

//-----------------------------------------------------------------------------

function PlayerData::onEndSequence(%this, %obj, %slot)
{
	//echo("slot" SPC %slot SPC "finished playing");
	
	if(%slot == 0)
	{
		%obj.removeThread(0);
		%obj.setArmThread(%obj.getMountedImage(%slot).armThread);
	}
}

//-----------------------------------------------------------------------------

function PlayerData::onTrigger(%this, %obj, %triggerNum, %val)
{
	// This method is invoked when the player receives a trigger
	// move event.  The player automatically triggers slot 0 and
	// slot 1 off of triggers # 0 & 1.  Trigger # 2 is also used
	// as the jump key.

	if(!%obj.client)
		return;
	
	//--------------------------------------------------------------------------
	// Primary fire
	//--------------------------------------------------------------------------
	if( %triggerNum == 0 )
	{

	}

	//--------------------------------------------------------------------------
	// Jump
	//--------------------------------------------------------------------------
	if( %triggerNum == 2 && %obj.isCAT )
	{
        if(%val)
        {
            %obj.schedule(0, "checkReJump");
        }
	}
	
	//--------------------------------------------------------------------------
	// Grenade
	//--------------------------------------------------------------------------
	if( %triggerNum == 3 )
	{
		%obj.setImageTrigger(2, %val);
	}

	//--------------------------------------------------------------------------
	// Marching & Sliding
	//--------------------------------------------------------------------------
	if( %triggerNum == 4 || %triggerNum == 5 )
	{
		if(%obj.client.hasPermaboard)
			return;

		if(%val)
      {
         if(%triggerNum == 4 && %obj.getEnergyLevel() > 20)
    			%obj.nonSnipingBodyPose = $PlayerBodyPose::Standard;
         else if(%triggerNum == 5 && %obj.client.hasEtherboard)
            %obj.nonSnipingBodyPose = $PlayerBodyPose::Sliding;
      }
		else
			%obj.nonSnipingBodyPose = $PlayerBodyPose::Marching;
 
      if(!%obj.isSniping)
         %obj.setBodyPose(%obj.nonSnipingBodyPose);
	}
}

//-----------------------------------------------------------------------------

function Player::setSniping(%this, %sniping)
{
	if(!isObject(%this.client))
		return;

	if(%this.client.hasPermaboard)
		return;

    %this.isSniping = %sniping;
    if(%this.isSniping)
        %this.setBodyPose($PlayerBodyPose::Crouched);
    else
        %this.setBodyPose(%this.nonSnipingBodyPose);
}

//-----------------------------------------------------------------------------

function Player::updateSliding(%this)
{
    %dtTime = 50;
    
    //%warnLevel = 0.15;
    //%hotLevel = 0.5;

    %slidingingDt = -0.01;
    %coolingDt = 0.02;
    %cooldownDelay = 1.0;

    if(%this.slidingThread !$= "")
        cancel(%this.slidingThread);
        
    %store   = %this.sliding;
    %storeDt = %this.slidingDt;

    if(%this.getBodyPose() == $PlayerBodyPose::Sliding)
    {
        %this.slidingDt = %slidingingDt;
        %this.slidingCooldownTime = %cooldownDelay;
    }
    else
    {
        %this.slidingCooldownTime -= %dtTime / 1000;
        if(%this.slidingCooldownTime >= 0)
            %this.slidingDt = 0;
        else
            %this.slidingDt = %coolingDt;
    }
    
    %this.sliding += %this.slidingDt;
    if(%this.sliding > 1)
    {
        %this.sliding = 1;
        %this.slidingDt = 0;
    }
    else if(%this.sliding <= 0)
    {
        %this.sliding = 0;
        %this.slidingDt = 0;
        %this.setBodyPose($PlayerBodyPose::Normal);
    }
    
    if(%this.client && %this.slidingDt !$= %storeDt)
       	messageClient(%this.client, 'MsgHeat', "", %this.sliding, %this.slidingDt);

    %this.slidingThread = %this.schedule(%dtTime, "updateSliding");
}

//-----------------------------------------------------------------------------

function Player::updateRegeneration(%this)
{
	%dtTime = 50;

	if(%this.updateRegenerationThread !$= "")
		cancel(%this.updateRegenerationThread);
        
	%this.updateRegenerationThread = %this.schedule(%dtTime, "updateRegeneration");
	
	%regenerate = false;
	
	%client = %this.client;
	if(isObject(%client))
		if(%client.numRegenerators > 0 && VectorLen(%this.getVelocity()) < 0.1)
			%regenerate = true;
			
	if(%regenerate)
		%this.setRepairRate(0.1 * mPow(2,%client.numRegenerators-1));		
	else
		%this.setRepairRate(0);		
}

//-----------------------------------------------------------------------------

function Player::setGridConnection(%this, %val)
{
	if(%this.client.hasStabilizer || !%this.client.hasAnchor)
		return;

	if(%val == %this.gridConnection)
		return;

	%this.gridConnection = %val;
					
	%this.shapeFxSetTexture($PlayerShapeFxSlot::GridConnection, 0);					
	%this.shapeFxSetColor($PlayerShapeFxSlot::GridConnection, 1);			
	%this.shapeFxSetFade($PlayerShapeFxSlot::GridConnection, %val, %this.gridConnectionDt * 20);
}

function Player::updateGridConnection(%this)
{
	if(!isObject(%this.client))
		return;

	if(%this.client.hasStabilizer)
	{
		%this.gridConnection = 1;
		%this.shapeFxSetTexture($PlayerShapeFxSlot::GridConnection, 0);					
		%this.shapeFxSetColor($PlayerShapeFxSlot::GridConnection, 1);			
		%this.shapeFxSetFade($PlayerShapeFxSlot::GridConnection, 1, 0);
		%this.shapeFxSetActive($PlayerShapeFxSlot::GridConnection, true, true);
		return;
	}

	if(!%this.client.hasAnchor)
		return;

	%dtTime = 50;
    
	if(%this.updateGridConnectionThread !$= "")
		cancel(%this.updateGridConnectionThread);
        
	%this.updateGridConnectionThread = %this.schedule(%dtTime, "updateGridConnection");
	
	if(%this.getDamageState() !$= "enabled")
		return;
	
	%updateFx = false;
    
	if(!isObject(%this.zCurrentZone))
	{
		%this.gridConnection = 0;
		%this.shapeFxSetActive($PlayerShapeFxSlot::GridConnection, false, false);
		return;
	}      
	
	if(%this.getBodyPose() == $PlayerBodyPose::Sliding)
	{
		%contact = false;
	}
	else
	{
		%start = %this.getWorldBoxCenter();
		%end	= VectorAdd(%start, "0 0 -3");	
		%contact = containerRayCast(%start, %end, $TypeMasks::TerrainObjectType |
			$TypeMasks::InteriorObjectType , %this);
	}
	
	%speed = VectorLen(%this.getVelocity());
	//%this.client.setHudMenuR(0, %speed, 1, 1);
	

	if(%contact)
	{
		%newDt = 0.010;
	}
	else
	{
		%newDt = -0.008;
	}

	// FIXME: in the linux version %newDt - %this.gridConnectionDt is not exactly
    //        0 even if %newDt and %this.gridConnectionDt are the same number
	//echo(%newDt SPC %this.gridConnectionDt SPC %newDt - %this.gridConnectionDt);
	if(mAbs(%newDt-%this.gridConnectionDt) > 0.001)
	{
		%this.gridConnectionDt = %newDt;
		%updateFx = true;		
	}

	%this.gridConnection += %this.gridConnectionDt;
	if(%this.gridConnection > 1)
	{
		%this.gridConnection = 1;
	}
	else if(%this.gridConnection < 0)
	{
		%this.gridConnection = 0;
	}
	
	//%this.client.setHudMenuR(1, %this.gridConnection, 1, 1);

	// Zone changed?
	if(%this.lastZoneBlocked != %this.zCurrentZone.zBlocked ||
	   %this.lastZoneTeamId != %this.zCurrentZone.teamId)
	{
		%updateFx = true;
	}
	%this.lastZoneBlocked = %this.zCurrentZone.zBlocked;	
	%this.lastZoneTeamId = %this.zCurrentZone.teamId;	
	
	// Update shape FX?
	if(%updateFx || %this.forceGridConnectionShapeFxUpdate)
	{
		//error("Player::updateGridConnection(): Updating Shape FX");	
		%this.forceGridConnectionShapeFxUpdate = false;
		if(%this.gridConnection < 0)
		{
			%this.shapeFxSetTexture($PlayerShapeFxSlot::GridConnection, 7);							
			%this.shapeFxSetFade($PlayerShapeFxSlot::GridConnection, 1, -3);					
		}
		else
		{
//			if(!%this.zCurrentZone.zHasNeighbour)
//			{
//				%color = 6;
//			}		
//			else if(%this.zCurrentZone.getTeamId() == 0)
//			{
//				%color = 0;
//			}
//			else if(%this.zCurrentZone.getTeamId() == %this.getTeamId())
//			{
//				if(%this.zCurrentZone.zBlocked)
//					%color = 2;
//				else
//					%color = 1;
//			}
//			else
//			{
//				%color = 3;
//			}
			%color = 1;						
			%this.shapeFxSetTexture($PlayerShapeFxSlot::GridConnection, 0);					
			%this.shapeFxSetColor($PlayerShapeFxSlot::GridConnection, %color);			
			%this.shapeFxSetFade($PlayerShapeFxSlot::GridConnection, %this.gridConnection, %this.gridConnectionDt * 20);					
		}
				
		%this.shapeFxSetBalloon($PlayerShapeFxSlot::GridConnection, 1.00, 0);	
		%this.shapeFxSetActive($PlayerShapeFxSlot::GridConnection, true, true);   	
	}	
}

//-----------------------------------------------------------------------------

function Player::updateAccuracy(%this)
{
	// TODO: move this into mounted image code

	if(!isObject(%this.client))
		return;

	%dtTime = 32;

	if(%this.updateAccuracyThread !$= "")
		cancel(%this.updateAccuracyThread);

	%this.updateAccuracyThread = %this.schedule(%dtTime, "updateAccuracy");

	if(%this.getDamageState() !$= "enabled")
		return;

   %this.firingInaccuracy--;

   if(%this.firingInaccuracy < 0)
      %this.firingInaccuracy = 0;

   %image = %this.getMountedImage(0);
   if(false) //isObject(%image) && %image.isMethod("getBulletSpread"))
   {
   	%spread = %image.getBulletSpread(%this);
      %mod = 2-(%this.getCameraFov()/80);
      %mod = mPow(%mod,mPow(%mod,%mod));
      %this.eCrosshairSpread = %spread * %mod;
   }
   else
      %this.eCrosshairSpread = 0;
}

//-----------------------------------------------------------------------------

function Player::updatePose(%this)
{
   // Infantry implements this
}

//-----------------------------------------------------------------------------

function Player::numAttackingDiscs(%this)
{
    return %this.attackingDiscs;
}

function Player::addAttackingDisc(%this, %disc)
{
    %this.attackingDiscs += 1;
//	%this.activateBarrier(0);
}

function Player::removeAttackingDisc(%this, %disc)
{
    %this.attackingDiscs -= 1;
}

//-----------------------------------------------------------------------------

function Player::hasBarrier(%this)
{
    return %this.barrier != 0;
}

function Player::activateBarrier(%this, %time)
{
    if(%this.deactivateBarrierThread !$= "")
        cancel(%this.deactivateBarrierThread);

    %this.barrier = 1;
    %this.shapeFxSetActive($PlayerShapeFxSlot::Barrier, true, true);
    %this.shapeFxSetTexture($PlayerShapeFxSlot::Barrier, 2);
	%this.shapeFxSetBalloon($PlayerShapeFxSlot::Barrier, 1, 0);
	if(%time == 0)
	{
    	%this.shapeFxSetFade($PlayerShapeFxSlot::Barrier, 1.0, 0);
		%this.deactivateBarrierThread = "";
	}
	else
	{
    	%this.shapeFxSetFade($PlayerShapeFxSlot::Barrier, 1.0, (-1/%time) + 0.05);
		%this.deactivateBarrierThread =
		   %this.schedule(%time*1000, "deactivateBarrier");
	}
}

function Player::deactivateBarrier(%this)
{
    if(%this.deactivateBarrierThread !$= "")
	{
        cancel(%this.deactivateBarrierThread);
		%this.deactivateBarrierThread = "";
	}

    %this.barrier = 0;

	//%this.shapeFxSetActive($PlayerShapeFxSlot::Barrier, false, false);

	%this.shapeFxSetTexture($PlayerShapeFxSlot::Barrier, 0);
	%this.shapeFxSetBalloon($PlayerShapeFxSlot::Barrier, 1, 0);
	%this.shapeFxSetFade($PlayerShapeFxSlot::Barrier, 1.0, -1/1);
    %this.shapeFxSetActive($PlayerShapeFxSlot::Barrier, true, true);
}

//-----------------------------------------------------------------------------

function Player::checkReJump(%this)
{
    %this.getDataBlock().checkReJump(%this);
}

function PlayerData::checkReJump(%this, %obj)
{
   return;
    if(%obj.lastJumpTime != getSimTime() && %obj.getEnergyLevel() > %this.reJumpEnergyDrain)
    {
       	createExplosion(CatJumpExplosion, %obj.getPosition(), "0 0 1");
  		%impulseVec = VectorScale("0 0 1", %this.reJumpForce);
  		%obj.applyImpulse(%pos, %impulseVec);
        %obj.setEnergyLevel( %obj.getEnergyLevel() -  %this.reJumpEnergyDrain);
    }
}

//-----------------------------------------------------------------------------

function PlayerData::onJump(%this, %obj)
{
    %obj.lastJumpTime = getSimTime();
}

//-----------------------------------------------------------------------------

function PlayerData::transformInto(%this,%obj,%into)
{
	%client = %obj.client;
	%success = false;

	//
	// transform into flyer
	//
	if( %into == 1 )
	{
		// need some energy in order to transform...
		if( %obj.getEnergyLevel() < 75 )
			return false;

		%pos = %obj.getTransform();
		%vel = %obj.getVelocity();
		%dmg = %obj.getDamageLevel();

		//%obj.setInvisible(true);

		%data = RedFlyer;

		%newObj = new FlyingVehicle()
		{
			datablock = %data;
			client = %client;
			teamId = %client.team.teamId;
		};

		%effect = new FXShape()
		{
			datablock = Team1ScoutToFlyerEffect;
		};
		%effect.linkToObject(%newObj);

		%newObj.setEnergyLevel($PLAYER_MAX_ENERGY/2);
		%newObj.setDamageLevel(%dmg);
		%newObj.setShapeName(%client.name);

//		$aiTarget = %newObj;

		%newObj.setTransform(%pos);
		%newObj.applyImpulse(%pos, VectorScale(%vel,100));

		MissionCleanup.add(%newObj);

		%obj.setControlObject(%newObj);
//		%client.player = %newObj;

		// create a cool(tm) effect...
		%newObj.startFade(100,900,false);
		%effect.schedule(1000,"delete");
		%newObj.playAudio(0,TransformSound);

		%success = true;
	}

	return %success;
}

//-----------------------------------------------------------------------------
// Player object methods
//-----------------------------------------------------------------------------

function Player::hasDisc(%this)
{
	return %this.numDiscs > 0;
}

function Player::setDiscs(%this, %numDiscs)
{
	%this.numDiscs = %numDiscs;

	%hasDisc = %this.hasDisc();
	%this.setImageAmmo(1, %hasDisc);

	if(isObject(%this.client))
   {
      %this.client.inventoryMode = "hidden";
		%this.client.displayInventory(%this);
   }
}

function Player::incDiscs(%this)
{
	%this.numDiscs++;
	%this.setDiscs(%this.numDiscs);
}

function Player::decDiscs(%this)
{
	%this.numDiscs--;
	%this.setDiscs(%this.numDiscs);
}

function Player::hasGrenade(%this)
{
	return %this.grenadeAmmo >= 1.0;
}

function Player::incGrenadeAmmo(%this, %amount)
{
    %this.setGrenadeAmmo(%this.grenadeAmmo + %amount);
}

function Player::decGrenadeAmmo(%this, %amount)
{
    %this.setGrenadeAmmo(%this.grenadeAmmo - %amount);
}

function Player::setGrenadeAmmo(%this, %amount)
{
    %store = %this.grenadeAmmo;

    if(%amount < 0)
        %this.grenadeAmmo = 0;
    else if(%amount > 1)
        %this.grenadeAmmo = 1;
    else
        %this.grenadeAmmo = %amount;

    if(%this.grenadeAmmo != %store)
    {
        if(isObject(%this.client))
            messageClient(%this.client, 'MsgGrenadeAmmo', "", %this.grenadeAmmo);
    }
    
	%this.setImageAmmo(2, %this.hasGrenade());
}

function Player::setGrenadeAmmoDt(%this, %amount)
{
    %this.grenadeAmmoDt = %amount;

    if(isObject(%this.client))
        messageClient(%this.client, 'MsgGrenadeAmmoDt', "", 250, %amount);

    %this.updateGrenadeAmmo();
}

function Player::updateGrenadeAmmo(%this)
{
	if(%this.grenadeAmmoThread !$= "")
    {
        cancel(%this.grenadeAmmoThread);
		%this.grenadeAmmoThread = "";
	}
 
    %this.grenadeAmmo += %this.grenadeAmmoDt/1000 * 250;
    
    if(%this.grenadeAmmo < 0)
        %this.grenadeAmmo = 0;
    else if(%this.grenadeAmmo > 1)
        %this.grenadeAmmo = 1;
                
	if(%this.getMountedImage(0).usesGrenadeAmmo)
		%this.setImageAmmo(0, %this.grenadeAmmo >= %this.getMountedImage(0).minGrenadeAmmo);        

	%this.setImageAmmo(2, %this.hasGrenade());
 
    %this.grenadeAmmoThread = %this.schedule(250, "updateGrenadeAmmo");
}

function Player::mountVehicle(%this, %vehicle)
{
	%vehicleData = %vehicle.getDataBlock();

	// Is the vehicle mountable?
	if(%vehicleData.mountable == false)
	{
		echo("Sorry, the vehicle is not mountable.");
		return;
	}

	// Check the speed of the vehicle. If it's moving, we can't mount it.
	// Note there is a threshold that determines if the vehicle is moving.
	%vel = %vehicle.getVelocity();
	%speed = vectorLen(%vel);

	if(%speed <= %vehicleData.maxMountSpeed)
	{
		// Find an empty seat
		%seat = %vehicle.findEmptySeat();
		if(%seat == -1)
		{
			echo("No free seat available");
			return;
		}

		%this.mVehicle = %col;
		%this.mSeat = %seat;
		%this.isMounted = true;

		echo("Mounting vehicle in seat " @ %seat);

		// Now mount the vehicle.
		%vehicle.mountObject(%this, %seat);
		%this.setTransform(%vehicle.getSlotTransform(%seat));
	}
	else
	{
		echo("You cannot mount a moving vehicle.");
	}
}

function Player::dismountVehicle(%this)
{
	if(!%this.isMounted())
		return;

	%vehicle = %this.getObjectMount();

	%speed = vectorLen(%vehicle.getVelocity());
	if(%speed > %vehicle.getDataBlock().maxDismountSpeed)
		return;

	// Find the position above dismount point.
	%pos	 = getWords(%this.getTransform(), 0, 2);
	%oldPos = %pos;
	%vec[0] = " 2  0  0";
	%vec[1] = " 0  -2  0";
	%vec[2] = " 0  2  0";
	%vec[3] = "-2  0  0";
	%vec[4] = " 4  0  0";
	%impulseVec  = "0 0 0";
	%vec[0] = MatrixMulVector( %this.getTransform(), %vec[0]);

	// Make sure the point is valid
	%pos = "0 0 0";
	%numAttempts = 5;
	%success	  = -1;
	for (%i = 0; %i < %numAttempts; %i++) {
		%pos = VectorAdd(%oldPos, VectorScale(%vec[%i], 3));
		if(%this.checkDismountPoint(%oldPos, %pos)) {
			%success = %i;
			%impulseVec = %vec[%i];
			break;
		}
	}

	if(%forced && %success == -1)
		%pos = %oldPos;

	%this.unmount();
	%this.mountVehicle = false;

	// Schedule the function to set the mount flag, so that the player
	// can mount another vehicle in the future.
	%this.schedule(4000, "MountVehicles", true);

	// Position above dismount point
	%this.setTransform(%pos);
	%this.applyImpulse(%pos, VectorScale(%impulseVec, %this.getDataBlock().mass));

	%this.setActionThread("run",true,true);
	%this.setArmThread("look");

	%this.client.setControlObject(%this);

	// Command the client to display the correct movement map and
	// activate the command menu.
//	CommandToClient(%obj.client, 'PopActionMap', $Vehicle::moveMaps[%obj.mSeat]);
//	CommandToClient(%obj.client, 'PushActionMap', moveMap);
//	CommandToClient(%obj.client,'activateCommandMenu');
}

function Player::kill(%this, %damageType)
{
	%this.damage(0, %this.getPosition(), 10000, %damageType);
}

//----------------------------------------------------------------------------

function Player::isPilot(%this)
{
	%vehicle = %this.getObjectMount();
	// There are two "if" statements to avoid a script warning.
	if (%vehicle)
		if (%vehicle.getMountNodeObject(0) == %this)
			return true;
	return false;
}

//----------------------------------------------------------------------------

function Player::playDeathCry( %this )
{
	%client = %this.client;
	//playTargetAudio( %client.target, "replaceme", AudioClosest3d, false );
}

function Player::playPain( %this )
{
	%client = %this.client;
	//playTargetAudio( %client.target, "replaceme", AudioClosest3d, false);
}




//----------------------------------------------------------------------------

function Player::kill(%this, %damageType)
{
	%this.damage(0, %this.getPosition(), 10000, %damageType);
}


//----------------------------------------------------------------------------

function Player::mountVehicles(%this,%bool)
{
	// If set to false, this variable disables vehicle mounting.
	%this.mountVehicle = %bool;
}

function Player::isPilot(%this)
{
	%vehicle = %this.getObjectMount();
	// There are two "if" statements to avoid a script warning.
	if (%vehicle)
		if (%vehicle.getMountNodeObject(0) == %this)
			return true;
	return false;
}


//----------------------------------------------------------------------------

function Player::playDeathAnimation(%this, %damageLocation, %damageType)
{
	%vertPos =  getWord(%damageLocation, 0);
	%quadrant = getWord(%damageLocation, 1);

	//echo("damagetype: " @ %damageType);
	//echo("location: " @ %damageLocation);
	//echo("vert Pos: " @ %vertPos);
	//echo("quad: " @ %quadrant);

	if( %damageType == $DamageType::Splash )
	{
		if(%quadrant $= "front_left" || %quadrant $= "front_right")
			%curDie = $PlayerDeathAnim::ExplosionBlowBack;
		else
			%curDie = $PlayerDeathAnim::TorsoBackFallForward;
	}
	else if(%vertPos $= "head")
	{
		if(%quadrant $= "front_left" ||  %quadrant $= "front_right" )
			%curDie = $PlayerDeathAnim::HeadFrontDirect;
		else
			%curDie = $PlayerDeathAnim::HeadBackFallForward;
	}
	else if(%vertPos $= "torso")
	{
		if(%quadrant $= "front_left" )
			%curDie = $PlayerDeathAnim::TorsoLeftSpinDeath;
		else if(%quadrant $= "front_right")
			%curDie = $PlayerDeathAnim::TorsoRightSpinDeath;
		else if(%quadrant $= "back_left" )
			%curDie = $PlayerDeathAnim::TorsoBackFallForward;
		else if(%quadrant $= "back_right")
			%curDie = $PlayerDeathAnim::TorsoBackFallForward;
	}
	else if (%vertPos $= "legs")
	{
		if(%quadrant $= "front_left" ||  %quadrant $= "back_left")
			%curDie = $PlayerDeathAnim::LegsLeftGimp;
		if(%quadrant $= "front_right" || %quadrant $= "back_right")
			%curDie = $PlayerDeathAnim::LegsRightGimp;
	}

	if(%curDie $= "" || %curDie < 1 || %curDie > 11)
		%curDie = 1;

	%this.setArmThread("look");
	%this.setActionThread("Death" @ %curDie);
}

function Player::playPartyAnimation(%this,%key)
{
	if (%this.getState() !$= "Dead")
	{
		%idx = $PlayerPartyAnims.getIndexFromKey(%key);
		%thread = $PlayerPartyAnims.getValue(%idx);
		%idx = $PlayerPartySounds.getIndexFromKey(%key);
		%sound = $PlayerPartySounds.getValue(%idx);
		%this.setActionThread(%thread);
		%this.playAudio(1,%sound);
	}
}

//----------------------------------------------------------------------------

function Player::playDeathCry( %this )
{
	%client = %this.client;
	//playTargetAudio( %client.target, "replaceme", AudioClosest3d, false );
}

function Player::playPain( %this )
{
	%client = %this.client;
	//playTargetAudio( %client.target, "replaceme", AudioClosest3d, false);
}

//----------------------------------------------------------------------------

