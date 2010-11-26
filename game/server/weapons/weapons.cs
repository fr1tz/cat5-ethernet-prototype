//------------------------------------------------------------------------------
// Revenge Of The Cats: Ethernet
// Copyright (C) 2008, mEthLab Interactive
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Revenge Of The Cats - weapons.cs
// code for all weapons
//------------------------------------------------------------------------------

exec("./weapons.sfx.cs");
exec("./weapons.gfx.cs");

$DamageType::Impact = 0;
$DamageType::Splash	= 1;
$DamageType::Force  = 2;

$SplashDamageFalloff::Linear = 0;
$SplashDamageFalloff::Exponential = 1;

// These are bit masks ($TargetingMask::FreeX = 2^X)...
$TargetingMask::Disc  = 1;
$TargetingMask::Heat  = 2;
$TargetingMask::Free1 = 4;
$TargetingMask::Free2 = 8;

//-----------------------------------------------------------------------------

datablock AudioProfile(WeaponSwitchSound)
{
	filename = "share/sounds/rotc/weaponSwitch.wav";
	description = AudioClosest3D;
	preload = true;
};

//-----------------------------------------------------------------------------

function WeaponImage::onMount(%this, %obj, %slot)
{
//	// mount secondary...
//	%obj.mountImage(%this.secondary,1);

//	if(!%this.specialWeapon)
//	{
//		// holster special weapon...
//		if(%obj.specialWeapon == $SpecialWeapon::AssaultRifle)
//			%obj.mountImage(HolsteredAssaultRifleImage,2);
//		if(%obj.specialWeapon == $SpecialWeapon::GrenadeLauncher)
//			%obj.mountImage(HolsteredGrenadeLauncherImage,2);
//		if(%obj.mainspecialWeaponWeapon == $SpecialWeapon::SniperRifle)
//			%obj.mountImage(HolsteredSniperRifleImage,2);
//	}
	
	%obj.setArmThread(%this.armThread);
	%obj.playAudio(0,WeaponSwitchSound);
}

function WeaponImage::onUnmount(%this, %obj, %slot)
{
	%client = %obj.client;
}

function WeaponImage::onFire(%this, %obj, %slot)
{

}

//-----------------------------------------------------------------------------

function ProjectileData::onAdd(%this, %obj)
{
	%client = %obj.sourceObject.client;
	if(!isObject(%client))
		return;

	%a = %client.stats.fired;
	%n = %this.getName();
	arrayChangeElement(%a, "All", arrayGetValue(%a, "All") + 1);
	arrayChangeElement(%a, %n, arrayGetValue(%a, %n) + 1);
}

function ProjectileData::onRemove(%this, %obj)
{
    // avoid console spam
}

function ProjectileData::onCollision(%this,%obj,%col,%fade,%pos,%normal,%dist)
{
	if( !(%col.getType() & $TypeMasks::ShapeBaseObjectType) )
		return;

	// apply impulse...
	if(%this.impactImpulse > 0)
	{
		%impulseVec = VectorNormalize(%obj.getVelocity());
		%impulseVec = VectorScale(%impulseVec, %this.impactImpulse);
		%col.applyImpulse(%pos, %impulseVec);
	}
	
	// bail out here if projectile doesn't do impact damage...
	if( %this.impactDamage == 0 )
		return;

	// call damage func...
	%col.damage(%obj, %pos, %this.impactDamage, $DamageType::Impact);
	
	// if projectile was fired by a player, regain some of his energy...
	%sourceObject = %obj.getSourceObject();
	if(%sourceObject.getType() & $TypeMasks::PlayerObjectType)
	{
		%newSrcEnergy = %sourceObject.getEnergyLevel() + %this.energyDrain;
		%sourceObject.setEnergyLevel(%newSrcEnergy);
	}
}

function ProjectileData::onExplode(%this,%obj,%pos,%normal,%fade,%dist,%expType)
{
    // can we bail out early?
    if(%this.splashDamageRadius == 0)
        return;

	%radius = %this.splashDamageRadius;
	%damage = %this.splashDamage;
	%damageType = $DamageType::Splash;
	
	%sourceObject = %obj.getSourceObject();
	%regainEnergy = %sourceObject.getClassName() $= "Player";
		
	InitContainerRadiusSearch(%pos, %radius, $TypeMasks::ShapeBaseObjectType);
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

		%dist = containerSearchCurrRadiusDist();
		%prox = %radius - %dist;
		if(%this.splashDamageFalloff == $SplashDamageFalloff::Exponential)
			%distScale = (%prox*%prox) / (%radius*%radius);
		else
			%distScale = %prox / %radius;
		
		// apply impulse...
		if(%this.splashImpulse > 0)
		{
            %center = %targetObject.getWorldBoxCenter();
			%impulseVec = VectorNormalize(VectorSub(%center, %pos));
			%impulseVec = VectorScale(%impulseVec, %this.splashImpulse);
			%targetObject.applyImpulse(%pos, %impulseVec);
		}

		// bail out here if projectile doesn't do splash damage...
		if( %this.splashDamage == 0 )
			continue;

		// call damage func...
		%targetObject.damage(%obj, %pos,
			%damage * %coverage * %distScale, %damageType);
			
		// if projectile was fired by a player, regain some of his energy...
		if(%regainEnergy)
		{
			%newSrcEnergy = %sourceObject.getEnergyLevel()
				+ %this.energyDrain*%distScale;
			%sourceObject.setEnergyLevel(%newSrcEnergy);
		}
	}
}

