//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

exec("./etherform.sfx.cs");
exec("./etherform.gfx.cs");

//-----------------------------------------------------------------------------

function EtherformData::useWeapon(%this, %obj, %nr)
{
	%client = %obj.client.changeInventory(%nr);
}

function EtherformData::damage(%this, %obj, %sourceObject, %pos, %damage, %damageType)
{
	// ignore damage
}

function EtherformData::onAdd(%this, %obj)
{
	Parent::onAdd(%this, %obj);

	// start animation
	%obj.playThread(0, "ambient");

	// start singing...
	%obj.playAudio(1, EtherformSingSound);

	// Make sure grenade ammo bar is not visible...
	messageClient(%obj.client, 'MsgGrenadeAmmo', "", 1);

	// lights...
   %obj.mountImage(%this.lightImage, 3);

	%obj.client.inventoryMode = "show";
	%obj.client.displayInventory();
	
	if($Server::NewbieHelp && isObject(%obj.client))
	{
		%client = %obj.client;
		if(!%client.newbieHelpData_HasManifested)
		{
			%client.setNewbieHelp("You are in etherform! Press @bind34 inside a" SPC 
				(%client.team == $Team1 ? "red" : "blue") SPC "zone to change into CAT form.");
		}
		else if(%client.newbieHelpData_NeedsRepair && !%client.newbieHelpData_HasRepaired)
		{
			%client.setNewbieHelp("If you don't have enough health to change into CAT form," SPC
				"fly into one of your team's zones to regain your health.");
		}		
		else
		{
			%client.setNewbieHelp("random", false);
		}
	}			
}

function EtherformData::onDamage(%this, %obj, %delta)
{
	Parent::onDamage(%this, %obj, %delta);
}

//-----------------------------------------------------------------------------

datablock EtherformData(GreenEtherform)
{
	hudImageNameFriendly = "~/client/ui/hud/pixmaps/teammate.etherform.png";
	hudImageNameEnemy = "~/client/ui/hud/pixmaps/enemy.etherform.png";

	thirdPersonOnly = true;

   //category = "Vehicles"; don't appear in mission editor
   shapeFile = "share/shapes/cat5/etherform.green.dts";
	emap = true;

	cameraDefaultFov = 110.0;
	cameraMinFov     = 110.0;
	cameraMaxFov     = 130.0;
	cameraMinDist    = 2;
	cameraMaxDist    = 3;

	//renderWhenDestroyed = false;
	//explosion = FlyerExplosion;
	//defunctEffect = FlyerDefunctEffect;
	//debris = BomberDebris;
	//debrisShapeName = "share/shapes/rotc/vehicles/bomber/vehicle.dts";

	mass = 40;
	drag = 0.999;
	density = 10;

	maxDamage = 100;
	damageBuffer = 0;
	maxEnergy = 0;

	damageBufferRechargeRate = 0.15;
	damageBufferDischargeRate = 0.05;
	energyRechargeRate = 0.4;

    // collision box...
    boundingBox = "1.0 1.0 1.0";

    // etherform movement...
    accelerationForce = 200;

	// impact damage...
	minImpactSpeed = 1;		// If hit ground at speed above this then it's an impact. Meters/second
	speedDamageScale = 0.0;	// Dynamic field: impact damage multiplier

	// damage info eyecandy...
	damageBufferParticleEmitter = RedEtherformDamageBufferEmitter;
	repairParticleEmitter = GreenEtherformRepairEmitter;
//	bufferRepairParticleEmitter = RedEtherformBufferRepairEmitter;

	// laser trail...
	laserTrail[0] = GreenEtherform_LaserTrailOne;
	laserTrail[1] = GreenEtherform_LaserTrailTwo;

	// contrail...
	minTrailSpeed = 1;
	//particleTrail = RedEtherform_ContrailEmitter;

	// various emitters...
	//forwardJetEmitter = FlyerJetEmitter;
	//downJetEmitter = FlyerJetEmitter;

	//
//	jetSound = Team1ScoutFlyerThrustSound;
//	engineSound = EtherformSound;
	softImpactSound = EtherformImpactSound;
	hardImpactSound = EtherformImpactSound;
	//wheelImpactSound = WheelImpactSound;


};

