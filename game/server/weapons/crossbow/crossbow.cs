//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

exec("./crossbow.sfx.cs");
exec("./crossbow.gfx.white.cs");

datablock TracerProjectileData(CrossbowPseudoProjectile)
{
	lifetime = 1000;

   // Keep these three in sync with the real projectiles down below!
	energyDrain = 50;
	muzzleVelocity = 150 * $Server::Game.slowpokemod;
	velInheritFactor = 0.0 * $Server::Game.slowpokemod;
};

function CrossbowPseudoProjectile::onAdd(%this, %obj)
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

datablock ProjectileData(CrossbowProjectile)
{
   allowColorization = true;

	stat = "crossbow";

	// script damage properties...
	impactDamage       = 0;
	impactImpulse      = 0;
	splashDamage       = 0;
	splashDamageRadius = 0;
	splashImpulse      = 0;
	bypassDamageBuffer = false;
	
	trackingAgility = 0;
	
	explodesNearEnemies			= true;
	explodesNearEnemiesRadius	= 2;
	explodesNearEnemiesMask	  = $TypeMasks::PlayerObjectType;

	//sound = CrossbowProjectileSound;
 
// projectileShapeName = "share/shapes/cat5/bounceprojectile.dts";

	explosion             = WhiteCrossbowProjectileImpact;
//	bounceExplosion       = WhiteCrossbowProjectileBounceExplosion;
	hitEnemyExplosion     = WhiteCrossbowProjectileHit;
//	nearEnemyExplosion    = WhiteCrossbowProjectileExplosion;
	hitTeammateExplosion  = WhiteCrossbowProjectileHit;
//	hitDeflectorExplosion = DiscDeflectedEffect;

	missEnemyEffectRadius = 10;
	missEnemyEffect = CrossbowProjectileMissedEnemyEffect;

//	particleEmitter = WhiteCrossbowProjectileParticleEmitter;
//	laserTrail[0]   = CrossbowProjectileLaserTrail;
//	laserTrail[1]   = GreenCrossbowProjectileLaserTrail;
	laserTail	    = WhiteCrossbowProjectileLaserTail;
	laserTailLen    = 12;

   // Keep these three in sync with the pseudo projectile above!
	energyDrain = 50;
	muzzleVelocity	= 150 * $Server::Game.slowpokemod;
	velInheritFactor = 0.0 * $Server::Game.slowpokemod;
	
	isBallistic = false;
	gravityMod  = 5.0 * $Server::Game.slowpokemod;

	armingDelay			= 0;
	lifetime				= 1000*5;
	fadeDelay			  = 2500;
	
	//numBounces = 2;
	
	//decals[0] = ExplosionDecalTwo;
	
	//bounceDecals[0] = SmashDecalOne;
	//bounceDecals[1] = SmashDecalTwo;
	//bounceDecals[2] = SmashDecalThree;
	//bounceDecals[3] = SmashDecalFour;
	
	hasLight	 = false;
	lightRadius = 4.0;
	lightColor  = "0.4 0.4 0.4";
};

function CrossbowProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
   Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);

	if( !(%col.getType() & $TypeMasks::ShapeBaseObjectType) )
		return;

   %col.setDamageDt(1, $DamageType::Impact);
}

//--------------------------------------------------------------------------
// weapon image which does all the work...
// (images do not normally exist in the world, they can only
// be mounted on ShapeBase objects)

datablock ShapeBaseImageData(CrossbowImage)
{
	// add the WeaponImage namespace as a parent
	className = WeaponImage;
	
	// basic item properties
	shapeFile = "share/shapes/rotc/weapons/assaultrifle/image3.red.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 0;
	offset		= "0 0 0";
	rotation	 = "0 0 0";
	eyeOffset	= "0.275 0.1 -0.05";
	eyeRotation = "0 0 0 0";

	// Adjust firing vector to eye's LOS point?
	correctMuzzleVector = true;

	usesEnergy = true;
	minEnergy = 50;

	projectile = CrossbowPseudoProjectile;

	// light properties...
	//lightType = "WeaponFireLight";
	//lightColor = "1 0.5 0";
	//lightTime = 100;
	//lightRadius = 15;
	//lightCastsShadows = false;
	//lightAffectsShapes = true;

	// script fields...
	iconId = 7;
	mainWeapon = true;
	armThread  = "aimrifle";  // armThread to use when holding this weapon
	crosshair  = "assaultrifle"; // crosshair to display when holding this weapon
	fireprojectile[0] = CrossbowProjectile;
	//fireprojectile[1] = CrossbowProjectile;

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
		stateTimeoutValue[3]             = 1.0;
		stateFire[3]                     = true;
		stateFireProjectile[3]           = CrossbowPseudoProjectile;
		stateRecoil[3]                   = MediumRecoil;
		stateAllowImageChange[3]         = false;
		stateEjectShell[3]               = true;
		stateArmThread[3]                = "aimrifle";
		stateSequence[3]                 = "Fire";
		stateSound[3]                    = CrossbowFireSound;

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

function CrossbowImage::onMount(%this, %obj, %slot)
{
   Parent::onMount(%this, %obj, %slot);
}

