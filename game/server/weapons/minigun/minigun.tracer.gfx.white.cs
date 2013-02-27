//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// projectile particle emitter

datablock ParticleData(WhiteMinigunTracerParticleEmitter_Particles)
{
	dragCoeffiecient	  = 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;

	lifetimeMS			  = 200;
	lifetimeVarianceMS	= 0;

	useInvAlpha =  false;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;

	textureName = "share/textures/cat5/star3.white.png";

	colors[0]	  = "1.0 1.0 1.0 0.6";
	colors[1]	  = "1.0 1.0 1.0 0.3";
	colors[2]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 0.6;
	sizes[1]		= 0.3;
	sizes[2]		= 0.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

	allowLighting = false;
};

datablock ParticleEmitterData(WhiteMinigunTracerParticleEmitte)
{
	ejectionPeriodMS = 2;
	periodVarianceMS = 0;
	ejectionVelocity = 0.0;
	velocityVariance = 0.0;
	ejectionOffset	 = 0.0;
	thetaMin		 = 0;
	thetaMax		 = 0;
	phiReferenceVel  = 0;
	phiVariance		 = 0;
	overrideAdvances = false;
	orientParticles  = false;
	lifetimeMS		 = 0;
	particles = "WhiteMinigunTracerParticleEmitter_Particles";
};


//-----------------------------------------------------------------------------
// laser tail...

datablock LaserBeamData(WhiteMinigunTracerLaserTail)
{
   allowColorization = true;

	hasLine = true;
	lineStartColor	= "0.9 0.9 0.9 0.0";
	lineBetweenColor = "0.9 0.9 0.9 1.0";
	lineEndColor	  = "0.9 0.9 0.9 1.0";
	lineWidth		  = 2.0;

	hasInner = true;
	innerStartColor = "0.9 0.9 0.9 0.0";
	innerBetweenColor = "0.9 0.9 0.9 1.0";
	innerEndColor = "0.9 0.9 0.9 1.0";
	innerStartWidth = "0.2";
	innerBetweenWidth = "0.2";
	innerEndWidth = "0.2";

	hasOuter = true;
	outerStartColor = "0.9 0.9 0.9 0.0";
	outerBetweenColor = "0.9 0.9 0.9 1.0";
	outerEndColor = "0.9 0.9 0.9 1.0";
	outerStartWidth = "0.3";
	outerBetweenWidth = "0.3";
	outerEndWidth = "0.3";
	
//	bitmap = "share/shapes/rotc/weapons/blaster/lasertail.blue";
//	bitmapWidth = 0.20;
//	crossBitmap = "share/shapes/rotc/weapons/blaster/lasertail.blue.cross";
//	crossBitmapWidth = 0.10;

	betweenFactor = 0.99;
	blendMode = 1;
};

//-----------------------------------------------------------------------------
// laser trail

datablock MultiNodeLaserBeamData(WhiteMinigunTracerLaserTrailOne)
{
   allowColorization = true;

	hasLine = true;
	lineColor	= "1.00 1.00 1.00 0.5";

	hasInner = false;
	innerColor = "0.00 1.00 0.00 1.00";
	innerWidth = "0.05";

	hasOuter = false;
	outerColor = "1.00 1.00 1.00 0.02";
	outerWidth = "0.05";

	bitmap = "share/textures/cat5/beam2.white";
	bitmapWidth = 0.5;

	blendMode = 1;
 
    windCoefficient = 0.0;

    // node x movement...
    nodeMoveMode[0]     = $NodeMoveMode::None;
    nodeMoveSpeed[0]    = -0.002;
    nodeMoveSpeedAdd[0] =  0.004;
    // node y movement...
    nodeMoveMode[1]     = $NodeMoveMode::None;
    nodeMoveSpeed[1]    = -0.002;
    nodeMoveSpeedAdd[1] =  0.004;
    // node z movement...
    nodeMoveMode[2]     = $NodeMoveMode::None;
    nodeMoveSpeed[2]    = 0.5;
    nodeMoveSpeedAdd[2] = 1.0;

	//nodeDistance = 3;
 
	fadeTime = 250;
};

datablock MultiNodeLaserBeamData(WhiteMinigunTracerLaserTrailTwo)
{
   allowColorization = true;

	hasLine = false;
	lineColor	= "1.00 1.00 1.00 0.5";

	hasInner = false;
	innerColor = "0.00 1.00 0.00 1.00";
	innerWidth = "0.05";

	hasOuter = false;
	outerColor = "1.00 1.00 1.00 0.02";
	outerWidth = "0.05";

	bitmap = "share/textures/cat5/beam1.white";
	bitmapWidth = 1.0;

	blendMode = 1;

    windCoefficient = 0.0;

    // node x movement...
    nodeMoveMode[0]     = $NodeMoveMode::None;
    nodeMoveSpeed[0]    = -0.002;
    nodeMoveSpeedAdd[0] =  0.004;
    // node y movement...
    nodeMoveMode[1]     = $NodeMoveMode::None;
    nodeMoveSpeed[1]    = -0.002;
    nodeMoveSpeedAdd[1] =  0.004;
    // node z movement...
    nodeMoveMode[2]     = $NodeMoveMode::None;
    nodeMoveSpeed[2]    = 0.5;
    nodeMoveSpeedAdd[2] = 1.0;

	//nodeDistance = 3;

	fadeTime = 1000;
};