datablock EtherformData(RedEtherform)
{
	hudImageNameFriendly = "~/client/ui/hud/pixmaps/teammate.etherform.png";
	hudImageNameEnemy = "~/client/ui/hud/pixmaps/enemy.etherform.png";
	
	thirdPersonOnly = true;

    //category = "Vehicles"; don't appear in mission editor
	shapeFile = "share/shapes/rotc/vehicles/etherform/vehicle.red.dts";
	emap = true;
 
	cameraDefaultFov = 110.0;
	cameraMinFov     = 110.0;
	cameraMaxFov     = 130.0;
	cameraMinDist    = 2;
	cameraMaxDist    = 3;
	
	//renderWhenDestroyed = false;
	//explosion = FlyerExplosion;
	//defunctEffect = FlyerDefunctEffect;
	//debris = BomberDebris;
	//debrisShapeName = "share/shapes/rotc/vehicles/bomber/vehicle.dts";

	mass = 40;
	drag = 0.999;
	density = 10;

	maxDamage = 100;
	damageBuffer = 0;
	maxEnergy = 0;

	damageBufferRechargeRate = 0.15;
	damageBufferDischargeRate = 0.05;
	energyRechargeRate = 0.4;
 
    // collision box...
    boundingBox = "1.0 1.0 1.0";
 
    // etherform movement...
    accelerationForce = 200;

	// impact damage...
	minImpactSpeed = 1;		// If hit ground at speed above this then it's an impact. Meters/second
	speedDamageScale = 0.0;	// Dynamic field: impact damage multiplier

	// damage info eyecandy...
	damageBufferParticleEmitter = RedEtherformDamageBufferEmitter;
	repairParticleEmitter = RedEtherformRepairEmitter;
//	bufferRepairParticleEmitter = RedEtherformBufferRepairEmitter;

	// laser trail...
	laserTrail[0] = RedEtherform_LaserTrailOne;
	laserTrail[1] = RedEtherform_LaserTrailTwo;

	// contrail...
	minTrailSpeed = 1;
	//particleTrail = RedEtherform_ContrailEmitter;
	
	// various emitters...
	//forwardJetEmitter = FlyerJetEmitter;
	//downJetEmitter = FlyerJetEmitter;

	//
//	jetSound = Team1ScoutFlyerThrustSound;
//	engineSound = EtherformSound;
	softImpactSound = EtherformImpactSound;
	hardImpactSound = EtherformImpactSound;
	//wheelImpactSound = WheelImpactSound;


};

if($Server::Game.noshield)
	RedEtherform.damageBuffer = 0;

if($Server::Game.lowhealth)
	RedEtherform.maxDamage = 50;

datablock EtherformData(BlueEtherform : RedEtherform)
{
	shapeFile = "share/shapes/rotc/vehicles/etherform/vehicle.blue.dts";
	damageBufferParticleEmitter = BlueEtherformDamageBufferEmitter;
	repairParticleEmitter = BlueEtherformRepairEmitter;
//	bufferRepairParticleEmitter = BlueEtherformBufferRepairEmitter;
	laserTrail[0] = BlueEtherform_LaserTrailOne;
	laserTrail[1] = BlueEtherform_LaserTrailTwo;
	//particleTrail = BlueEtherform_ContrailEmitter;
};

datablock EtherformData(RedNyanEtherform : RedEtherform)
{
	shapeFile = "share/shapes/rotc/vehicles/etherform/vehicle.nyanred.dts";
	laserTrail[0] = NyanEtherform_LaserTrail;
	laserTrail[1] = "";
};

datablock EtherformData(BlueNyanEtherform : BlueEtherform)
{
	shapeFile = "share/shapes/rotc/vehicles/etherform/vehicle.nyanblue.dts";
	laserTrail[0] = NyanEtherform_LaserTrail;
	laserTrail[1] = "";
};


