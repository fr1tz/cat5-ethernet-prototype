//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

exec("./assaultrifle.sfx.cs");
exec("./assaultrifle.gfx.white.cs");

datablock TracerProjectileData(AssaultriflePseudoProjectile)
{
	energyDrain = 10;
	lifetime = 1000;
	muzzleVelocity = 150 * $Server::Game.slowpokemod;
	velInheritFactor = 0.5 * $Server::Game.slowpokemod;
};

function AssaultriflePseudoProjectile::onAdd(%this, %obj)
{
	%player = %obj.sourceObject;
	%slot = %obj.sourceSlot;
	%image = %player.getMountedImage(%slot);

	%muzzlePoint = %obj.initialPosition;
	%muzzleVec = %obj.initialVelocity;
	%muzzleTransform = createOrientFromDir(VectorNormalize(%muzzleVector));

   %pos = %muzzlePoint;

   %projectile = %image.fireprojectile[0];

	for(%i = 0; %i < 1; %i++)
	{
      %spread = %image.getBulletSpread(%player);
      %randX = %spread * ((getRandom(1000)-500)/1000);
      %vec = %randX SPC "1 0";
      //%vec = "0 1 0";
      %mat = createOrientFromDir(VectorNormalize(%muzzleVec));
      %vel = VectorScale(MatrixMulVector(%mat, %vec), %this.muzzleVelocity);

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

datablock ProjectileData(AssaultrifleActualProjectile)
{
   allowColorization = true;

	stat = "Assaultrifle";

	// script damage properties...
	impactDamage       = 30;
	impactImpulse      = 400;
	splashDamage       = 0;
	splashDamageRadius = 0;
	splashImpulse      = 0;
	bypassDamageBuffer = false;

	trackingAgility = 0;

	explodesNearEnemies			= false;
	explodesNearEnemiesRadius	= 2;
	explodesNearEnemiesMask	  = $TypeMasks::PlayerObjectType;

	//sound = AssaultrifleProjectileSound;

   //projectileShapeName = "share/shapes/rotc/weapons/assaultrifle/projectile2.red.dts";

	explosion             = WhiteAssaultrifleProjectileImpact;
//	bounceExplosion       = AssaultrifleProjectileBounceExplosion;
	hitEnemyExplosion     = WhiteAssaultrifleProjectileHit;
//	nearEnemyExplosion    = AssaultrifleProjectileExplosion;
	hitTeammateExplosion  = WhiteAssaultrifleProjectileHit;
//	hitDeflectorExplosion = DiscDeflectedEffect;

	missEnemyEffectRadius = 10;
	missEnemyEffect = WhiteAssaultrifleProjectileMissedEnemyEffect;

//	particleEmitter = AssaultrifleProjectileParticleEmitter;
	laserTrail[0]			= WhiteAssaultrifleProjectileLaserTrail;
	laserTrailFlags[0]   = 0;
	laserTail	    = WhiteAssaultrifleProjectileLaserTail;
	laserTailLen    = 3.5;

	muzzleVelocity	= 0; // Handled by AssaultriflePseudoProjectile
	velInheritFactor = 0; // Handled by AssaultriflePseudoProjectile

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

function AssaultrifleActualProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
	 if( !(%col.getType() & $TypeMasks::ShapeBaseObjectType) )
		return;
}

//--------------------------------------------------------------------------
// weapon image which does all the work...
// (images do not normally exist in the world, they can only
// be mounted on ShapeBase objects)

datablock ShapeBaseImageData(AssaultrifleImage)
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
	minEnergy = 10;

	projectile = AssaultriflePseudoProjectile;

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
	fireprojectile[0] = AssaultrifleActualProjectile;

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
		stateTransitionOnTriggerUp[3]    = "Ready";
		stateTransitionOnNoAmmo[3]       = "NoAmmo";
		stateTransitionOnTimeout[3]      = "Fire";
		stateTimeoutValue[3]             = 0.10;
		stateFire[3]                     = true;
		stateFireProjectile[3]           = AssaultriflePseudoProjectile;
		stateRecoil[3]                   = MediumRecoil;
		stateAllowImageChange[3]         = false;
		stateEjectShell[3]               = true;
		stateArmThread[3]                = "aimblaster";
		stateSequence[3]                 = "Fire";
		stateSound[3]                    = AssaultrifleFireSound;

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

function AssaultrifleImage::getBulletSpread(%this, %obj)
{
   %speed = VectorLen(%obj.getVelocity());
   return %speed * 0.01 + 0.1;
}

function AssaultrifleImage::onMount(%this, %obj, %slot)
{
    Parent::onMount(%this, %obj, %slot);
}


