//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// laser trail

datablock MultiNodeLaserBeamData(RedRepelDiscInnerLaserTrail)
{
	hasLine = false;
	lineColor	= "1.00 0.00 0.00 0.3";

	hasInner = false;
	innerColor = "1.00 0.00 1.00 0.3";
	innerWidth = "0.05";

	hasOuter = false;
	outerColor = "1.00 0.00 1.00 0.1";
	outerWidth = "0.10";

	bitmap = "share/shapes/rotc/weapons/disc/lasertrail2.red";
	bitmapWidth = 0.60;

	blendMode = 1;
	renderMode = $MultiNodeLaserBeamRenderMode::FaceViewer;
	fadeTime = 250;
};

datablock MultiNodeLaserBeamData(RedRepelDiscOuterLaserTrail)
{
	hasLine = false;
	lineColor	= "1.00 0.00 0.00 0.3";

	hasInner = true;
	innerColor = "1.00 1.00 1.00 0.5";
	innerWidth = "0.1";

	hasOuter = false;
	outerColor = "1.00 0.00 1.00 0.1";
	outerWidth = "0.10";

	//bitmap = "share/shapes/rotc/weapons/disc/lasertrail2.red";
	//bitmapWidth = 0.60;

	blendMode = 1;
	renderMode = $MultiNodeLaserBeamRenderMode::FaceViewer;
	fadeTime = 750;
};

//-----------------------------------------------------------------------------
// explosion

datablock ParticleData(RedRepelDiscExplosion_Emitter_Particle)
{
	dragCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 220;
	lifetimeVarianceMS	= 0;
	colors[0]	  = "1.0 0.0 0.0 1.0";
	colors[1]	  = "1.0 0.0 0.0 1.0";
	colors[2]	  = "1.0 0.0 0.0 0.0";
	sizes[0]		= 3.0;
	sizes[1]		= 2.0;
	sizes[2]		= 0.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	spinRandomMin = 0.0;
	spinRandomMax = 0.0;
	textureName	= "share/textures/rotc/smoke_particle";
	allowLighting = false;
};

datablock ParticleEmitterData(RedRepelDiscExplosion_Emitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = -5.0;
	velocityVariance = 0.0;
	ejectionOffset	 = 1.0;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvance  = false;
	orientParticles  = false;
	lifetimeMS		 = 100;
	particles = RedRepelDiscExplosion_Emitter_Particle;
};

datablock ExplosionData(RedRepelDiscExplosion)
{
	soundProfile = DiscExplosionSound;

	lifetimeMS = 200;

 	// shape...
	explosionShape = "share/shapes/rotc/effects/explosion4_white.dts";
	faceViewer	  = false;
	playSpeed = 4.0;
	sizes[0] = "0.2 0.2 0.2";
	sizes[1] = "0.5 0.5 0.5";
	times[0] = 0.0;
	times[1] = 1.0;
 
	emitter[0] = RedRepelDiscExplosion_Emitter;

	// Camera shake
	shakeCamera = false;
	camShakeFreq = "10.0 6.0 9.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.5;
	camShakeRadius = 20.0;

	// Dynamic light
	lightStartRadius = 0;
	lightEndRadius = 4;
	lightStartColor = "1.0 0.0 0.0";
	lightEndColor = "0.0 0.0 0.0";
};

//-----------------------------------------------------------------------------
// deflected effect

