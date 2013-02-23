//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// light images...

datablock ShapeBaseImageData(StandardCatLightImage)
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
	lightColor = "0.7 0.7 0.7";
	lightTime = 1000;
	lightRadius = 5.0;
	lightCastsShadows = false;
	lightAffectsShapes = false;

	stateName[0] = "DoNothing";
};

//------------------------------------------------------------------------------

datablock ParticleData(StandardCatSlideFootEmitter_Particle)
{
	dragCoefficient		= 1.0;
	gravityCoefficient	= 2.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 300;
	lifetimeVarianceMS	= 100;
	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "1.0 0.0 0.0 0.5";
	colors[2]	  = "1.0 0.0 0.0 0.0";
	sizes[0]		= 1.0;
	sizes[1]		= 1.0;
	sizes[2]		= 1.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	textureName	= "share/textures/rotc/dustParticle";
	allowLighting = false;
};

datablock ParticleEmitterData(StandardCatSlideFootEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = -5;
	velocityVariance = 0;
	ejectionOffset	= 0.75;
	thetaMin			= 90;
	thetaMax			= 90;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	orientParticles  = false;
	lifetimeMS		 = 0; // forever
	particles = StandardCatSlideFootEmitter_Particle;
};

//------------------------------------------------------------------------------

datablock ParticleData(CatSlideContactTrailEmitter_Particle)
{
	dragCoefficient		= 1.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 1000;
	lifetimeVarianceMS	= 350;
	colors[0]	  = "0.7 0.7 0.7 1.0";
	colors[1]	  = "0.7 0.7 0.7 0.5";
	colors[2]	  = "0.7 0.7 0.7 0.0";
	sizes[0]		= 1.0;
	sizes[1]		= 1.0;
	sizes[2]		= 1.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	textureName	= "share/textures/rotc/dustParticle";
	useInvAlpha = true;
	allowLighting = false;
};

datablock ParticleEmitterData(CatSlideContactTrailEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 2;
	velocityVariance = 1;
	ejectionOffset	= 0.75;
	thetaMin			= 90;
	thetaMax			= 90;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	orientParticles  = false;
	lifetimeMS		 = 0; // forever
	particles = CatSlideContactTrailEmitter_Particle;
};

//------------------------------------------------------------------------------

datablock ParticleData(CatSkidFootEmitter_Particle)
{
	dragCoefficient		= 1.0;
	gravityCoefficient	= 2.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 300;
	lifetimeVarianceMS	= 100;
	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "1.0 1.0 0.0 0.5";
	colors[2]	  = "1.0 1.0 0.0 0.0";
	sizes[0]		= 1.0;
	sizes[1]		= 1.0;
	sizes[2]		= 1.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	textureName	= "share/textures/rotc/dustParticle";
	allowLighting = false;
};

datablock ParticleEmitterData(CatSkidFootEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 1;
	velocityVariance = 0;
	ejectionOffset	= 0.75;
	thetaMin			= 20;
	thetaMax			= 50;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	orientParticles  = false;
	lifetimeMS		 = 0; // forever
	particles = CatSkidFootEmitter_Particle;
};

//------------------------------------------------------------------------------

datablock ParticleData(CatSkidTrailEmitter0_Particle)
{
	dragCoefficient		= 0.0;
	gravityCoefficient	= 3.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 1000;
	lifetimeVarianceMS	= 350;
	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "1.0 1.0 1.0 0.5";
	colors[2]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 0.5;
	sizes[1]		= 0.5;
	sizes[2]		= 0.5;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	textureName	= "share/textures/rotc/spark00";
	useInvAlpha = false;
	allowLighting = false;
};

datablock ParticleEmitterData(CatSkidTrailEmitter0)
{
	ejectionPeriodMS = 2;
	periodVarianceMS = 0;
	ejectionVelocity = 10;
	velocityVariance = 5;
	ejectionOffset	= 0.0;
	thetaMin			= 20;
	thetaMax			= 50;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	orientParticles  = true;
	lifetimeMS		 = 0; // forever
	particles = CatSkidTrailEmitter0_Particle;
};

//------------------------------------------------------------------------------

