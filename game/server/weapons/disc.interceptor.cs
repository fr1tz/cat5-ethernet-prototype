//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Cat5 - disc.interceptor.cs
// Disc-intercepting disc projectile
//------------------------------------------------------------------------------

exec("./disc.interceptor.gfx.red.cs");
exec("./disc.interceptor.gfx.blue.cs");

//-----------------------------------------------------------------------------
// projectile datablock...

datablock NortDiscData(RedInterceptorDisc)
{
	stat = "interceptor";

	// script damage properties...
	impactDamage         = 0;
	impactImpulse        = 1000;
	splashDamage         = 0;
	splashDamageRadius   = 0;
	splashImpulse        = 0;
	
	energyDrain = 0; // how much energy does firing this projectile drain?

	sound = DiscProjectileSound;
	
	targetLockTimeMS = 50;

	maxTrackingAbility = 150;
	trackingAgility = 2;

	explodesNearEnemies = false;
	explodesNearEnemiesRadius = 3;

	muzzleVelocity		= 125 * $Server::Game.slowpokemod;
    maxVelocity         = 125 * $Server::Game.slowpokemod;
    acceleration        = 4 * $Server::Game.slowpokemod;
	velInheritFactor	 = 0.0 * $Server::Game.slowpokemod;

	armingDelay			= 0*1000;
	lifetime				= 10*1000;
	fadeDelay			  = 3*1000;

	isBallistic = true;
	gravityMod			 = 1.0;
	bounceElasticity	 = 0.99;
	bounceFriction		 = 0.0;
    numBounces           = 3;

	projectileShapeName = "share/shapes/rotc/weapons/disc/projectile_red.dts";

	explosion             = RedInterceptorDiscExplosion;
	hitEnemyExplosion     = RedInterceptorDiscHitEnemy;
// nearEnemyExplosion	 = ThisDoesNotExist;
	hitTeammateExplosion  = RedInterceptorDiscHitEnemy;
	hitDeflectorExplosion = RedInterceptorDiscDeflectedEffect;
	bounceExplosion       = RedInterceptorDiscHit;

    laserTail				 =  RedInterceptorDiscLaserTail;
    laserTailLen			 = 4.0;

	//laserTrail[0]		= RedInterceptorDiscInnerLaserTrail;
	//laserTrail[1]		= RedInterceptorDiscOuterLaserTrail;
	
	decals[0] = SlashDecalOne;

	hasLight	 = true;
	lightRadius = 10.0;
	lightColor  = "1.0 0.0 0.0";
 
    startVertical = true;
};

function RedInterceptorDisc::onAdd(%this,%obj)
{
   %obj.setTargetingMask($TargetingMask::Disc);
}

function RedInterceptorDisc::onRemove(%this,%obj)
{
	%source = %obj.sourceObject;
	%source.incDiscs();
}

function RedInterceptorDisc::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
	Parent::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

function RedInterceptorDisc::onHitTarget(%this,%obj)
{
    %obj.getTarget().setDeflected(%obj.getVelocity());
//    %obj.sourceObject.activateBarrier(3);
//	%enemy = %obj.getTarget().sourceObject;
//	%player = %obj.sourceObject;
//	if(isObject(%enemy) && isObject(%player))
//		%player.setDiscTarget(%enemy);
    %obj.explode();
}

function RedInterceptorDisc::onLostTarget(%this, %obj)
{
    %obj.explode();
}

//-----------------------------------------------------------------------------

datablock NortDiscData(BlueInterceptorDisc : RedInterceptorDisc)
{
	projectileShapeName = "share/shapes/rotc/weapons/disc/projectile_blue.dts";

	explosion             = BlueInterceptorDiscExplosion;
	hitEnemyExplosion     = BlueInterceptorDiscHitEnemy;
	hitTeammateExplosion  = BlueInterceptorDiscHitEnemy;
	hitDeflectorExplosion = BlueInterceptorDiscDeflectedEffect;
	bounceExplosion       = BlueInterceptorDiscHit;

    laserTail				 =  BlueInterceptorDiscLaserTail;

	//laserTrail[0]		= BlueInterceptorDiscInnerLaserTrail;
	//laserTrail[1]		= BlueInterceptorDiscOuterLaserTrail;

	//fxLight				 = BlueInterceptorDiscFxLight;

	hasLight	 = true;
	lightRadius = 10.0;
	lightColor  = "0.0 0.0 1.0";
};

function BlueInterceptorDisc::onAdd(%this,%obj)
{
	RedInterceptorDisc::onAdd(%this,%obj);
}

function BlueInterceptorDisc::onRemove(%this,%obj)
{
	RedInterceptorDisc::onRemove(%this,%obj);
}

function BlueInterceptorDisc::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
	RedInterceptorDisc::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist);
}

function BlueInterceptorDisc::onHitTarget(%this,%obj)
{
	RedInterceptorDisc::onHitTarget(%this,%obj);
}

function BlueInterceptorDisc::onLostTarget(%this, %obj)
{
	RedInterceptorDisc::onLostTarget(%this,%obj);
}