datablock ExplosionData(RedRepelDiscDeflectedEffect)
{
	soundProfile	= DiscDeflectedSound;

	explosionShape = "share/shapes/rotc/effects/explosion_white.dts";
	faceViewer	  = true;
	playSpeed = 4.0;
	sizes[0] = "0.2 0.2 0.2";
	sizes[1] = "1.0 1.0 1.0";
	times[0] = 0.0;
	times[1] = 1.0;

//	debris = Team1AntiairNearEnemyExplosionDebris;
//	debrisThetaMin = 0;
//	debrisThetaMax = 180;
//	debrisNum = 15;
//	debrisVelocity = 20.0;
//	debrisVelocityVariance = 15.0;

//	emitter[0] = Team1AntiairNearEnemyExplosion_CloudEmitter;
//	emitter[0] = Team1AntiairNearEnemyExplosion_Cloud2Emitter;

	// Camera Shaking
	shakeCamera = true;
	camShakeFreq = "10.0 11.0 10.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.5;
	camShakeRadius = 20.0;

	// Dynamic light
	lightStartRadius = 20;
	lightEndRadius = 2;
	lightStartColor = "1.0 1.0 1.0 1.0";
	lightEndColor = "1.0 1.0 1.0 0.3";
};

//-----------------------------------------------------------------------------
// bounce effect

datablock ExplosionData(RedRepelDiscBounceEffect)
{
	soundProfile	= DiscBounceSound;

	// shape...
	explosionShape = "share/shapes/rotc/effects/explosion_white.dts";
	faceViewer	  = false;
	playSpeed = 4.0;
	sizes[0] = "0.2 0.2 0.2";
	sizes[1] = "1.0 1.0 1.0";
	times[0] = 0.0;
	times[1] = 1.0;

//	debris = Team1AntiairNearEnemyExplosionDebris;
//	debrisThetaMin = 0;
//	debrisThetaMax = 180;
//	debrisNum = 15;
//	debrisVelocity = 20.0;
//	debrisVelocityVariance = 15.0;

//	emitter[0] = Team1AntiairNearEnemyExplosion_CloudEmitter;
//	emitter[0] = Team1AntiairNearEnemyExplosion_Cloud2Emitter;

	// Camera Shaking
	shakeCamera = true;
	camShakeFreq = "10.0 11.0 10.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.5;
	camShakeRadius = 20.0;

	// Dynamic light
	lightStartRadius = 20;
	lightEndRadius = 2;
	lightStartColor = "1.0 1.0 1.0 1.0";
	lightEndColor = "1.0 1.0 1.0 0.3";
};

//-----------------------------------------------------------------------------
// hit effect

datablock DebrisData(RedRepelDiscHit_Debris)
{
	// shape...
	shapeFile = "share/shapes/rotc/misc/debris1.white.dts";

	// bounce...
	staticOnMaxBounce = true;
	numBounces = 5;

	// physics...
	gravModifier = 2.0;
	elasticity = 0.6;
	friction = 0.1;

	// spin...
	minSpinSpeed = 60;
	maxSpinSpeed = 600;

	// lifetime...
	lifetime = 4.0;
	lifetimeVariance = 1.0;
};

datablock ExplosionData(RedRepelDiscHit)
{
	soundProfile	= DiscHitSound;

	faceViewer	  = true;
	explosionScale = "0.8 0.8 0.8";

	lifetimeMS = 250;
	
	emitter[0] = DefaultSmallWhiteDebrisEmitter;

	//debris = RedRepelDiscHit_Debris;
	//debrisThetaMin = 0;
	//debrisThetaMax = 60;
	//debrisNum = 3;
	//debrisNumVariance = 1;
	//debrisVelocity = 10.0;
	//debrisVelocityVariance = 5.0;

	// Dynamic light
	lightStartRadius = 1;
	lightEndRadius = 5;
	lightStartColor = "1.0 0.0 0.0";
	lightEndColor = "0.0 0.0 0.0";

	shakeCamera = false;
};

//-----------------------------------------------------------------------------
// hit enemy...

datablock ExplosionData(RedRepelDiscHitEnemy)
{
	soundProfile = DiscHitSound;

	faceViewer	  = true;
	explosionScale = "0.8 0.8 0.8";

	lifetimeMS = 250;

	// Dynamic light
	lightStartRadius = 1;
	lightEndRadius = 5;
	lightStartColor = "1.0 0.0 0.0";
	lightEndColor = "0.0 0.0 0.0";

	shakeCamera = false;
};