datablock ParticleData(CatSkidTrailEmitter1_Particle)
{
	dragCoefficient		= 1.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 1000;
	lifetimeVarianceMS	= 350;
	colors[0]	  = "0.5 0.5 0.5 1.0";
	colors[1]	  = "0.5 0.5 0.5 0.5";
	colors[2]	  = "0.5 0.5 0.5 0.0";
	sizes[0]		= 1.0;
	sizes[1]		= 1.0;
	sizes[2]		= 1.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	textureName	= "share/textures/rotc/smoke_particle";
	useInvAlpha = true;
	allowLighting = false;
};

datablock ParticleEmitterData(CatSkidTrailEmitter1)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 0;
	velocityVariance = 0;
	ejectionOffset	= 0.25;
	thetaMin			= 90;
	thetaMax			= 90;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	orientParticles  = false;
	lifetimeMS		 = 0; // forever
	particles = CatSkidTrailEmitter1_Particle;
};

//------------------------------------------------------------------------------

datablock ParticleData(StandardCatDebris_SmokeEmitter_Particles)
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
	sizes[0]		= 1.0;
	sizes[1]		= 1.5;
	sizes[2]		= 2.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

	allowLighting = false;
};

datablock ParticleEmitterData(StandardCatDebris_SmokeEmitter)
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
	particles = "StandardCatDebris_SmokeEmitter_Particles";
};

datablock ParticleData(StandardCatDebris_FireEmitter_Particles)
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

datablock ParticleEmitterData(StandardCatDebris_FireEmitter)
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
	particles = "StandardCatDebris_FireEmitter_Particles";
};

datablock DecalData(StandardCatDebrisDecalOne)
{
	sizeX = "3.0";
	sizeY = "3.0";
	textureName = "share/textures/cat5/bluedecal1";
	SelfIlluminated = true;
};

datablock DebrisData(StandardCatDebris)
{
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

	velocity = 18.0;
	velocityVariance = 12.0;

   terminalVelocity = 0;

   emitters[0] = StandardCatDebris_SmokeEmitter;
   //emitters[1] = StandardCatDebris_FireEmitter;

   decals[0] = StandardCatDebrisDecalOne;
};

//----------------------------------------------------------------------------
// Splash
//----------------------------------------------------------------------------

datablock ParticleData(StandardCatSplashMist)
{
	dragCoefficient		= 2.0;
	gravityCoefficient	= -0.05;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 400;
	lifetimeVarianceMS	= 100;
	useInvAlpha			 = false;
	spinRandomMin		  = -90.0;
	spinRandomMax		  = 500.0;
	textureName			 = "share/shapes/rotc/players/shared/splash";
	colors[0]	  = "0.7 0.8 1.0 1.0";
	colors[1]	  = "0.7 0.8 1.0 0.5";
	colors[2]	  = "0.7 0.8 1.0 0.0";
	sizes[0]		= 0.5;
	sizes[1]		= 0.5;
	sizes[2]		= 0.8;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
};

datablock ParticleEmitterData(StandardCatSplashMistEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 3.0;
	velocityVariance = 2.0;
	ejectionOffset	= 0.0;
	thetaMin			= 85;
	thetaMax			= 85;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	lifetimeMS		 = 250;
	particles = StandardCatSplashMist;
};


datablock ParticleData(StandardCatBubbleParticle)
{
	dragCoefficient		= 0.0;
	gravityCoefficient	= -0.50;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 400;
	lifetimeVarianceMS	= 100;
	useInvAlpha			 = false;
	textureName			 = "share/shapes/rotc/players/shared/splash";
	colors[0]	  = "0.7 0.8 1.0 0.4";
	colors[1]	  = "0.7 0.8 1.0 0.4";
	colors[2]	  = "0.7 0.8 1.0 0.0";
	sizes[0]		= 0.1;
	sizes[1]		= 0.3;
	sizes[2]		= 0.3;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
};

datablock ParticleEmitterData(StandardCatBubbleEmitter)
{
	ejectionPeriodMS = 1;
	periodVarianceMS = 0;
	ejectionVelocity = 2.0;
	ejectionOffset	= 0.5;
	velocityVariance = 0.5;
	thetaMin			= 0;
	thetaMax			= 80;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	particles = StandardCatBubbleParticle;
};

