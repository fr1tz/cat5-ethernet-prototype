//------------------------------------------------------------------------------
// Alux
// Copyright (C) 2013 Michael Goldener <mg@wasted.ch>
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// laser tail...

datablock LaserBeamData(WhiteAssaultrifleProjectileLaserTail)
{
   allowColorization = true;

	hasLine = true;
	lineStartColor	= "1.00 1.00 1.00 0.0";
	lineBetweenColor = "1.00 1.00 1.00 0.5";
	lineEndColor	  = "1.00 1.00 1.00 0.5";
	lineWidth		  = 2.0;

	hasInner = true;
	innerStartColor = "1.00 1.00 1.00 0.5";
	innerBetweenColor = "1.00 1.00 1.00 0.5";
	innerEndColor = "1.00 1.00 1.00 0.5";
	innerStartWidth = "0.0";
	innerBetweenWidth = "0.4";
	innerEndWidth = "0.0";

	hasOuter = true;
	outerStartColor = "1.00 1.00 1.00 0.5";
	outerBetweenColor = "1.00 1.00 1.00 0.5";
	outerEndColor = "1.00 1.00 1.00 0.5";
	outerStartWidth = "0.1";
	outerBetweenWidth = "0.5";
	outerEndWidth = "0.1";
	
//	bitmap = "share/shapes/rotc/weapons/blaster/lasertail.red";
//	bitmapWidth = 0.20;
//	crossBitmap = "share/shapes/rotc/weapons/blaster/lasertail.red.cross";
//	crossBitmapWidth = 0.10;

	betweenFactor = 0.9;
	blendMode = 1;
};

//-----------------------------------------------------------------------------
// laser trail

datablock MultiNodeLaserBeamData(WhiteAssaultrifleProjectileLaserTrail)
{
   allowColorization = false;

	hasLine   = false;
	lineColor = "0.90 0.90 0.90 0.5";
	lineWidth = 2.0;

	hasInner = true;
	innerColor = "0.90 0.90 0.90 0.1";
	innerWidth = "0.2";

	hasOuter = false;
	outerColor = "1.00 0.00 0.00 0.75";
	outerWidth = "0.20";

	//bitmap = "share/textures/cat5/smoke2";
	bitmapWidth = 0.2;

	blendMode = 1;
	renderMode = $MultiNodeLaserBeamRenderMode::FaceViewer;
	fadeTime = 1000;

    windCoefficient = 0.0;

    // node x movement...
    nodeMoveMode[0]     = $NodeMoveMode::ConstantSpeed;
    nodeMoveSpeed[0]    = -0.5;
    nodeMoveSpeedAdd[0] =  1.0;
    // node y movement...
    nodeMoveMode[1]     = $NodeMoveMode::ConstantSpeed;
    nodeMoveSpeed[1]    = -0.5;
    nodeMoveSpeedAdd[1] =  1.0;
    // node z movement...
    nodeMoveMode[2]     = $NodeMoveMode::ConstantSpeed;
    nodeMoveSpeed[2]    = -0.5;
    nodeMoveSpeedAdd[2] =  1.0;

    nodeDistance = 5;
};

//-----------------------------------------------------------------------------
// hit enemy...

datablock ParticleData(WhiteAssaultrifleProjectileHit_Particle)
{
	dragCoefficient    = 0.0;
	windCoefficient    = 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;

	lifetimeMS			  = 250;
	lifetimeVarianceMS	= 0;

	useInvAlpha =  false;

	textureName	= "share/textures/cat5/circle1";

	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "1.0 1.0 1.0 0.5";
	colors[2]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 0.5;
	sizes[1]		= 1.0;
	sizes[2]		= 1.5;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

	allowLighting = false;
	renderDot = true;
};

datablock ParticleEmitterData(WhiteAssaultrifleProjectileHit_Emitter)
{
	ejectionOffset	= 0;

	ejectionPeriodMS = 40;
	periodVarianceMS = 0;

	ejectionVelocity = 0.0;
	velocityVariance = 0.0;

	thetaMin			= 0.0;
	thetaMax			= 60.0;

	lifetimeMS		 = 100;

	particles = "WhiteAssaultrifleProjectileHit_Particle";
};

datablock ExplosionData(WhiteAssaultrifleProjectileHit)
{
	soundProfile = AssaultrifleProjectileHitSound;

	lifetimeMS = 450;

	particleEmitter = WhiteAssaultrifleProjectileHit_Emitter;
	particleDensity = 1;
	particleRadius = 0;

	// Dynamic light
	lightStartRadius = 2;
	lightEndRadius = 0;
	lightStartColor = "1.0 1.0 1.0";
	lightEndColor = "1.0 1.0 1.0";
   lightCastShadows = false;
};

//-----------------------------------------------------------------------------
// impact...

datablock ParticleData(WhiteAssaultrifleProjectileImpact_Smoke)
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

datablock ParticleEmitterData(WhiteAssaultrifleProjectileImpact_SmokeEmitter)
{
	ejectionOffset	= 0;

	ejectionPeriodMS = 40;
	periodVarianceMS = 0;

	ejectionVelocity = 2.0;
	velocityVariance = 0.1;

	thetaMin			= 0.0;
	thetaMax			= 60.0;

	lifetimeMS		 = 100;

	particles = "WhiteAssaultrifleProjectileImpact_Smoke";
};

datablock ExplosionData(WhiteAssaultrifleProjectileImpact : WhiteAssaultrifleProjectileHit)
{
	soundProfile = AssaultrifleProjectileImpactSound;

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
	emitter[1] = WhiteAssaultrifleProjectileImpact_SmokeEmitter;

	//debris = WhiteAssaultrifleProjectileImpact_Debris;
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
// missed enemy...

datablock ExplosionData(WhiteAssaultrifleProjectileMissedEnemyEffect)
{
	soundProfile = AssaultrifleProjectileMissedEnemySound;

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
	//lightEndRadius = 2;
	//lightStartColor = "0.5 0.5 0.5";
	//lightEndColor = "0.0 0.0 0.0";
   //lightCastShadows = false;
};


