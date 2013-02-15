//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

exec("./minigun.tracer.gfx.white.cs");

//-----------------------------------------------------------------------------
// projectile datablock...

datablock ProjectileData(MinigunTracer)
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

	//explosion               = WhiteMinigunProjectileImpact;
	//hitEnemyExplosion       = WhiteMinigunProjectileImpact;
	//hitTeammateExplosion    = WhiteMinigunProjectileImpact;
	//nearEnemyExplosion	= DefaultProjectileNearEnemyExplosion;
	//hitDeflectorExplosion = SeekerDiscBounceEffect;

	//fxLight					= WhiteMinigunProjectileFxLight;

	missEnemyEffect = WhiteMinigunProjectileMissedEnemyEffect;

	//laserTail    = WhiteMinigunProjectileLaserTail;
	//laserTailLen = 8.0;

	//laserTrail[0] = WhiteMinigunProjectileLaserTrail;

	particleEmitter = WhiteMinigunTracerParticleEmitte;

	muzzleVelocity   = 50 * $Server::Game.slowpokemod;
	velInheritFactor = 1.0 * $Server::Game.slowpokemod;

	isBallistic = false;
	gravityMod  = 4.0 * $Server::Game.slowpokemod;

	armingDelay	= 1000*0;
	lifetime	= 3000;
	fadeDelay	= 5000;

	decals[0] = BulletHoleDecalOne;

	hasLight    = true;
	lightRadius = 4.0;
	lightColor  = "0.5 0.5 0.5";
};


