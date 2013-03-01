//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Cat5 - shell.cs
// Code for Cat5' Shell
//------------------------------------------------------------------------------

if(isObject(DefaultCursor))
{
    DefaultCursor.delete();

    new GuiCursor(DefaultCursor)
    {
	   hotSpot = "8 7";
	   bitmapName = "./pixmaps/cat5cursor";
    };
}

function addWindow(%control, %inactive)
{
	%oldparent = %control.getParent();
	%parent = ShellWindows;
	if(%control.getParent().getId() != %parent.getId())
	{
      %parent.clear();
		%parent.add(%control);
		%parent.pushToBack(%control);
		if(%control.getParent() != %oldParent)
			%control.onAddedAsWindow();
	}
//	if(!%inactive)
//		windowSelected(%control);
   if(ShellWindows.getCount() > 0)
   {
      ShellTS.pan(0, 48);
      ShellSidebar.visible = false;
      ShellMissionWindowContainer.visible = false;
      ShellWindows.visible = true;
      ShellStack.pushToBack(ShellWindows);
   }
	Canvas.repaint();
}

function removeWindow(%control)
{
	%control.getParent().remove(%control);
	%control.onRemovedAsWindow();
   if(ShellWindows.getCount() == 0)
   {
      ShellTS.pan(-22, 48);
      ShellSidebar.visible = true;
      ShellMissionWindowContainer.visible = true;
      ShellStack.pushToBack(ShellMissionWindowContainer);
      ShellStack.pushToBack(ShellSidebar);
   }
	Canvas.repaint();
}

function startUpdateHilightedGuiControlsThread()
{
	%set = HilightedGuiControlsSet;
	if(%set.updateThread $= "")
	{
		%set.updateThread = schedule(500, HilightedGuiControlsSet,
			"updateHilightedGuiControlsThread");
	}
}

function stopUpdateHilightedGuiControlsThread()
{
	%set = HilightedGuiControlsSet;
	if(%set.updateThread !$= "")
	{
		cancel(%set.updateThread);
		%set.updateThread = "";
	}
}

function updateHilightedGuiControlsThread()
{
	//echo("updateHilightedGuiControlsThread()");

	stopUpdateHilightedGuiControlsThread();

	%set = HilightedGuiControlsSet;
	if(%set.hilightState $= "")
		%set.hilightState = true;
	else
		%set.hilightState = "";

	for(%i = 0; %i < %set.getCount(); %i++)
	{
		%control = %set.getObject(%i);
		if(%set.hilightState == true)
			windowChangeProfile(%control, GuiHilightButtonProfile);
		else
			windowChangeProfile(%control, GuiButtonProfile);

	}

	startUpdateHilightedGuiControlsThread();
}

function hilightControl(%control, %hilight)
{
	%set = HilightedGuiControlsSet;
	if(%hilight)
	{
		%set.add(%control);
	}
	else
	{
		%set.remove(%control);
			windowChangeProfile(%control, GuiButtonProfile);

	}
}

function clientUpdateScanlinesThread()
{
   schedule(50, 0, "clientUpdateScanlinesThread");

   if(GuiScanlinesProfile.zPulseDt $= "")
      GuiScanlinesProfile.zPulseDt = 4;

   GuiScanlinesProfile.zPulse += GuiScanlinesProfile.zPulseDt;
   if(GuiScanlinesProfile.zPulse > 255)
   {
      GuiScanlinesProfile.zPulse = 255;
      GuiScanlinesProfile.zPulseDt = -4;
   }
   else if(GuiScanlinesProfile.zPulse < 0)
   {
      GuiScanlinesProfile.zPulse = 0;
      GuiScanlinesProfile.zPulseDt = 4;
   }

   //error(GuiScanlinesProfile.zPulse);
   GuiScanlinesProfile.fillColor = "0 255" SPC GuiScanlinesProfile.zPulse SPC "100";

   //if(getRandom(100) == 0)
   //   GuiScanlinesProfile.fillColor = "0 255 255 140";
}

function Shell::onAdd(%this)
{
	new SimSet(HilightedGuiControlsSet);
   clientUpdateScanlinesThread();
}

function Shell::onWake(%this)
{
   if(!$cTorqueSplashDone)
   {
      ShellTS.visible = false;
      ShellStack.visible = false;
      ShellTorqueSplash.visible = true;
      Shell.schedule(100, checkTorqueSplashScreenDone);
   }

	ShellVersionString.setText("version:" SPC $GameVersionString);
	//windowSelected(RootMenuWindow);
   ShellMissionWindowContainer.add(MissionWindow);
   ShellMissionWindowContainer.add(RecordingControlsWindow);
	startUpdateHilightedGuiControlsThread();
}

function Shell::onSleep(%this)
{
	stopUpdateHilightedGuiControlsThread();
}

function ShellRoot::onMouseDown(%this,%modifier,%coord,%clickCount)
{
	//
	// display the root menu...
	//

	if( Shell.isMember(RootMenu) )
		removeWindow(RootMenu);
	else
	{
		RootMenu.position = %coord;
		//addWindow(RootMenu);
		//Canvas.repaint();
	}
}

function ShellRoot::onMouseEnter(%this,%modifier,%coord,%clickCount)
{
 //
}

function Shell::checkTorqueSplashScreenDone(%this)
{
   if(ShellTorqueSplash.done)
   {
      $cTorqueSplashDone = true;

      ShellTS.visible = true;
      ShellStack.visible = true;
      ShellTorqueSplash.visible = false;

      if(!$Pref::Audio::ChannelMuted[$SimAudioType])
         alxSetChannelVolume($SimAudioType, $pref::Audio::ChannelVolume[$SimAudioType]);
   }
   else
      %this.schedule(100, checkTorqueSplashScreenDone);
}

function windowChangeProfile(%ctrl, %profile)
{
	%ctrl.profile = %profile;
	%parent = %ctrl.getParent();
	%parent.remove(%ctrl);
	%parent.add(%ctrl);
}

function windowSelected(%ctrl)
{
	if(%ctrl.getId() == $SelectedWindow.getId())
		return;

	if($SelectedWindow !$= "")
		windowChangeProfile($SelectedWindow, GuiInactiveWindowProfile);
		
	windowChangeProfile(%ctrl, GuiWindowProfile);
	%ctrl.makeFirstResponder(true);

	$SelectedWindow = %ctrl;
}

function GuiCanvas::onCanvasMouseDown(%this, %ctrl)
{
	%win = "";
	while(isObject(%ctrl))
	{
		if(%ctrl.getClassName() $= "GuiWindowCtrl")
		{
			%win = %ctrl;
			break;
		}
		%ctrl = %ctrl.getParent();
	}

	if(isObject(%win))
		windowSelected(%win);
}

