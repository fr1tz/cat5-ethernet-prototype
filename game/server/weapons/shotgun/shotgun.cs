//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

exec("./shotgun.sfx.cs");
exec("./shotgun.gfx.white.cs");

datablock TracerProjectileData(ShotgunPseudoProjectile)
{
	energyDrain = 40;
	lifetime = 1000;
	muzzleVelocity = 100 * $Server::Game.slowpokemod;
	velInheritFactor = 0.0 * $Server::Game.slowpokemod;
};

function ShotgunPseudoProjectile::onAdd(%this, %obj)
{
	%player = %obj.sourceObject;
	%slot = %obj.sourceSlot;
	%image = %player.getMountedImage(%slot);

	%muzzlePoint = %obj.initialPosition;
	%muzzleVec = %obj.initialVelocity;
	%muzzleTransform = createOrientFromDir(VectorNormalize(%muzzleVector));

   %pos = %muzzlePoint;

   %projectile = %image.fireprojectile[0];

	for(%i = 0; %i < 9; %i++)
	{
      %spread = %image.getBulletSpread(%player);
      %randX = %spread * ((getRandom(1000)-500)/1000);
      %vec = %randX SPC "1 0";
      %mat = createOrientFromDir(VectorNormalize(%muzzleVec));
      %vel = VectorScale(MatrixMulVector(%mat, %vec), %this.muzzleVelocity);
      %pos = VectorAdd(%pos, VectorScale(VectorNormalize(%vel),getRandom(1000)/1000));
      %pos = VectorSub(%pos, VectorScale(VectorNormalize(%vel),0.5));

		// create the projectile object...
		%p = new Projectile() {
			dataBlock       = %projectile;
			teamId          = %obj.teamId;
			initialVelocity = %vel;
			initialPosition = %pos;
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

datablock ProjectileData(ShotgunActualProjectile)
{
   allowColorization = true;

	stat = "shotgun";

	// script damage properties...
	impactDamage       = 21;
	impactImpulse      = 400;
	splashDamage       = 0;
	splashDamageRadius = 0;
	splashImpulse      = 0;
	bypassDamageBuffer = false;

	trackingAgility = 0;

	explodesNearEnemies			= false;
	explodesNearEnemiesRadius	= 2;
	explodesNearEnemiesMask	  = $TypeMasks::PlayerObjectType;

	//sound = ShotgunProjectileSound;

   //projectileShapeName = "share/shapes/rotc/weapons/assaultrifle/projectile2.red.dts";

	explosion             = WhiteShotgunProjectileImpact;
//	bounceExplosion       = ShotgunProjectileBounceExplosion;
	hitEnemyExplosion     = WhiteShotgunProjectileHit;
//	nearEnemyExplosion    = ShotgunProjectileExplosion;
	hitTeammateExplosion  = WhiteShotgunProjectileHit;
//	hitDeflectorExplosion = DiscDeflectedEffect;

	missEnemyEffectRadius = 10;
	missEnemyEffect = WhiteShotgunProjectileMissedEnemyEffect;

//	particleEmitter = ShotgunProjectileParticleEmitter;
//	laserTrail[0]   = ShotgunProjectileLaserTrail;
//	laserTrail[1]   = ShotgunProjectileLaserTrail;
	laserTail	    = WhiteShotgunProjectileLaserTail;
	laserTailLen    = 3.5;

	muzzleVelocity	= 0; // Handled by ShotgunPseudoProjectile
	velInheritFactor = 0; // Handled by ShotgunPseudoProjectile

	isBallistic			= false;
	gravityMod			 = 1.0 * $Server::Game.slowpokemod;

	armingDelay			= 0;
	lifetime		   	= 1500;
	fadeDelay			= 0;

	numBounces = 0;

	decals[0] = BulletHoleDecalOne;

	bounceDecals[0] = SmashDecalOne;
	bounceDecals[1] = SmashDecalTwo;
	bounceDecals[2] = SmashDecalThree;
	bounceDecals[3] = SmashDecalFour;

	hasLight	 = true;
	lightRadius = 2.0;
	lightColor  = "0.05 0.05 0.05";
};

function ShotgunActualProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
	 if( !(%col.getType() & $TypeMasks::ShapeBaseObjectType) )
		return;
}

//--------------------------------------------------------------------------
// weapon image which does all the work...
// (images do not normally exist in the world, they can only
// be mounted on ShapeBase objects)

datablock ShapeBaseImageData(ShotgunImage)
{
	// add the WeaponImage namespace as a parent
	className = WeaponImage;

	// basic item properties
	shapeFile = "share/shapes/rotc/weapons/blaster/image2.red.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 0;
	offset      = "0 0 0";
	rotation    = "0 0 0";
	eyeOffset	= "0.3 -0.025 -0.15";
	eyeRotation = "0 0 0";

	// Adjust firing vector to eye's LOS point?
	correctMuzzleVector = true;

	usesEnergy = true;
	minEnergy = 40;

	projectile = ShotgunPseudoProjectile;

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
	armThread  = "aimblaster";  // armThread to use when holding this weapon
	crosshair  = "assaultrifle"; // crosshair to display when holding this weapon
	fireprojectile[0] = ShotgunActualProjectile;

	//-------------------------------------------------
	// image states...
	//
		// preactivation...
		stateName[0]                     = "Preactivate";
		stateTransitionOnAmmo[0]         = "Activate";
		stateTransitionOnNoAmmo[0]		 = "NoAmmo";

		// when mounted...
		stateName[1]                     = "Activate";
		stateTransitionOnTimeout[1]      = "Ready";
		stateTimeoutValue[1]             = 0.5;
		stateSequence[1]                 = "idle";
		stateSpinThread[1]               = "SpinUp";

		// ready to fire, just waiting for the trigger...
		stateName[2]                     = "Ready";
		stateTransitionOnNoAmmo[2]       = "NoAmmo";
  		stateTransitionOnNotLoaded[2]    = "Disabled";
		stateTransitionOnTriggerDown[2]  = "Fire";
      stateArmThread[2]                = "aimblaster";
		stateSpinThread[2]               = "FullSpeed";
		stateSequence[2]                 = "idle";

		stateName[3]                     = "Fire";
		stateTimeoutValue[3]             = 0.00;
		stateTransitionOnTimeout[3]      = "AfterFire";
		stateFire[3]                     = true;
		stateFireProjectile[3]           = ShotgunPseudoProjectile;
		stateRecoil[3]                   = MediumRecoil;
		stateAllowImageChange[3]         = false;
		stateEjectShell[3]               = true;
		stateArmThread[3]                = "aimblaster";
		stateSequence[3]                 = "Fire";
		stateSound[3]                    = ShotgunFireSound;

		// after fire...
		stateName[8]                     = "AfterFire";
		stateTransitionOnTriggerUp[8]    = "KeepAiming";

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
		stateSequence[5]                 = "idle";

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

function ShotgunImage::getBulletSpread(%this, %obj)
{
   return 0.3;
}

function ShotgunImage::onMount(%this, %obj, %slot)
{
    Parent::onMount(%this, %obj, %slot);
}


