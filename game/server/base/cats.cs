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
}

executeCatScripts();

//------------------------------------------------------------------------------

function Cat::onAdd(%this, %obj)
{
	Parent::onAdd(%this, %obj);
	//%obj.mountImage(StandardCatLightImage, 3);
   if(%obj.client.PPN !$= "")
      %obj.playAudio(0, VoxPumpgunnerSpawn2);
   else
      %obj.playAudio(0, VoxPumpgunnerSpawn1);
   error("weex");
   %obj.playAudio(0, VoxPumpgunnerSpawn2);
}

function Cat::useWeapon(%this, %obj, %nr)
{
   return;

   %sound = "";
   switch(%nr)
   {
      case 1: %sound = VoxPumpgunnerRetreating;
      case 2: %sound = VoxPumpgunnerHolding;
      case 3: %sound = VoxPumpgunnerMoving;
      case 4: %sound = VoxPumpgunnerRetreat;
      case 5: %sound = VoxPumpgunnerHold;
      case 6: %sound = VoxPumpgunnerMove;
   }

   if(isObject(%sound))
      %obj.playAudio(0, %sound);
}

