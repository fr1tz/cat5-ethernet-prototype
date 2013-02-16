//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

function clientCmdSkyColor(%color, %elementMask)
{
   if(!(%elementMask > 0))
      return;

	if($sky $= "")
		$sky = client_find_sky();

   %mask = 1;
   for(%i = 0; %i < 9; %i++)
   {
      if(%elementMask & %mask)
      	$sky.changeColor(%i, %color);
      %mask *= 2;
   }
}

function clientCmdCrosshair(%option, %arg1, %arg2, %arg3, %arg4, %arg5)
{
   //error("clientCmdCrosshair():" SPC %arg1 SPC %arg2 SPC %arg3 SPC %arg4 SPC %arg5);
   Hud.setCrosshair(%option, %arg1, %arg2, %arg3, %arg4, %arg5);
}

function clientCmdHud(%option, %arg1, %arg2, %arg3, %arg4, %arg5)
{
   //error("clientCmdHud():" SPC %arg1 SPC %arg2 SPC %arg3 SPC %arg4 SPC %arg5);
   if(%option $= "health")
   {
      if(%arg1 !$= "")
      {
         HealthMeter.setVisible(%arg1);
         HealthIcon.setVisible(%arg1);
      }
   }
   else if(%option $= "energy")
   {
      if(%arg1 !$= "")
         EnergyMeter.setVisible(%arg1);
      if(%arg2 !$= "")
      {
         EnergyMeter.bitmap = %arg2;
         Hud.remove(EnergyMeter);
         Hud.add(EnergyMeter);
      }
   }
   else if(%option $= "minimap")
   {
      if(%arg1 !$= "")
         MiniMap.setVisible(%arg1);
      if(%arg2 !$= "")
         MiniMap.visRadius = %arg2;
      if(%arg3 !$= "")
         MiniMap.rotate = %arg3;

   }
}

function clientCmdCockpitMode(%mode, %arg1, %arg2, %arg3, %arg4, %arg5)
{
   //error("clientCmdCockpitMode():" SPC %mode SPC %arg1 SPC %arg2 SPC %arg3 SPC %arg4 SPC %arg5);
   HUD.zMode = %mode;
   if(%mode == 0)
   {
      %pos = %arg1;
      HudRulerH.visible = false;
      HudRulerV.visible = false;
      $mvPosActive = false;
      $mvMapActive = true;
      $mvMapX = getWord(%pos, 0);
      $mvMapY = getWord(%pos, 1);
      HUD.viewMode = 1;
      HUD.pan($mvMapX, $mvMapY);
   }
   else if(%mode == 1)
   {
      HudRulerH.visible = true;
      HudRulerV.visible = true;
      $mvMapActive = false;
      $mvPosActive = true;
      HUD.viewMode = 3;
   }
   else if(%mode == 2)
   {
      %pos = %arg1;
      HudRulerH.visible = false;
      HudRulerV.visible = false;
      $mvPosActive = false;
      $mvMapActive = true;
      $mvMapX = getWord(%pos, 0);
      $mvMapY = getWord(%pos, 1);
      HUD.viewMode = 2;
   }
   else if(%mode == 3)
   {
      HudRulerH.visible = true;
      HudRulerV.visible = true;
      $mvMapActive = false;
      $mvPosActive = false;
      HUD.viewMode = 5;
   }
   else if(%mode == 4)
   {
      HudRulerH.visible = true;
      HudRulerV.visible = true;
      $mvMapActive = false;
      $mvPosActive = false;
      HUD.viewMode = 6;
   }
}
