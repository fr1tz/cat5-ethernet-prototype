//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// projectile particle emitter

datablock ParticleData(WhiteCrossbowProjectileParticleEmitter_Particles)
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

datablock ParticleEmitterData(WhiteCrossbowProjectileParticleEmitter)
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
	particles = "WhiteCrossbowProjectileParticleEmitter_Particles";
};

//-----------------------------------------------------------------------------
// laser trail

datablock MultiNodeLaserBeamData(WhiteCrossbowProjectileLaserTrail)
{
	hasLine = true;
	lineColor	= "0.00 0.00 1.00 1.00";
	lineWidth = 1;

	hasInner = false;
	innerColor = "0.00 1.00 0.00 1.00";
	innerWidth = "0.05";

	hasOuter = false;
	outerColor = "1.00 0.00 0.00 0.02";
	outerWidth = "0.10";

	bitmap = "share/shapes/rotc/weapons/disc/lasertrail2.blue";
	bitmapWidth = 0.25;

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
    nodeMoveSpeedAdd[2] = 0.5;
 
	fadeTime = 200;
};

//-----------------------------------------------------------------------------
// laser tail...

datablock LaserBeamData(WhiteCrossbowProjectileLaserTail)
{
   allowColorization = true;

	hasLine = true;
	lineStartColor	= "0.9 0.9 0.9 0.0";
	lineBetweenColor = "0.9 0.9 0.9 1.0";
	lineEndColor	  = "0.9 0.9 0.9 1.0";
	lineWidth		  = 2.0;

	hasInner = false;
	innerStartColor = "0.9 0.9 0.9 0.0";
	innerBetweenColor = "0.9 0.9 0.9 1.0";
	innerEndColor = "0.9 0.9 0.9 1.0";
	innerStartWidth = "0.2";
	innerBetweenWidth = "0.2";
	innerEndWidth = "0.2";

	hasOuter = false;
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
// hit

datablock ExplosionData(WhiteCrossbowProjectileHit)
{
	soundProfile = CrossbowProjectileHitSound;

	// shape...
	//explosionShape = "share/shapes/rotc/effects/explosion5.green.dts";
	//faceViewer	  = true;
	//playSpeed = 4.0;
	//sizes[0] = "0.01 0.01 0.01";
	//sizes[1] = "0.07 0.07 0.07";
	//times[0] = 0.0;
	//times[1] = 1.0;

	// dynamic light...
	//lightStartRadius = 0;
	//lightEndRadius = 0;
	//lightStartColor = "0.0 1.0 0.0";
	//lightEndColor = "0.0 0.0 0.0";
   //lightCastShadows = false;
};

//-----------------------------------------------------------------------------
// impact...

datablock ExplosionData(WhiteCrossbowProjectileImpact)
{
	soundProfile = CrossbowProjectileImpactSound;

	lifetimeMS = 200;

 	// shape...
	explosionShape = "share/shapes/rotc/effects/explosion2_white.dts";
	faceViewer	  = false;
	playSpeed = 8.0;
	sizes[0] = "0.2 0.2 0.2";
	sizes[1] = "0.2 0.2 0.2";
	times[0] = 0.0;
	times[1] = 1.0;

	//debris = 0; //WhiteCrossbowProjectileHit_Debris;
	//debrisThetaMin = 0;
	//debrisThetaMax = 180;
	//debrisNum = 3;
	//debrisVelocity = 50.0;
	//debrisVelocityVariance = 10.0;

	//particleEmitter = WhiteCrossbowProjectileHit_CloudEmitter;
	//particleDensity = 25;
	//particleRadius = 0.5;

	emitter[0] = DefaultMediumWhiteDebrisEmitter;
	//emitter[1] = WhiteCrossbowProjectileHit_DustEmitter;
	//emitter[2] = 0; // WhiteCrossbowProjectileHit_SmokeEmitter;

	// Camera shake
	shakeCamera = false;
	camShakeFreq = "10.0 6.0 9.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.5;
	camShakeRadius = 20.0;

	// Dynamic light
	//lightStartRadius = 15;
	//lightEndRadius = 0;
	//lightStartColor = "0.9 0.9 0.9";
	//lightEndColor = "0.0 0.0 0.0";
};

//-----------------------------------------------------------------------------
// missed enemy...

datablock ExplosionData(WhiteCrossbowProjectileMissedEnemyEffect)
{
	soundProfile = CrossbowProjectileMissedEnemySound;

	// shape...
	//explosionShape = "share/shapes/rotc/effects/explosion5.green.dts";
	//faceViewer	  = true;
	//playSpeed = 4.0;
	//sizes[0] = "0.01 0.01 0.01";
	//sizes[1] = "0.07 0.07 0.07";
	//times[0] = 0.0;
	//times[1] = 1.0;

	// dynamic light...
	//lightStartRadius = 0;
	//lightEndRadius = 0;
	//lightStartColor = "0.0 1.0 0.0";
	//lightEndColor = "0.0 0.0 0.0";
   //lightCastShadows = false;
};