//-----------------------------------------------------------------------------
// hit enemy...

datablock ParticleData(WhiteMinigunTracerHit_Particle)
{
	dragCoefficient    = 0.0;
	windCoefficient    = 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;

	lifetimeMS			  = 250;
	lifetimeVarianceMS	= 0;

	useInvAlpha =  false;

	textureName	= "share/textures/cat5/circle1";

	colors[0]	  = "0.0 0.0 1.0 1.0";
	colors[1]	  = "0.0 0.0 1.0 1.0";
	colors[2]	  = "0.0 0.0 1.0 0.0";
	sizes[0]		= 0.5;
	sizes[1]		= 1.0;
	sizes[2]		= 1.5;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

	allowLighting = false;
	renderDot = true;
};

datablock ParticleEmitterData(WhiteMinigunTracerHit_Emitter)
{
	ejectionOffset	= 0;

	ejectionPeriodMS = 40;
	periodVarianceMS = 0;

	ejectionVelocity = 0.0;
	velocityVariance = 0.0;

	thetaMin			= 0.0;
	thetaMax			= 60.0;

	lifetimeMS		 = 100;

	particles = "WhiteMinigunTracerHit_Particle";
};

datablock ExplosionData(WhiteMinigunTracerHit)
{
	soundProfile = WhiteMinigunTracerImpactSound;

	lifetimeMS = 450;

	particleEmitter = WhiteMinigunTracerHit_Emitter;
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
// impact...

datablock ParticleData(WhiteMinigunTracerImpact_Smoke)
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
	sizes[1]		= 3.0;
	times[0]		= 0.0;
	times[1]		= 1.0;

	allowLighting = false;
};

datablock ParticleEmitterData(WhiteMinigunTracerImpact_SmokeEmitter)
{
	ejectionOffset	= 0;

	ejectionPeriodMS = 20;
	periodVarianceMS = 0;

	ejectionVelocity = 2.0;
	velocityVariance = 0.1;

	thetaMin			= 0.0;
	thetaMax			= 60.0;

	lifetimeMS		 = 100;

	particles = "WhiteMinigunTracerImpact_Smoke";
};

datablock ExplosionData(WhiteMinigunTracerImpact)
{
	soundProfile = WhiteMinigunTracerImpactSound;

	lifetimeMS = 150;

 	// shape...
	//explosionShape = "share/shapes/rotc/weapons/blaster/projectile.impact.red.dts";
	faceViewer = false;
	playSpeed = 0.4;
	sizes[0] = "1 1 1";
	sizes[1] = "1 1 1";
	times[0] = 0.0;
	times[1] = 1.0;

	particleEmitter = WhiteMinigunTracerHit_Emitter;
	particleDensity = 1;
	particleRadius = 0;

	emitter[0] = DefaultSmallWhiteDebrisEmitter;
	emitter[1] = WhiteMinigunTracerImpact_SmokeEmitter;

	//debris = WhiteMinigunTracerImpact_Debris;
	//debrisThetaMin = 0;
	//debrisThetaMax = 60;
	//debrisNum = 1;
	//debrisNumVariance = 1;
	//debrisVelocity = 10.0;
	//debrisVelocityVariance = 5.0;

	// Dynamic light
	lightStartRadius = 2;
	lightEndRadius = 0;
	lightStartColor = "1.0 1.0 1.0";
	lightEndColor = "1.0 1.0 1.0";
   lightCastShadows = false;

	shakeCamera = false;
};

//-----------------------------------------------------------------------------
// missed enemy...

datablock ExplosionData(WhiteMinigunTracerMissedEnemyEffect)
{
	soundProfile = MinigunTracerMissedEnemySound;

	// shape...
	//explosionShape = "share/shapes/rotc/effects/explosion2_white.dts";
	//faceViewer	  = true;
	//playSpeed = 8.0;
	//sizes[0] = "0.07 0.07 0.07";
	//sizes[1] = "0.01 0.01 0.01";
	//times[0] = 0.0;
	//times[1] = 1.0;
	
	//emitter[0] = WhiteMinigunTracerImpact_SmokeEmitter;

	// dynamic light...
	//lightStartRadius = 0;
	//lightEndRadius = 0;
	//lightStartColor = "0.5 0.5 0.5";
	//lightEndColor = "0.0 0.0 0.0";
   //lightCastShadows = false;
};

