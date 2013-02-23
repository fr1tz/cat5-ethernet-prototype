//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

exec("./minigun.projectile.cs");
exec("./minigun.tracer.cs");

exec("./minigun.sfx.cs");

//-----------------------------------------------------------------------------
// fire particle emitter

datablock ParticleData(MinigunFireEmitter_Particles)
{
	dragCoefficient       = 1;
	gravityCoefficient    = 0.0;
	windCoefficient       = 0.0;
	inheritedVelFactor    = 0.5;
	constantAcceleration  = 0.0;
	lifetimeMS            = 50;
	lifetimeVarianceMS    = 0;
	textureName           = "share/textures/rotc/smoke_particle";
	colors[0]             = "1.0 1.0 1.0 1.0";
	colors[1]             = "1.0 1.0 1.0 1.0";
	colors[2]             = "1.0 1.0 1.0 0.0";
	sizes[0]              = 0.3;
	sizes[1]              = 0.3;
	sizes[2]              = 0.0;
	times[0]              = 0.0;
	times[1]              = 0.5;
	times[2]              = 1.0;

};

datablock ParticleEmitterData(MinigunFireEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = 20.0;
	velocityVariance = 0.2;
	ejectionOffset   = 0;
	thetaMin         = 0;
	thetaMax         = 0;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvances = false;
	orientParticles  = false;
	lifetimeMS       = 0;
	particles        = "MinigunFireEmitter_Particles";
};

//------------------------------------------------------------------------------
// weapon image which does all the work...
// (images do not normally exist in the world, they can only
// be mounted on ShapeBase objects)

datablock ShapeBaseImageData(MinigunImage)
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
	//eyeOffset   = "0.275 -0.25 -0.2";
	//eyeRotation = "0 0 0";

	// Adjust firing vector to eye's LOS point?
	correctMuzzleVector = true;

	usesEnergy = true;
	minEnergy = 1;

	projectile = MinigunProjectile;

	// script fields...
	armThread = "aimblaster";  // armThread to use when holding this weapon
	
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
		stateSequence[1]                 = "idle";
		stateSpinThread[1]               = "SpinDown";

		// ready to fire, just waiting for the trigger...
		stateName[2]                     = "Ready";
		stateTransitionOnNoAmmo[2]       = "NoAmmo";
		stateTransitionOnNotLoaded[2]    = "Disabled";
		stateTransitionOnTriggerDown[2]  = "Charge";
		stateArmThread[2]                = "aimblaster";
		stateSpinThread[2]               = "Stop";
		stateSequence[2]                 = "idle";
		stateScript[2]                   = "onReady";

		// charge...
		stateName[3]                     = "Charge";
		stateTransitionOnNoAmmo[3]       = "NoAmmo";
		stateTransitionOnTriggerUp[3]    = "Cooldown";
		stateTransitionOnTimeout[3]      = "Spin";
		stateTimeoutValue[3]             = 0.5;
      stateEnergyDrain[3]              = 50;
		stateAllowImageChange[3]         = false;
		stateArmThread[3]                = "aimblaster";
		stateSound[3]                    = MinigunSpinUpSound;
		stateSpinThread[3]               = "SpinUp";
		stateScript[3]                   = "onCharge";

		stateName[9]                     = "Spin";
		stateTransitionOnTriggerUp[9]    = "Cooldown";
		stateTransitionOnTimeout[9]      = "Fire";
		stateTimeoutValue[9]             = 0.1;
      stateEnergyDrain[9]              = 20;
		stateAllowImageChange[9]         = false;
		stateArmThread[9]                = "aimblaster";
		stateSound[9]                    = MinigunSpinSound;
		stateSpinThread[9]               = "FullSpeed";

		// fire!...
		stateName[4]                     = "Fire";
		stateTransitionOnTimeout[4]      = "Fire";
		stateTransitionOnTriggerUp[4]    = "Cooldown";
		stateTransitionOnNoAmmo[4]       = "Cooldown";
		stateTimeoutValue[4]             = 0.075; 
      stateEnergyDrain[4]              = 20;
		stateFire[4]                     = true;
		stateFireProjectile[4]           = MinigunProjectile;
		stateAllowImageChange[4]         = false;
		stateEjectShell[4]               = true;
		stateArmThread[4]                = "aimblaster";
		stateSequence[4]                 = "fire";
		stateSequenceRandomFlash[4]      = true;
		stateSound[4]                    = MinigunSpinSound;
		stateEmitter[4]                  = MinigunFireEmitter;
		stateEmitterNode[4]              = "fireparticles";
		stateEmitterTime[4]              = 0.1;
		stateSpinThread[4]               = "FullSpeed";
		stateScript[4]                   = "onFire";

		// cooldown...
		stateName[5]                     = "Cooldown";
		stateTransitionOnTimeout[5]      = "Ready";
		stateTimeoutValue[5]             = 0.5;
		stateAllowImageChange[5]         = false;
		stateEjectShell[5]               = true;
		stateArmThread[5]                = "aimblaster";
		stateSound[5]                    = MinigunSpinDownSound;
		stateSpinThread[5]               = "SpinDown";
		stateScript[5]                   = "onCooldown";
		
		// no ammo...
		stateName[6]                     = "NoAmmo";
      stateTransitionOnTriggerDown[6]  = "DryFire";
		stateTransitionOnAmmo[6]         = "Ready";
		stateTimeoutValue[6]             = 0.50;
		stateSequence[6]                 = "idle";
  
        	// dry fire...
		stateName[7]                     = "DryFire";
		stateTransitionOnTriggerUp[7]    = "NoAmmo";
		stateSound[7]                    = WeaponEmptySound;

		// disabled...
		stateName[8]                     = "Disabled";
		stateTransitionOnLoaded[8]       = "Ready";
		stateAllowImageChange[8]         = false;
		stateSequence[8]                 = "idle";		
	//
	// ...end of image states
	//-------------------------------------------------
};

function MinigunImage::getBulletSpread(%this, %obj)
{
   return 0.015;
}

function MinigunImage::onReady(%this, %obj, %slot)
{
   %obj.zCurrentMinigunTracer = -1;
   %obj.setSniping(false);
}

function MinigunImage::onCharge(%this, %obj, %slot)
{
	//error("Minigun: onCharge()");
    %obj.setSniping(true);
}

function MinigunImage::onFire(%this, %obj, %slot)
{
   return;

   %obj.zCurrentMinigunTracer += 1;

   if(!(%obj.zCurrentMinigunTracer % 4 == 0))
      return;

	%projectile = MinigunTracer;

	// determine muzzle-point...
	%muzzlePoint = %obj.getMuzzlePoint(%slot);

	// determine initial projectile velocity...
	%muzzleVector = %obj.getMuzzleVector(%slot);

	%objectVelocity = %obj.getVelocity();
	%muzzleVelocity = VectorAdd(
		VectorScale(%muzzleVector, %projectile.muzzleVelocity),
		VectorScale(%objectVelocity, %projectile.velInheritFactor));

	// create the projectile object...
	%p = new Projectile() {
		dataBlock       = %projectile;
		teamId          = %obj.teamId;
		initialVelocity = %muzzleVelocity;
		initialPosition = %muzzlePoint;
		sourceObject    = %obj;
		sourceSlot      = %slot;
		client	    = %obj.client;
	};
	MissionCleanup.add(%p);

	return %p;
}

function MinigunImage::onCooldown(%this, %obj, %slot)
{
   %obj.setSniping(false);
}


