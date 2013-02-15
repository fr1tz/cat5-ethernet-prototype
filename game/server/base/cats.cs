//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Cat5 - cats.cs
// Code for all CATs
//------------------------------------------------------------------------------

function executeCatScripts()
{
	echo(" ----- executing cat scripts ----- ");

	%i = 1;
	// Weapons...
	$CatEquipment::Blaster         = %i; %i++;
	$CatEquipment::BattleRifle     = %i; %i++;
	$CatEquipment::SniperRifle     = %i; %i++;
	$CatEquipment::MiniGun         = %i; %i++;
	$CatEquipment::RepelGun        = %i; %i++;
	$CatEquipment::GrenadeLauncher = %i; %i++;
	// Discs...
	$CatEquipment::SlasherDisc     = %i; %i++;
	$CatEquipment::RepelDisc       = %i; %i++;
	$CatEquipment::ExplosiveDisc   = %i; %i++;
	// Other...
	$CatEquipment::Damper          = %i; %i++;
	$CatEquipment::VAMP            = %i; %i++;
	$CatEquipment::Anchor          = %i; %i++;
	$CatEquipment::Stabilizer      = %i; %i++;
	$CatEquipment::Grenade         = %i; %i++;
	$CatEquipment::Bounce          = %i; %i++;
	$CatEquipment::Etherboard      = %i; %i++;
	$CatEquipment::Permaboard      = %i; %i++;
	$CatEquipment::Regeneration    = %i; %i++;
	
	exec("./cats.sfx.cs");
	exec("./cats.gfx.cs");
}

executeCatScripts();

//------------------------------------------------------------------------------

function Cat::useWeapon(%this, %obj, %nr)
{
	%client = %obj.client;

   return;

	if(%nr == 4)
	{
		dropGreen(%obj);
		return;
	}

   if(%nr == -17)
   {
		if(%client.hasBounce)
			deployRepel3(%obj);
      return;
   }

	// discs...
	if($Game::GameType == $Game::Ethernet)
	{
		if(%nr == 6)
		{
			launchExplosiveDisc(%obj);
			return;
		}
	}
	
	if(%client.numWeapons == 0)
		return;

	if(%nr > %client.numWeapons)
		return;

	if(%nr == 0)
		%obj.currWeapon++;
	else
		%obj.currWeapon = %nr;
	
	if(%obj.currWeapon > %client.numWeapons)
		%obj.currWeapon = 1;	

	%wpn = %client.weapons[%obj.currWeapon-1];

	if(%wpn == 1)
	{
   	%obj.mountImage(ShotgunImage, 0, -1, true);
	}
	else if(%wpn == $CatEquipment::BattleRifle)
	{
		%obj.mountImage(BounceGunImage, 0, -1, true);
	}
	else if(%wpn == $CatEquipment::SniperRifle)
	{
		%obj.mountImage(SniperRifleImage, 0, -1, true);
	}
	else if(%wpn == $CatEquipment::MiniGun)
	{
		%obj.mountImage(MinigunImage, 0, -1, true);
	}
	else if(%wpn == $CatEquipment::RepelGun)
	{
		%obj.mountImage(RepelGunImage, 0, -1, true);
	}
	else if(%wpn == $CatEquipment::GrenadeLauncher)
	{
		%obj.mountImage(GrenadeLauncherImage, 0, -1, true);
	}
}

