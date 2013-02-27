//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

exec("./pumpgun.projectile.cs");
exec("./pumpgun.sfx.cs");

//-----------------------------------------------------------------------------
// fire particle emitter

datablock ParticleData(WhitePumpgunFireEmitter_Particles)
{
	dragCoefficient       = 1;
	gravityCoefficient    = 0.0;
	windCoefficient       = 0.0;
	inheritedVelFactor    = 1.0;
	constantAcceleration  = 0.0;
	lifetimeMS            = 100;
	lifetimeVarianceMS    = 0;
	textureName           = "share/textures/cat5/smoke1";
	colors[0]             = "1.0 1.0 1.0 1.0";
	colors[1]             = "1.0 0.0 0.0 1.0";
	colors[2]             = "1.0 0.0 0.0 0.0";
	sizes[0]              = 0.5;
	sizes[1]              = 0.5;
	sizes[2]              = 0.0;
	times[0]              = 0.0;
	times[1]              = 0.5;
	times[2]              = 1.0;

};

datablock ParticleEmitterData(WhitePumpgunFireEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = 5.0*10;
	velocityVariance = 0.2;
	ejectionOffset   = 0;
	thetaMin         = 0;
	thetaMax         = 0;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvances = false;
	orientParticles  = false;
	lifetimeMS       = 0;
	particles        = "WhitePumpgunFireEmitter_Particles";
};

//------------------------------------------------------------------------------
// weapon image which does all the work...
// (images do not normally exist in the world, they can only
// be mounted on ShapeBase objects)

datablock ShapeBaseImageData(PumpgunImage)
{
	// add the WeaponImage namespace as a parent
	className = WeaponImage;

	// basic item properties
	shapeFile = "share/shapes/cat5/nothing.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 0;
	offset      = "0 0 0";
	rotation    = "0 0 0";
	//eyeOffset	= "0.3 -0.025 -0.15";
	//eyeRotation = "0 0 0";

	// Adjust firing vector to eye's LOS point?
	correctMuzzleVector = true;

	usesEnergy = true;
	minEnergy = 25;

	projectile = PumpgunProjectile;

    // charging...
    minCharge = 0.4;
 
	// script fields...
	iconId = 5;
	armThread = "aimblaster";  // armThread to use when holding this weapon
	crosshair = "blaster"; // crosshair to display when holding this weapon
	
	//-------------------------------------------------
	// image states...
	//
		// preactivation...
		stateName[0]                     = "Preactivate";
		stateTransitionOnAmmo[0]         = "Activate";
		stateTransitionOnNoAmmo[0]       = "NoAmmo";

		// when mounted...
		stateName[1]                     = "Activate";
		stateTransitionOnTimeout[1]      = "Ready";
		stateTimeoutValue[1]             = 0.5;
		stateSequence[1]                 = "activate";
		stateSpinThread[1]               = "SpinDown";

		// ready to fire, just waiting for the trigger...
		stateName[2]                     = "Ready";
		stateTransitionOnNoAmmo[2]       = "NoAmmo";
		stateTransitionOnNotLoaded[2]    = "Disabled";
		stateTransitionOnTriggerDown[2]  = "Fire";
		stateArmThread[2]                = "aimblaster";
		stateSpinThread[2]               = "Stop";
		//stateSequence[2]                 = "idle";

		// fire!...
		stateName[3]                     = "Fire";
		stateTransitionOnTimeout[3]      = "Reload";
		stateTimeoutValue[3]             = 0.6;
		stateFire[3]                     = true;
		stateFireProjectile[3]           = PumpgunProjectile;
		stateRecoil[3]                   = MediumRecoil;
		stateAllowImageChange[3]         = false;
		stateSequence[3]                 = "fire";
		stateSound[3]                    = PumpgunFireSound;
		stateEmitter[3]                  = PumpgunFireEmitter;
		stateEmitterNode[3]              = "fireparticles";
		stateEmitterTime[3]              = 0.1;
		stateSpinThread[3]               = "Stop";
		stateScript[3]                   = "onFire";

		// reload
		stateName[4]                     = "Reload";
		stateTransitionOnTimeout[4]      = "Ready";
		stateTimeoutValue[4]             = 0.6;
		stateAllowImageChange[4]         = false;
		stateEjectShell[4]               = true;
		stateSound[4]                    = PumpgunReloadSound;

		// no ammo...
		stateName[6]                     = "NoAmmo";
      stateTransitionOnTriggerDown[6]  = "DryFire";
		stateTransitionOnAmmo[6]         = "Ready";
		stateTimeoutValue[6]             = 0.50;
		//stateSequence[6]                 = "idle";
  
        // dry fire...
		stateName[7]                     = "DryFire";
		stateTransitionOnTriggerUp[7]    = "NoAmmo";
		stateSound[7]                    = WeaponEmptySound;
		//stateSequence[7]                 = "idle";

		// disabled...
		stateName[8]                     = "Disabled";
		stateTransitionOnLoaded[8]       = "Ready";
		stateAllowImageChange[8]         = false;
		//stateSequence[8]                 = "idle";
	//
	// ...end of image states
	//-------------------------------------------------
};

function PumpgunImage::getBulletSpread(%this, %obj)
{
   return 0.07;
}

function PumpgunImage::onMount(%this, %obj, %slot)
{
   Parent::onMount(%this, %obj, %slot);
}