datablock ParticleData(StandardCatFoamParticle)
{
	dragCoefficient		= 2.0;
	gravityCoefficient	= -0.05;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 400;
	lifetimeVarianceMS	= 100;
	useInvAlpha			 = false;
	spinRandomMin		  = -90.0;
	spinRandomMax		  = 500.0;
	textureName			 = "share/shapes/rotc/players/shared/splash";
	colors[0]	  = "0.7 0.8 1.0 0.20";
	colors[1]	  = "0.7 0.8 1.0 0.20";
	colors[2]	  = "0.7 0.8 1.0 0.00";
	sizes[0]		= 0.2;
	sizes[1]		= 0.4;
	sizes[2]		= 1.6;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
};

datablock ParticleEmitterData(StandardCatFoamEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = 3.0;
	velocityVariance = 1.0;
	ejectionOffset	= 0.0;
	thetaMin			= 85;
	thetaMax			= 85;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	particles = StandardCatFoamParticle;
};


datablock ParticleData(StandardCatFoamDropletsParticle)
{
	dragCoefficient		= 1;
	gravityCoefficient	= 0.2;
	inheritedVelFactor	= 0.2;
	constantAcceleration = -0.0;
	lifetimeMS			  = 600;
	lifetimeVarianceMS	= 0;
	textureName			 = "share/shapes/rotc/players/shared/splash";
	colors[0]	  = "0.7 0.8 1.0 1.0";
	colors[1]	  = "0.7 0.8 1.0 0.5";
	colors[2]	  = "0.7 0.8 1.0 0.0";
	sizes[0]		= 0.8;
	sizes[1]		= 0.3;
	sizes[2]		= 0.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
};

datablock ParticleEmitterData(StandardCatFoamDropletsEmitter)
{
	ejectionPeriodMS = 7;
	periodVarianceMS = 0;
	ejectionVelocity = 2;
	velocityVariance = 1.0;
	ejectionOffset	= 0.0;
	thetaMin			= 60;
	thetaMax			= 80;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	orientParticles  = true;
	particles = StandardCatFoamDropletsParticle;
};


datablock ParticleData(StandardCatSplashParticle)
{
	dragCoefficient		= 1;
	gravityCoefficient	= 0.2;
	inheritedVelFactor	= 0.2;
	constantAcceleration = -0.0;
	lifetimeMS			  = 600;
	lifetimeVarianceMS	= 0;
	colors[0]	  = "0.7 0.8 1.0 1.0";
	colors[1]	  = "0.7 0.8 1.0 0.5";
	colors[2]	  = "0.7 0.8 1.0 0.0";
	sizes[0]		= 0.5;
	sizes[1]		= 0.5;
	sizes[2]		= 0.5;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
};

datablock ParticleEmitterData(StandardCatSplashEmitter)
{
	ejectionPeriodMS = 1;
	periodVarianceMS = 0;
	ejectionVelocity = 3;
	velocityVariance = 1.0;
	ejectionOffset	= 0.0;
	thetaMin			= 60;
	thetaMax			= 80;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	orientParticles  = true;
	lifetimeMS		 = 100;
	particles = StandardCatSplashParticle;
};

datablock SplashData(StandardCatSplash)
{
	numSegments = 15;
	ejectionFreq = 15;
	ejectionAngle = 40;
	ringLifetime = 0.5;
	lifetimeMS = 300;
	velocity = 4.0;
	startRadius = 0.0;
	acceleration = -3.0;
	texWrap = 5.0;

	texture = "share/shapes/rotc/players/shared/splash";

	emitter[0] = StandardCatSplashEmitter;
	emitter[1] = StandardCatSplashMistEmitter;

	colors[0] = "0.7 0.8 1.0 0.0";
	colors[1] = "0.7 0.8 1.0 0.3";
	colors[2] = "0.7 0.8 1.0 0.7";
	colors[3] = "0.7 0.8 1.0 0.0";
	times[0] = 0.0;
	times[1] = 0.4;
	times[2] = 0.8;
	times[3] = 1.0;
};

