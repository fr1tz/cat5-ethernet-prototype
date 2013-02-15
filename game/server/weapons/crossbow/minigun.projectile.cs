//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

exec("./minigun.projectile.sfx.cs");
exec("./minigun.projectile.gfx.white.cs");

//-----------------------------------------------------------------------------
// projectile datablock...

datablock ProjectileData(MinigunProjectile)
{
   allowColorization = true;

	stat = "minigun";

	// script damage properties...
	impactDamage       = 60;
	impactImpulse      = 500;
	splashDamage       = 0;
	splashDamageRadius = 0;
	splashImpulse      = 0;

	energyDrain = 2; // how much energy does firing this projectile drain?

	explodesNearEnemies	      = false;
	explodesNearEnemiesRadius = 10;
	explodesNearEnemiesMask   = $TypeMasks::PlayerObjectType;

	//sound = MinigunProjectileFlybySound;

	//projectileShapeName = "share/shapes/rotc/weapons/blaster/projectile.red.dts";

	explosion               = WhiteMinigunProjectileImpact;
	hitEnemyExplosion       = WhiteMinigunProjectileImpact;
	hitTeammateExplosion    = WhiteMinigunProjectileImpact;
	//nearEnemyExplosion	= DefaultProjectileNearEnemyExplosion;
	//hitDeflectorExplosion = SeekerDiscBounceEffect;

	//fxLight					= WhiteMinigunProjectileFxLight;

	missEnemyEffect = WhiteMinigunProjectileMissedEnemyEffect;

	laserTail    = WhiteMinigunProjectileLaserTail;
	laserTailLen = 8.0;

	//laserTrail[0] = WhiteMinigunProjectileLaserTrail;

	//particleEmitter	  = WhiteMinigunProjectileParticleEmitter;

	muzzleVelocity   = 150 * $Server::Game.slowpokemod;
	velInheritFactor = 1.0 * $Server::Game.slowpokemod;

	isBallistic = false;
	gravityMod  = 4.0 * $Server::Game.slowpokemod;

	armingDelay	= 1000*0;
	lifetime	= 3000;
	fadeDelay	= 5000;

	decals[0] = BulletHoleDecalOne;

	hasLight    = false;
	lightRadius = 10.0;
	lightColor  = "1.0 0.0 0.0";
};

function MinigunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
    Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);

	if( !(%col.getType() & $TypeMasks::ShapeBaseObjectType) )
		return;
}


