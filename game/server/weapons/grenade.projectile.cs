//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

exec("./grenade.projectile.gfx.red.cs");
exec("./grenade.projectile.gfx.blue.cs");

datablock ProjectileData(RedGrenade)
{
	stat = "grenade";

	// script damage properties...
	impactDamage       = 0;
 	impactImpulse      = 0;
	splashDamage       = 150;
	splashDamageRadius = 15;
	splashImpulse      = 0;

	energyDrain = 0; // how much energy does firing this projectile drain?

	sound = GrenadeProjectileSound;

	muzzleVelocity   = 100;
	velInheritFactor = 0.0;

	armingDelay  = 1000;
	lifetime     = 10000;
	fadeDelay    = 0;

	isBallistic      = true;
	gravityMod       = 8.0;
	bounceElasticity = 0.7;
	bounceFriction   = 0.5;
    //numBounces           = 3;

    projectileShapeName = "share/shapes/rotc/weapons/disc/projectile.red.dts";

	explosion             = RedGrenadeExplosion;
//	hitEnemyExplosion     = RedDiscHitEnemy;
// nearEnemyExplosion	 = ThisDoesNotExist;
//	hitTeammateExplosion  = RedDiscHitEnemy;
//	hitDeflectorExplosion = RedDiscDeflectedEffect;
	bounceExplosion       = RedGrenadeBounceEffect;
 
    particleEmitter = RedGrenade_ParticleEmitter;

//	laserTrail[0]		= RedGrenade_Lasertrail;

	decals[0] = ExplosionDecalOne;

	hasLight	 = true;
	lightRadius = 10.0;
	lightColor  = "1.0 0.0 0.0";
};

function RedGrenade::onAdd(%this, %obj)
{
	Parent::onAdd(%this, %obj);
}

function RedGrenade::onRemove(%this, %obj)
{	
	Parent::onRemove(%this, %obj);
}

function RedGrenade::onExplode(%this,%obj,%pos,%normal,%fade,%dist,%expType)
{
    Parent::onExplode(%this,%obj,%pos,%normal,%fade,%dist,%expType);

	%radius = %this.splashDamageRadius;
	%sourceObject = %obj.getSourceObject();

	InitContainerRadiusSearch(%pos, %radius, $TypeMasks::ShapeBaseObjectType);
	%halfRadius = %radius / 2;
	while( (%targetObject = containerSearchNext()) != 0 )
	{
        // the observer cameras are ShapeBases; ignore them...
        if(%targetObject.getType() & $TypeMasks::CameraObjectType)
    	   continue;

		%coverage = calcExplosionCoverage(%pos, %targetObject,
			$TypeMasks::InteriorObjectType |  $TypeMasks::TerrainObjectType |
			$TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType |
			$TypeMasks::TurretObjectType);

		if (%coverage == 0)
			continue;

        %sourceObject.setDiscTarget(%targetObject);
	}
}

//------------------------------------------------------------------------------

datablock ProjectileData(BlueGrenade : RedGrenade)
{
    projectileShapeName = "share/shapes/rotc/weapons/disc/projectile.blue.dts";
	explosion = BlueGrenadeExplosion;
	bounceExplosion = BlueGrenadeBounceEffect;
    particleEmitter = BlueGrenade_ParticleEmitter;
//	laserTrail[0] = BlueGrenade_Lasertrail;
	lightColor = "0.0 0.0 1.0";
};

function BlueGrenade::onAdd(%this, %obj)
{
    RedGrenade::onAdd(%this, %obj);
}

function BlueGrenade::onRemove(%this, %obj)
{
    RedGrenade::onRemove(%this, %obj);
}

function BlueGrenade::onExplode(%this,%obj,%pos,%normal,%fade,%dist,%expType)
{
    RedGrenade::onExplode(%this,%obj,%pos,%normal,%fade,%dist,%expType);
}
