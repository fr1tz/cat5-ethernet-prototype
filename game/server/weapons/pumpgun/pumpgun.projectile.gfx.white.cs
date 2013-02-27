//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// laser trail

datablock MultiNodeLaserBeamData(WhitePumpgunProjectileLaserTrail)
{
   allowColorization = true;

	hasLine = true;
	lineColor	= "0.90 0.90 0.90 0.5";

	hasInner = true;
	innerColor = "0.90 0.90 0.90 0.5";
	innerWidth = "0.10";

	hasOuter = false;
	outerColor = "1.00 0.00 0.00 1.0";
	outerWidth = "0.20";

	//bitmap = "share/shapes/rotc/weapons/blaster/lasertrail.red";
	//bitmapWidth = 0.70;

	blendMode = 1;
	renderMode = $MultiNodeLaserBeamRenderMode::FaceViewer;
//	fadeTime = 100*20;
	fadeTime = 2000;
    
	windCoefficient = 0.0;
    
	// node x movement...
	nodeMoveMode[0]     = $NodeMoveMode::DynamicSpeed;
	nodeMoveSpeed[0]    = 6.0;
	nodeMoveSpeedAdd[0] = -12.0;
	// node y movement...
	nodeMoveMode[1]     = $NodeMoveMode::DynamicSpeed;
	nodeMoveSpeed[1]    = 6.0;
	nodeMoveSpeedAdd[1] = -12.0;
	// node z movement...
	nodeMoveMode[2]     = $NodeMoveMode::DynamicSpeed;
	nodeMoveSpeed[2]    = 6.0;
	nodeMoveSpeedAdd[2] = -12.0;
    
    nodeDistance = 5;
};

datablock MultiNodeLaserBeamData(WhitePumpgunProjectileLaserTrailTwo)
{
   allowColorization = true;

	hasLine   = false;
	lineColor = "0.90 0.90 0.90 0.5";
	lineWidth = 2.0;

	hasInner = false;
	innerColor = "0.90 0.90 0.90 0.5";
	innerWidth = "0.5";

	hasOuter = false;
	outerColor = "1.00 0.00 0.00 0.75";
	outerWidth = "0.20";

	bitmap = "share/textures/cat5/smoke2";
	bitmapWidth = 0.75;

	blendMode = 1;
	renderMode = $MultiNodeLaserBeamRenderMode::FaceViewer;
	fadeTime = 1000;

    windCoefficient = 0.0;

    // node x movement...
    nodeMoveMode[0]     = $NodeMoveMode::ConstantSpeed;
    nodeMoveSpeed[0]    = -2.0;
    nodeMoveSpeedAdd[0] =  4.0;
    // node y movement...
    nodeMoveMode[1]     = $NodeMoveMode::ConstantSpeed;
    nodeMoveSpeed[1]    = -2.0;
    nodeMoveSpeedAdd[1] =  4.0;
    // node z movement...
    nodeMoveMode[2]     = $NodeMoveMode::ConstantSpeed;
    nodeMoveSpeed[2]    = -2.0;
    nodeMoveSpeedAdd[2] =  4.0;

    nodeDistance = 1;
};

//-----------------------------------------------------------------------------
// impact...

datablock ParticleData(WhitePumpgunProjectileImpact_Smoke)
{
	dragCoeffiecient	  = 0.4;
	gravityCoefficient	= -0.4;
	inheritedVelFactor	= 0.025;

	lifetimeMS			  = 500;
	lifetimeVarianceMS	= 200;

	useInvAlpha =  true;

	textureName = "share/textures/cat5/smoke1";

	colors[0]	  = "1.0 1.0 1.0 0.5";
	colors[1]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 1.0;
	sizes[1]		= 1.0;
	times[0]		= 0.0;
	times[1]		= 1.0;

	allowLighting = false;
};

