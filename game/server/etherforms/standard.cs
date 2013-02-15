//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

datablock MultiNodeLaserBeamData(EthStandard_LaserTrailOne)
{
	hasLine = false;
	lineColor	= "1.00 1.00 0.00 0.7";

	hasInner = true;
	innerColor = "1.0 1.0 1.0 1.0";
	innerWidth = "0.3";

	hasOuter = false;
	outerColor = "0.9 0.9 0.9 0.5";
	outerWidth = "0.6";

	//bitmap = "share/shapes/rotc/vehicles/team1scoutflyer/lasertrail";
	//bitmapWidth = 1;

	blendMode = 1;
	fadeTime = 300;
};

datablock MultiNodeLaserBeamData(EthStandard_LaserTrailTwo)
{
   allowColorization = true;

	hasLine = false;
	lineColor	= "1.00 1.00 0.00 0.7";

	hasInner = false;
	innerColor = "1.0 1.0 1.0 1.0";
	innerWidth = "0.3";

	hasOuter = true;
	outerColor = "0.9 0.9 0.9 0.5";
	outerWidth = "0.6";

	//bitmap = "share/shapes/rotc/vehicles/team1scoutflyer/lasertrail";
	//bitmapWidth = 1;

	blendMode = 1;
	fadeTime = 300;
};

datablock MultiNodeLaserBeamData(EthStandard_LaserTrailThree)
{
   allowColorization = true;

	hasLine = false;
	lineColor	= "0.00 1.00 0.00 0.5";

	hasInner = true;
	innerColor = "0.9 0.9 0.9 0.5";
	innerWidth = "0.3";

	hasOuter = false;
	outerColor = "0.9 0.9 0.9 0.1";
	outerWidth = "0.10";

	//bitmap = "share/shapes/rotc/vehicles/team1scoutflyer/lasertrail";
	//bitmapWidth = 1;

	blendMode = 1;
	fadeTime = 600;
};

datablock ShapeBaseImageData(EthStandard_LightImage)
{
   allowColorization = true;

	// basic item properties
	shapeFile = "share/shapes/rotc/misc/nothing.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 4;
	offset = "0 0 0";

	// light properties...
	lightType = "ConstantLight";
	lightColor = "0.2 0.2 0.2";
	lightTime = 1000;
	lightRadius = 8;
	lightCastsShadows = true;
	lightAffectsShapes = true;

	stateName[0] = "DoNothing";
};

datablock EtherformData(EthStandard)
{
   lightImage = EthStandard_LightImage; // script field

	hudImageNameFriendly = "~/client/ui/hud/pixmaps/teammate.etherform.png";
	hudImageNameEnemy = "~/client/ui/hud/pixmaps/enemy.etherform.png";

	thirdPersonOnly = true;

    //category = "Vehicles"; don't appear in mission editor
	shapeFile = "share/shapes/cat5/etherform.standard.dts";
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
	laserTrail[0] = EthStandard_LaserTrailOne;
	laserTrail[1] = EthStandard_LaserTrailTwo;
	laserTrail[2] = EthStandard_LaserTrailThree;

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