//----------------------------------------------------------------------------
// Foot prints
//----------------------------------------------------------------------------

datablock DecalData(StandardCatFootprint)
{
	sizeX = "0.60";
	sizeY = "0.60";
	textureName = "share/textures/rotc/footprint.red";
	SelfIlluminated = true;
};

datablock DecalData(RedStandardCatFootprint)
{
	sizeX = "0.60";
	sizeY = "0.60";
	textureName = "share/textures/rotc/footprint.blue";
	SelfIlluminated = true;
};

//----------------------------------------------------------------------------
// Foot puffs
//----------------------------------------------------------------------------

datablock ParticleData(StandardCatFootPuff)
{
	dragCoefficient		= 2.0;
	gravityCoefficient	= -0.3;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 1200;
	lifetimeVarianceMS	= 0;
	useInvAlpha			 = false;
	spinRandomMin		  = -35.0;
	spinRandomMax		  = 35.0;
	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "1.0 0.5 0.5 0.5";
	colors[2]	  = "0.5 0.5 0.5 0.0";
	sizes[0]		= 0.8;
	sizes[1]		= 1.2;
	sizes[2]		= 0.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	textureName	= "share/textures/rotc/smoke_particle";
};

datablock ParticleEmitterData(StandardCatFootPuffEmitter)
{
	ejectionPeriodMS = 35;
	periodVarianceMS = 10;
	ejectionVelocity = 0;
	velocityVariance = 0.1;
	ejectionOffset   = -0.6;
	thetaMin         = 0;
	thetaMax         = 0;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	useEmitterColors = false;
	particles = StandardCatFootPuff;
};

//----------------------------------------------------------------------------
// Liftoff dust
//----------------------------------------------------------------------------

datablock ParticleData(StandardCatLiftoffDust)
{
	dragCoefficient		= 1.0;
	gravityCoefficient	= -0.01;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 1000;
	lifetimeVarianceMS	= 100;
	useInvAlpha			 = true;
	spinRandomMin		  = -90.0;
	spinRandomMax		  = 500.0;
	colors[0]	  = "1.0 1.0 1.0 1.0";
	sizes[0]		= 1.0;
	times[0]		= 1.0;
	textureName	= "share/textures/rotc/dustParticle";
};

datablock ParticleEmitterData(StandardCatLiftoffDustEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 2.0;
	velocityVariance = 0.0;
	ejectionOffset	= 0.0;
	thetaMin			= 90;
	thetaMax			= 90;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	useEmitterColors = true;
	particles = StandardCatLiftoffDust;
};

//----------------------------------------------------------------------------
// Ground connection beam
//----------------------------------------------------------------------------

datablock LaserBeamData(StandardCatGroundConnectionBeam)
{
	hasLine = true;
	lineStartColor	= "1.00 1.00 1.00 0.5";
	lineBetweenColor = "1.00 1.00 1.00 0.5";
	lineEndColor	  = "1.00 1.00 1.00 0.5";
 	lineWidth		  = 1.0;

	hasInner = false;
	innerStartColor	= "1.00 1.00 0.00 0.5";
	innerBetweenColor = "1.00 1.00 0.00 0.5";
	innerEndColor	  = "1.00 1.00 0.00 0.5";
	innerStartWidth	= "0.0";
	innerBetweenWidth = "0.05";
	innerEndWidth	  = "0.1";

	hasOuter = false;
	outerStartColor	= "1.00 1.00 0.00 0.0";
	outerBetweenColor = "1.00 1.00 0.00 0.2";
	outerEndColor	  = "1.00 1.00 0.00 0.2";
	outerStartWidth	= "0.0";
	outerBetweenWidth = "0.3";
	outerEndWidth	  = "0.0";

	bitmap = "share/textures/rotc/groundconnection";
	bitmapWidth = 0.35;
//	crossBitmap = "share/shapes/rotc/weapons/OrangeAssaultRifle/lasertail.cross";
//	crossBitmapWidth = 0.25;

	betweenFactor = 0.5;
	blendMode = 1;
};