datablock ParticleEmitterData(WhitePumpgunProjectileImpact_SmokeEmitter)
{
	ejectionOffset	= 0;

	ejectionPeriodMS = 40;
	periodVarianceMS = 0;

	ejectionVelocity = 2.0;
	velocityVariance = 0.1;

	thetaMin			= 0.0;
	thetaMax			= 60.0;

	lifetimeMS		 = 100;

	particles = "WhitePumpgunProjectileImpact_Smoke";
};

datablock ExplosionData(WhitePumpgunProjectileImpact)
{
	soundProfile = PumpgunProjectileImpactSound;

	lifetimeMS = 3000;
 
 	// shape...
	//explosionShape = "share/shapes/rotc/weapons/blaster/projectile.impact.red.dts";
	//faceViewer = false;
	//playSpeed = 0.4;
	//sizes[0] = "1 1 1";
	//sizes[1] = "1 1 1";
	//times[0] = 0.0;
	//times[1] = 1.0;

	emitter[0] = DefaultSmallWhiteDebrisEmitter;
	emitter[1] = WhitePumpgunProjectileImpact_SmokeEmitter;

	//debris = WhitePumpgunProjectileImpact_Debris;
	//debrisThetaMin = 0;
	//debrisThetaMax = 60;
	//debrisNum = 1;
	//debrisNumVariance = 1;
	//debrisVelocity = 10.0;
	//debrisVelocityVariance = 5.0;

	// Dynamic light
	lightStartRadius = 0;
	lightEndRadius = 0;
	lightStartColor = "1.0 0.0 0.0";
	lightEndColor = "0.0 0.0 0.0";
    lightCastShadows = false;

	shakeCamera = false;
};

//-----------------------------------------------------------------------------
// hit enemy...

datablock ParticleData(WhitePumpgunProjectileHit_Particle)
{
	dragCoefficient    = 0.0;
	windCoefficient    = 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;

	lifetimeMS			  = 500;
	lifetimeVarianceMS	= 0;

	useInvAlpha =  false;

	textureName	= "share/textures/cat5/circle1";

	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "1.0 0.0 0.0 1.0";
	colors[2]	  = "1.0 0.0 0.0 0.0";
	sizes[0]		= 0.5;
	sizes[1]		= 0.5;
	sizes[2]		= 0.5;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

	allowLighting = false;
	renderDot = true;
};

datablock ParticleEmitterData(WhitePumpgunProjectileHit_Emitter)
{
	ejectionOffset	= 0;

	ejectionPeriodMS = 40;
	periodVarianceMS = 0;

	ejectionVelocity = 0.0;
	velocityVariance = 0.0;

	thetaMin			= 0.0;
	thetaMax			= 60.0;

	lifetimeMS		 = 100;

	particles = "WhitePumpgunProjectileHit_Particle";
};

datablock ExplosionData(WhitePumpgunProjectileHit)
{
	soundProfile = PumpgunProjectileHitSound;

	lifetimeMS = 450;

	particleEmitter = WhitePumpgunProjectileHit_Emitter;
	particleDensity = 1;
	particleRadius = 0;

	// Dynamic light
	lightStartRadius = 2;
	lightEndRadius = 0;
	lightStartColor = "1.0 0.0 0.0";
	lightEndColor = "0.0 0.0 0.0";
    lightCastShadows = false;
};

//-----------------------------------------------------------------------------
// missed enemy...

datablock ExplosionData(WhitePumpgunProjectileMissedEnemyEffect)
{
	soundProfile = PumpgunProjectileMissedEnemySound;

	// shape...
	//explosionShape = "share/shapes/rotc/effects/explosion2_white.dts";
	//faceViewer	  = true;
	//playSpeed = 8.0;
	//sizes[0] = "0.07 0.07 0.07";
	//sizes[1] = "0.01 0.01 0.01";
	//times[0] = 0.0;
	//times[1] = 1.0;

	// dynamic light...
	//lightStartRadius = 0;
	//lightEndRadius = 0;
	//lightStartColor = "1 1 1";
	//lightEndColor = "0 0 0";
   //lightCastShadows = false;
};


