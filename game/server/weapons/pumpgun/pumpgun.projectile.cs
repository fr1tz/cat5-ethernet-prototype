//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

exec("./pumpgun.projectile.sfx.cs");
exec("./pumpgun.projectile.gfx.white.cs");

//-----------------------------------------------------------------------------
// projectile datablock...

datablock ShotgunProjectileData(PumpgunProjectile)
{
   allowColorization = true;

	stat = "pumpgun";

	// script damage properties...
	impactDamage        = 130;
   impactDamageFalloff = $ImpactDamageFalloff::Linear;
   impactDamageMaxDist = 100;
	impactImpulse       = 100;
	splashDamage        = 0;
	splashDamageRadius  = 0;
	splashImpulse       = 0;

	energyDrain = 25; // how much energy does firing this projectile drain?

	numBullets = 1; // 9 // number of shotgun bullets

	range = 75; // shotgun range
	muzzleSpreadRadius = 0.0;
	referenceSpreadRadius = 0.0;
	referenceSpreadDistance = 25;

	explodesNearEnemies	      = false;
	explodesNearEnemiesRadius = 4;
	explodesNearEnemiesMask   = $TypeMasks::PlayerObjectType;

	//sound = PumpgunProjectileFlybySound;

	//projectileShapeName = "share/shapes/rotc/weapons/blaster/projectile.red.dts";

	explosion               = WhitePumpgunProjectileImpact;
	hitEnemyExplosion       = WhitePumpgunProjectileHit;
	hitTeammateExplosion    = WhitePumpgunProjectileHit;
	//nearEnemyExplosion	= DefaultProjectileNearEnemyExplosion;
	//hitDeflectorExplosion = SeekerDiscBounceEffect;

	//fxLight					= WhitePumpgunProjectileFxLight;

	missEnemyEffect		 = WhitePumpgunProjectileMissedEnemyEffect;

	//laserTail				 = WhitePumpgunProjectileLaserTail;
	//laserTailLen			 = 10.0;

	laserTrail[0]			= WhitePumpgunProjectileLaserTrailTwo;
	laserTrailFlags[0]   = 1;

	//particleEmitter	  = WhitePumpgunProjectileParticleEmitter;

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

function PumpgunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}


