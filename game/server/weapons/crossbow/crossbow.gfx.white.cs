//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// projectile particle emitter

datablock ParticleData(WhiteCrossbowProjectileParticleEmitter_Particles)
{
	dragCoeffiecient	  = 0.0;
	gravityCoefficient	= -0.2;
	inheritedVelFactor	= 0.0;

	lifetimeMS			  = 1000;
	lifetimeVarianceMS	= 250;

	useInvAlpha =  false;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;

	textureName = "share/textures/cat5/smoke1.png";

	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "1.0 1.0 1.0 0.5";
	colors[2]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 0.5;
	sizes[1]		= 1.0;
	sizes[2]		= 1.5;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

	allowLighting = true;
};

datablock ParticleEmitterData(WhiteCrossbowProjectileParticleEmitter)
{
	ejectionPeriodMS = 10;
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
// laser tail...

datablock LaserBeamData(WhiteCrossbowProjectileLaserTail)
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
	innerStartWidth = "0.0";
	innerBetweenWidth = "0.5";
	innerEndWidth = "0.5";

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

datablock ParticleData(WhiteCrossbowProjectileImpact_Debris_SmokeEmitter_Particles)
{
	dragCoeffiecient	  = 0.0;
	gravityCoefficient	= -0.1;
	inheritedVelFactor	= 0.0;

	lifetimeMS			  = 1000;
	lifetimeVarianceMS	= 300;

	useInvAlpha =  true;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;

	textureName = "share/textures/cat5/smoke1.png";

	colors[0]	  = "1.0 1.0 1.0 0.4";
	colors[1]	  = "1.0 1.0 1.0 0.2";
	colors[2]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 0.3;
	sizes[1]		= 0.6;
	sizes[2]		= 0.9;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

	allowLighting = false;
};

datablock ParticleEmitterData(WhiteCrossbowProjectileImpact_Debris_SmokeEmitter)
{
	ejectionPeriodMS = 50;
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
	particles = "WhiteCrossbowProjectileImpact_Debris_SmokeEmitter_Particles";
};

datablock ParticleData(WhiteCrossbowProjectileImpact_Debris_FireEmitter_Particles)
{
	dragCoeffiecient	  = 0.0;
	gravityCoefficient	= -0.1;
	inheritedVelFactor	= 0.0;

	lifetimeMS			  = 200;
	lifetimeVarianceMS	= 20;

	useInvAlpha =  false;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;

	textureName = "share/textures/cat5/smoke2.png";

	colors[0]	  = "1.0 0.0 0.0 1.0";
	colors[1]	  = "1.0 0.5 0.0 0.5";
	colors[2]	  = "1.0 0.5 0.0 0.0";
	sizes[0]		= 3.0;
	sizes[1]		= 2.0;
	sizes[2]		= 1.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

	allowLighting = false;
};

datablock ParticleEmitterData(WhiteCrossbowProjectileImpact_Debris_FireEmitter)
{
	ejectionPeriodMS = 10;
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
	particles = "WhiteCrossbowProjectileImpact_Debris__FireEmitter_Particles";
};

datablock DebrisData(WhiteCrossbowProjectileImpact_Debris)
{
   shapeFile = "share/shapes/cat5/debris3.dts";

	explodeOnMaxBounce = false;

	elasticity = 0.35;
	friction = 0.5;

	lifetime = 4.0;
	lifetimeVariance = 1.0;

	minSpinSpeed = 60;
	maxSpinSpeed = 600;

	numBounces = 5;
	bounceVariance = 0;

	staticOnMaxBounce = true;
	gravModifier = 4.0;

	useRadiusMass = true;
	baseRadius = 1;

	velocity = 30.0;
	velocityVariance = 15.0;

   terminalVelocity = 0;

   emitters[0] = WhiteCrossbowProjectileImpact_Debris_SmokeEmitter;
   //emitters[1] = WhiteCrossbowProjectileImpact_Debris_FireEmitter;

   //decals[0] = StandardCatDebrisDecalOne;
};

datablock ExplosionData(WhiteCrossbowProjectileImpact)
{
	soundProfile = CrossbowProjectileImpactSound;

	lifetimeMS = 200;

 	// shape...
	//explosionShape = "share/shapes/rotc/effects/explosion2_white.dts";
	//faceViewer	  = false;
	//playSpeed = 8.0;
	//sizes[0] = "0.2 0.2 0.2";
	//sizes[1] = "0.2 0.2 0.2";
	//times[0] = 0.0;
	//times[1] = 1.0;

	debris = WhiteCrossbowProjectileImpact_Debris;
	debrisThetaMin = 0;
	debrisThetaMax = 180;
	debrisNum = 9;
	debrisVelocity = 40.0;
	debrisVelocityVariance = 4.0;

	//particleEmitter = WhiteCrossbowProjectileHit_CloudEmitter;
	//particleDensity = 25;
	//particleRadius = 0.5;

	//emitter[0] = DefaultMediumWhiteDebrisEmitter;
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



