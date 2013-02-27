//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// light images...

datablock ShapeBaseImageData(GreenEtherformLightImage)
{
	// basic item properties
	shapeFile = "share/shapes/cat5/misc/nothing.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 4;
	offset = "0 0 0";
	
	// light properties...
	lightType = "ConstantLight";
	lightColor = "0.0 0.2 0.0";
	lightTime = 1000;
	lightRadius = 8;
	lightCastsShadows = true;
	lightAffectsShapes = true;

	stateName[0] = "DoNothing";
};

datablock ShapeBaseImageData(RedEtherformLightImage : GreenEtherformLightImage)
{
	lightColor = "0.2 0.0 0.0";
};

//------------------------------------------------------------------------------
// damage buffer particle emitter...

datablock ParticleData(GreenEtherformDamageBufferEmitter_Particle)
{
	dragCoefficient		= 1.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 500;
	lifetimeVarianceMS	= 0;
	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "1.0 1.0 1.0 0.2";
	colors[2]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 1.0;
	sizes[1]		= 1.0;
	sizes[2]		= 1.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	spinRandomMin = 0.0;
	spinRandomMax = 0.0;
	textureName	= "share/textures/cat5/corona";
	allowLighting = false;
};

datablock ParticleEmitterData(GreenEtherformDamageBufferEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = 5;
	velocityVariance = 0;
	ejectionOffset	= 0.0;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvance  = false;
	orientParticles  = false;
	lifetimeMS		 = 0; // forever
	particles = GreenEtherformDamageBufferEmitter_Particle;
};

//-------------------------------

datablock ParticleData(RedEtherformDamageBufferEmitter_Particle)
{
	dragCoefficient		= 1.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 500;
	lifetimeVarianceMS	= 0;
	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "1.0 1.0 1.0 0.2";
	colors[2]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 1.0;
	sizes[1]		= 1.0;
	sizes[2]		= 1.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	spinRandomMin = 0.0;
	spinRandomMax = 0.0;
	textureName	= "share/textures/cat5/corona";
	allowLighting = false;
};

datablock ParticleEmitterData(RedEtherformDamageBufferEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = 5;
	velocityVariance = 0;
	ejectionOffset	= 0.0;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvance  = false;
	orientParticles  = false;
	lifetimeMS		 = 0; // forever
	particles = RedEtherformDamageBufferEmitter_Particle;
};

//------------------------------------------------------------------------------
// damage repair particle emitter...

datablock ParticleData(GreenEtherformRepairEmitter_Particle)
{
	dragCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 220;
	lifetimeVarianceMS	= 0;
	colors[0]	  = "1.0 1.0 1.0 0.0";
	colors[1]	  = "1.0 1.0 1.0 1.0";
	colors[2]	  = "1.0 1.0 1.0 1.0";
	sizes[0]		= 3.0;
	sizes[1]		= 2.0;
	sizes[2]		= 0.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	spinRandomMin = 0.0;
	spinRandomMax = 0.0;
	textureName	= "share/textures/cat5/corona";
	allowLighting = false;
};

datablock ParticleEmitterData(GreenEtherformRepairEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = -20.0;
	velocityVariance = 0.0;
	ejectionOffset	= 4.0;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvance  = false;
	orientParticles  = false;
	lifetimeMS		 = 0; // forever
	particles = GreenEtherformRepairEmitter_Particle;
};

//-------------------------------

datablock ParticleData(RedEtherformRepairEmitter_Particle)
{
	dragCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 220;
	lifetimeVarianceMS	= 0;
	colors[0]	  = "0.0 1.0 1.0 0.0";
	colors[1]	  = "0.0 1.0 1.0 1.0";
	colors[2]	  = "0.0 1.0 1.0 1.0";
	sizes[0]		= 3.0;
	sizes[1]		= 2.0;
	sizes[2]		= 0.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	spinRandomMin = 0.0;
	spinRandomMax = 0.0;
	textureName	= "share/textures/cat5/corona";
	allowLighting = false;
};

datablock ParticleEmitterData(RedEtherformRepairEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = -20.0;
	velocityVariance = 0.0;
	ejectionOffset	= 4.0;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvance  = false;
	orientParticles  = false;
	lifetimeMS		 = 0; // forever
	particles = RedEtherformRepairEmitter_Particle;
};

//------------------------------------------------------------------------------
// damage buffer repair particle emitter...

datablock ParticleData(GreenEtherformBufferRepairEmitter_Particle)
{
	dragCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 220;
	lifetimeVarianceMS	= 0;
	colors[0]	  = "1.0 1.0 1.0 0.0";
	colors[1]	  = "1.0 1.0 1.0 1.0";
	colors[2]	  = "1.0 1.0 1.0 1.0";
	sizes[0]		= 3.0;
	sizes[1]		= 2.0;
	sizes[2]		= 0.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	spinRandomMin = 0.0;
	spinRandomMax = 0.0;
	textureName	= "share/textures/cat5/corona";
	allowLighting = false;
};

datablock ParticleEmitterData(GreenEtherformBufferRepairEmitter)
{
	ejectionPeriodMS = 500;
	periodVarianceMS = 0;
	ejectionVelocity = -20.0;
	velocityVariance = 0;
	ejectionOffset	= 4.0;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvance  = false;
	orientParticles  = false;
	lifetimeMS		 = 0; // forever
	particles = GreenEtherformBufferRepairEmitter_Particle;
};

//-------------------------------

datablock ParticleData(RedEtherformBufferRepairEmitter_Particle)
{
	dragCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 220;
	lifetimeVarianceMS	= 0;
	colors[0]	  = "1.0 1.0 1.0 0.0";
	colors[1]	  = "1.0 1.0 1.0 1.0";
	colors[2]	  = "1.0 1.0 1.0 1.0";
	sizes[0]		= 3.0;
	sizes[1]		= 2.0;
	sizes[2]		= 0.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	spinRandomMin = 0.0;
	spinRandomMax = 0.0;
	textureName	= "share/textures/cat5/corona";
	allowLighting = false;
};

datablock ParticleEmitterData(RedEtherformBufferRepairEmitter)
{
	ejectionPeriodMS = 500;
	periodVarianceMS = 0;
	ejectionVelocity = -20.0;
	velocityVariance = 0;
	ejectionOffset	= 4.0;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvance  = false;
	orientParticles  = false;
	lifetimeMS		 = 0; // forever
	particles = RedEtherformBufferRepairEmitter_Particle;
};


