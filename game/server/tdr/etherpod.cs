//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

datablock EtherformData(Etherpod)
{
	//hudImageNameFriendly = "~/client/ui/hud/pixmaps/teammate.etherform.png";
	//hudImageNameEnemy = "~/client/ui/hud/pixmaps/enemy.etherform.png";
	
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

	mass = 90;
	drag = 0.99;
	density = 10;

	maxDamage = 75;
	damageBuffer = 25;
	maxEnergy = 100;

	damageBufferRechargeRate = 0.15;
	damageBufferDischargeRate = 0.05;
	energyRechargeRate = 0.4;
 
    // collision box...
    boundingBox = "1.0 1.0 1.0";
 
    // etherform movement...
    accelerationForce = 50;

	// impact damage...
	minImpactSpeed = 1;		// If hit ground at speed above this then it's an impact. Meters/second
	speedDamageScale = 0.0;	// Dynamic field: impact damage multiplier

	// damage info eyecandy...
	damageBufferParticleEmitter = RedEtherformDamageBufferEmitter;
	repairParticleEmitter = RedEtherformRepairEmitter;
	bufferRepairParticleEmitter = RedEtherformBufferRepairEmitter;

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