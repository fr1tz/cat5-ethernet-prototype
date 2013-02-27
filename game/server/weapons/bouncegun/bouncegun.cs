//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

exec("./bouncegun.sfx.cs");
exec("./bouncegun.gfx.cs");
exec("./bouncegun.gfx.white.cs");

datablock TracerProjectileData(BounceGunPseudoProjectile)
{
	lifetime = 1000;

   // Keep these three in sync with the real projectiles down below!
	energyDrain = 15;
	muzzleVelocity = 50 * $Server::Game.slowpokemod;
	velInheritFactor = 0.0 * $Server::Game.slowpokemod;
};

function BounceGunPseudoProjectile::onAdd(%this, %obj)
{
	%player = %obj.sourceObject;
	%slot = %obj.sourceSlot;
	%image = %player.getMountedImage(%slot);

	%muzzlePoint = %obj.initialPosition;
	%muzzleVector = %obj.initialVelocity;
	%muzzleTransform = createOrientFromDir(VectorNormalize(%muzzleVector));
	
	%pos[0] = "0 0 0";
	%vec[0] = "0 1 0";
	%pos[1] = "0 0 0";
	%vec[1] = "0 1 0";
	
	for(%i = 0; %i < 1; %i++)
	{
		%projectile = %image.fireprojectile[%i];
	
		%position =	VectorAdd(
			%muzzlePoint, 
			MatrixMulVector(%muzzleTransform, %pos[%i])
		);		
		%velocity = VectorScale(MatrixMulVector(%muzzleTransform, %vec[%i]), %this.muzzleVelocity);

		// create the projectile object...
		%p = new Projectile() {
			dataBlock       = %projectile;
			teamId          = %player.teamId;
			initialVelocity = %velocity;
			initialPosition = %position;
			sourceObject    = %player;
			sourceSlot      = %slot;
			client          = %player.client;
		};
		MissionCleanup.add(%p);
	}	
        
	// no need to ghost pseudo projectile to clients...
	%obj.delete();
}

//-----------------------------------------------------------------------------
// projectile datablock...

datablock ProjectileData(BounceGunProjectile)
{
   allowColorization = true;

	stat = "br";

	// script damage properties...
	impactDamage       = 40;
	impactImpulse      = 500;
	splashDamage       = 0;
	splashDamageRadius = 0;
	splashImpulse      = 0;
	bypassDamageBuffer = false;
	
	trackingAgility = 0;
	
	explodesNearEnemies			= false;
	explodesNearEnemiesRadius	= 0.5;
	explodesNearEnemiesMask	  = $TypeMasks::PlayerObjectType;

	//sound = BounceGunProjectileSound;
 
   projectileShapeName = "share/shapes/cat5/bounceprojectile.dts";

	explosion             = WhiteBounceGunProjectileExplosion;
	bounceExplosion       = WhiteBounceGunProjectileExplosion;
//	hitEnemyExplosion     = WhiteBounceGunProjectileExplosion;
//	nearEnemyExplosion    = WhiteBounceGunProjectileExplosion;
//	hitTeammateExplosion  = WhiteBounceGunProjectileExplosion;
//	hitDeflectorExplosion = DiscDeflectedEffect;

	missEnemyEffectRadius = 10;
	missEnemyEffect = BounceGunProjectileMissedEnemyEffect;

	particleEmitter = WhiteBounceGunProjectileParticleEmitter;
//	laserTrail[0]   = BounceGunProjectileLaserTrail;
//	laserTrail[1]   = GreenBounceGunProjectileLaserTrail;
	laserTail	    = WhiteBounceGunProjectileLaserTail;
	laserTailLen    = 2;

	posOffset = "0 0 0";
	velOffset = "0 0.005";

   // Keep these three in sync with the pseudo projectile above!
	energyDrain = 15;
	muzzleVelocity	= 50 * $Server::Game.slowpokemod;
	velInheritFactor = 0.0 * $Server::Game.slowpokemod;
	
	isBallistic = false;
	gravityMod  = 5.0 * $Server::Game.slowpokemod;

	armingDelay			= 0;
	lifetime				= 1000*5;
	fadeDelay			  = 5000;
	
	numBounces = 2;
	
	decals[0] = ExplosionDecalTwo;
	
	bounceDecals[0] = SmashDecalOne;
	bounceDecals[1] = SmashDecalTwo;
	bounceDecals[2] = SmashDecalThree;
	bounceDecals[3] = SmashDecalFour;
	
	hasLight	 = true;
	lightRadius = 4.0;
	lightColor  = "0.4 0.4 0.4";
};

function BounceGunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

//--------------------------------------------------------------------------
// weapon image which does all the work...
// (images do not normally exist in the world, they can only
// be mounted on ShapeBase objects)

datablock ShapeBaseImageData(BounceGunImage)
{
	// add the WeaponImage namespace as a parent
	className = WeaponImage;
	
	// basic item properties
   shapeFile = "share/shapes/cat5/nothing.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 0;
	offset		= "0 0 0";
	rotation	 = "0 0 0";
	//eyeOffset	= "0.275 0.1 -0.05";
	//eyeRotation = "0 0 0 0";

	// Adjust firing vector to eye's LOS point?
	correctMuzzleVector = true;

	usesEnergy = true;
	minEnergy = 15;

	projectile = BounceGunPseudoProjectile;

	// light properties...
	lightType = "WeaponFireLight";
	lightColor = "1 0.5 0";
	lightTime = 100;
	lightRadius = 15;
	lightCastsShadows = false;
	lightAffectsShapes = true;

	// script fields...
	iconId = 7;
	mainWeapon = true;
	armThread  = "aimrifle";  // armThread to use when holding this weapon
	crosshair  = "assaultrifle"; // crosshair to display when holding this weapon
	fireprojectile[0] = BounceGunProjectile;
	//fireprojectile[1] = BounceGunProjectile;

	//-------------------------------------------------
	// image states...
	//
		// preactivation...
		stateName[0]                     = "Preactivate";
		stateTransitionOnAmmo[0]         = "Activate";
		stateTransitionOnNoAmmo[0]		 = "NoAmmo";
		stateTimeoutValue[0]             = 0.25;
		stateSequence[0]                 = "idle";

		// when mounted...
		stateName[1]                     = "Activate";
		stateTransitionOnTimeout[1]      = "Ready";
		stateTimeoutValue[1]             = 0.25;
		stateSequence[1]                 = "idle";
		stateSpinThread[1]               = "SpinUp";

		// ready to fire, just waiting for the trigger...
		stateName[2]                     = "Ready";
		stateTransitionOnNoAmmo[2]       = "NoAmmo";
  		stateTransitionOnNotLoaded[2]    = "Disabled";
		stateTransitionOnTriggerDown[2]  = "Fire";
      stateArmThread[2]                = "aimrifle";
		stateSpinThread[2]               = "FullSpeed";
		stateSequence[2]                 = "idle";
		
		stateName[3]                     = "Fire";
		stateTransitionOnTriggerUp[3]    = "KeepAiming";
		stateTimeoutValue[3]             = 0.5;
		stateFire[3]                     = true;
		stateFireProjectile[3]           = BounceGunPseudoProjectile;
		stateRecoil[3]                   = MediumRecoil;
		stateAllowImageChange[3]         = false;
		stateEjectShell[3]               = true;
		stateArmThread[3]                = "aimrifle";
		stateSequence[3]                 = "Fire";
		stateSound[3]                    = BounceGunFireSound;

		stateName[4]                     = "KeepAiming";
		stateTransitionOnNoAmmo[4]       = "NoAmmo";
		stateTransitionOnNotLoaded[4]    = "Disabled";
		stateTransitionOnTriggerDown[4]  = "Fire";
		stateTransitionOnTimeout[4]      = "Ready";
		stateWaitForTimeout[4]           = false;
		stateTimeoutValue[4]             = 2.00;

        // no ammo...
		stateName[5]                     = "NoAmmo";
		stateTransitionOnAmmo[5]         = "Ready";
        stateTransitionOnTriggerDown[5]  = "DryFire";
		stateTimeoutValue[5]             = 0.50;
  
        // dry fire...
		stateName[6]                     = "DryFire";
		stateTransitionOnTriggerUp[6]    = "NoAmmo";
		stateSound[6]                    = WeaponEmptySound;
  
		// disabled...
		stateName[7]                     = "Disabled";
		stateTransitionOnLoaded[7]       = "Ready";
		stateAllowImageChange[7]         = false;
	//
	// ...end of image states
	//-------------------------------------------------
};

function BounceGunImage::onMount(%this, %obj, %slot)
{
   Parent::onMount(%this, %obj, %slot);

   // Set up recoil
   %obj.setImageRecoilEnabled(%slot, true);
   %obj.setImageCurrentRecoil(%slot, 80);
   %obj.setImageMaxRecoil(%slot, 80);
   %obj.setImageRecoilAdd(%slot, 0);
   %obj.setImageRecoilDelta(%slot, -0);
}

