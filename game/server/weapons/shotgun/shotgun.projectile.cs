//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

exec("./shotgun.projectile.sfx.cs");
exec("./shotgun.projectile.gfx.red.cs");
exec("./shotgun.projectile.gfx.blue.cs");

//-----------------------------------------------------------------------------
// projectile datablock...

datablock ShotgunProjectileData(RedShotgunProjectile)
{
	stat = "blaster";

	// script damage properties...
	impactDamage        = 20;
   impactDamageFalloff = $ImpactDamageFalloff::Linear;
   impactDamageMaxDist = 100;
	impactImpulse       = 100;
	splashDamage        = 0;
	splashDamageRadius  = 0;
	splashImpulse       = 0;

	energyDrain = 3; // how much energy does firing this projectile drain?

	numBullets = 9; // number of shotgun bullets

	range = 100; // shotgun range
	muzzleSpreadRadius = 0.5;
	referenceSpreadRadius = 1.0;
	referenceSpreadDistance = 25;

	explodesNearEnemies	      = false;
	explodesNearEnemiesRadius = 4;
	explodesNearEnemiesMask   = $TypeMasks::PlayerObjectType;

	//sound = ShotgunProjectileFlybySound;

	//projectileShapeName = "share/shapes/rotc/weapons/blaster/projectile.red.dts";

	explosion               = RedShotgunProjectileImpact;
	hitEnemyExplosion       = RedShotgunProjectileHit;
	hitTeammateExplosion    = RedShotgunProjectileHit;
	//nearEnemyExplosion	= DefaultProjectileNearEnemyExplosion;
	//hitDeflectorExplosion = SeekerDiscBounceEffect;

	//fxLight					= RedShotgunProjectileFxLight;

	missEnemyEffect		 = RedShotgunProjectileMissedEnemyEffect;

	//laserTail				 = RedShotgunProjectileLaserTail;
	//laserTailLen			 = 10.0;

	laserTrail[0]			= RedShotgunProjectileLaserTrail;
	laserTrailFlags[0]   = 1;
	laserTrail[1]			= RedShotgunProjectileLaserTrail;
	laserTrailFlags[1]   = 1;

	//particleEmitter	  = RedShotgunProjectileParticleEmitter;

	muzzleVelocity   = 9999;
	velInheritFactor = 0.0;

	isBallistic			= false;
	gravityMod			 = 10.0;

	armingDelay			= 1000*0;
	lifetime				= 3000;
	fadeDelay			  = 5000;

	decals[0] = BulletHoleDecalOne;

	hasLight    = false;
	lightRadius = 10.0;
	lightColor  = "1.0 0.0 0.0";
};

function RedShotgunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);

   return;

	if( !(%col.getType() & $TypeMasks::ShapeBaseObjectType) )
		return;

    %src =  %obj.getSourceObject();
    if(!%src)
        return;

    %currTime = getSimTime();

	// FIXME: strange linux version bug:
	//        after the game has been running a long time
	//        (%currTime == %obj.hitTime)
	//        often evaluates to false even if the
	//        values appear to be equal.
	//        (%currTime - %obj.hitTime) evaluates to 1
	//        in those cases.
    //if(%currTime == %obj.hitTime)
	if(%currTime - %obj.hitTime <= 1)
    {
        %col.numShotgunBulletHits += 1;
        if(%col.numShotgunBulletHits == 4)
            %src.setDiscTarget(%col);
    }
    else
    {
        %obj.hitTime = %currTime;
        %col.numShotgunBulletHits = 1;
    }
}

//-----------------------------------------------------------------------------

datablock ShotgunProjectileData(BlueShotgunProjectile : RedShotgunProjectile)
{
	//projectileShapeName = "share/shapes/rotc/weapons/blaster/projectile.blue.dts";

	explosion            = BlueShotgunProjectileImpact;
	hitEnemyExplosion    = BlueShotgunProjectileHit;
	hitTeammateExplosion = BlueShotgunProjectileHit;

	missEnemyEffect    = BlueShotgunProjectileMissedEnemyEffect;

	//laserTail          = BlueShotgunProjectileLaserTail;

	laserTrail[0]      = BlueShotgunProjectileLaserTrail;
	laserTrail[1]      = BlueShotgunProjectileLaserTrail;

	lightColor  = "0.0 0.0 1.0";
};

function BlueShotgunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    RedShotgunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}
