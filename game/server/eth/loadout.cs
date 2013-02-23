//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

function GameConnection::loadDefaultLoadout(%this, %no)
{
   switch(%no)
   {
      case 1:
         %this.loadoutName[%no] = "Cobra";
         %this.loadoutCode[%no] = "1";
      case 2:
         %this.loadoutName[%no] = "Malone";
         %this.loadoutCode[%no] = "2";
      case 3:
         %this.loadoutName[%no] = "Terminator";
         %this.loadoutCode[%no] = "3";
      case 4:
         %this.loadoutName[%no] = "Commando";
         %this.loadoutCode[%no] = "4";
      case 5:
         %this.loadoutName[%no] = "Predator";
         %this.loadoutCode[%no] = "5";
      default:
         %this.loadoutName[%no] = "";
         %this.loadoutCode[%no] = "1";
   }
}

//------------------------------------------------------------------------------

function GameConnection::defaultLoadout(%this)
{
	for(%i = 1; %i <= 9; %i++)
		this.loadout[%i] = "";

   %this.class = 1;

	if($Game::GameType == $Game::Ethernet)
	{
		%this.loadout[1] = $CatEquipment::Blaster;
		//%this.loadout[2] = $CatEquipment::Blaster;
		//%this.loadout[3] = $CatEquipment::Etherboard;
		//%this.loadout[4] = $CatEquipment::Damper;
		//%this.loadout[5] = $CatEquipment::VAMP;
		//%this.loadout[6] = $CatEquipment::Anchor;
		//%this.loadout[7] = $CatEquipment::Grenade;
		//%this.loadout[8] = $CatEquipment::Bounce;
		//%this.loadout[9] = $CatEquipment::RepelDisc;
		//%this.loadout[10] = $CatEquipment::ExplosiveDisc;
	}
	else
	{
		%this.loadout[1] = $CatEquipment::Blaster;
		%this.loadout[2] = $CatEquipment::BattleRifle;
		%this.loadout[3] = $CatEquipment::Etherboard;
		%this.loadout[4] = $CatEquipment::Damper;
		%this.loadout[5] = $CatEquipment::VAMP;
		%this.loadout[6] = $CatEquipment::Stabilizer;
		%this.loadout[7] = $CatEquipment::Grenade;
		%this.loadout[8] = $CatEquipment::SlasherDisc;
	}
}

function GameConnection::updateLoadout(%this)
{
	%this.numWeapons = 0;
	%this.hasDamper = false;
	%this.hasAnchor = false;
	%this.hasStabilizer = false;
	%this.hasSlasherDisc = false;
	%this.hasRepelDisc = false;
	%this.hasExplosiveDisc = false;
	%this.hasGrenade = false;
	%this.hasBounce = false;
	%this.hasEtherboard = false;
	%this.hasPermaboard = false;
	%this.numVAMPs = 0;
	%this.numRegenerators = 0;
	for(%i = 1; %i <= 9; %i++)
	{
		if(%this.loadout[%i] $= "")
			continue;

		//if(%this.loadout[%i] == $CatEquipment::Damper)
		//{
		//	%this.hasDamper = true;
		//}
		//else if(%this.loadout[%i] == $CatEquipment::Anchor)
		//{
		//	%this.hasAnchor = true;
		//}
		//else if(%this.loadout[%i] == $CatEquipment::Stabilizer)
		//{
		//	%this.hasStabilizer = true;
		//}
		if(%this.loadout[%i] == $CatEquipment::SlasherDisc)
		{
			%this.hasSlasherDisc = true;
		}
		else if(%this.loadout[%i] == $CatEquipment::RepelDisc)
		{
			%this.hasRepelDisc = true;
		}
		else if(%this.loadout[%i] == $CatEquipment::ExplosiveDisc)
		{
			%this.hasExplosiveDisc = true;
		}
		else if(%this.loadout[%i] == $CatEquipment::Grenade)
		{
			%this.hasGrenade = true;
		}
		//else if(%this.loadout[%i] == $CatEquipment::Bounce)
		//{
		//	%this.hasBounce = true;
		//}
		else if(%this.loadout[%i] == $CatEquipment::Etherboard)
		{
		   %this.hasEtherboard = true;
		}
		else if(%this.loadout[%i] == $CatEquipment::Permaboard)
		{
			%this.hasPermaboard = true;
		}
		//else if(%this.loadout[%i] == $CatEquipment::VAMP)
		//{
		//	%this.numVAMPs++;
		//}
		else if(%this.loadout[%i] == $CatEquipment::Regeneration)
		{
			%this.numRegenerators++;
		}
		else if(%this.loadout[%i] < $CatEquipment::SlasherDisc)
		{
			%this.weapons[%this.numWeapons] = %this.loadout[%i];
			%this.numWeapons++;
		}
	}
}

function GameConnection::displayInventory(%this, %obj)
{
   if(%this.team == $Team0)
   {
      %this.setHudMenuL("*", " ", 1, 0);
      %this.setHudMenuR("*", " ", 1, 0);
      return;
   }

	%iconname[1] = "pumpgunner";
	%iconname[2] = "shotgunner";
	%iconname[3] = "minigunner";
	%iconname[4] = "specialist";
	%iconname[5] = "hunter";

	%itemname[1] = "Pumpgunner";
	%itemname[2] = "Shotgunner";
	%itemname[3] = "Minigunner";
	%itemname[4] = "Specialist";
	%itemname[5] = "Hunter";

   if(%this.inventoryMode $= "hidden")
   {
      %this.setHudMenuL("*", " ", 1, 0);
   }
	if(%this.inventoryMode $= "show")
	{
		%this.setHudMenuL(0, "\n", 4, 1);
		%this.setHudMenuL(1, "<lmargin:100><font:Cat5:12>Current class:\n<lmargin:120>", 1, 1);
		%this.setHudMenuL(2, "<bitmap:share/ui/cat5/icon." @ %iconname[%this.class] @ ".64x64>", 1, 1);
		%this.setHudMenuL(3, "<sbreak><lmargin:100>(Press @bind51 to change)", 1, 1);
		for(%i = 4; %i <= 9; %i++)
			%this.setHudMenuL(%i, "", 1, 0);
	}
	else if(%this.inventoryMode $= "select")
	{
   	%numItems = 5;
		%this.setHudMenuL(0, "\n\n<lmargin:100><font:Cat5:12>Select new class:\n\n", 1, 1);
		for(%i = 1; %i <= %numItems; %i++)
			%this.setHudMenuL(%i, "@bind" @ (%i < 6 ? 34 : 41) + %i @ ": " @ %itemname[%i]  @  "\n" @
				"   <bitmap:share/ui/cat5/icon." @ %iconname[%i] @ ".64x64>" @ "<sbreak>", 1, 1);
		for(%i = %numItems + 2; %i <= 9; %i++)
			%this.setHudMenuL(%i, "", 1, 0);
	}
}

function GameConnection::changeInventory(%this, %nr)
{
	if(%this.inventoryMode $= "show")
	{
      if(%nr == -17)
      {
			%this.inventoryMode = "select";
			%this.inventoryMode[1] = 1;
        	%this.play2D(BipMessageSound);
         %this.displayInventory(0);
      }
	}
	else if(%this.inventoryMode $= "select")
	{
		if(%nr < 1 || %nr > 5)
			return;

      %this.class = %nr;

		//%this.loadout[%this.inventoryMode[1]] = %equipment;
		//%this.updateLoadout();

		%this.inventoryMode = "show";
		%this.displayInventory(0);
     	%this.play2D(BipMessageSound);
	}
}

//------------------------------------------------------------------------------

