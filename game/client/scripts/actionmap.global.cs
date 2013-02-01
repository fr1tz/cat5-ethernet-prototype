//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

// The global action map is created by the engine and always active.

//------------------------------------------------------------------------------
// misc...
//------------------------------------------------------------------------------

GlobalActionMap.bind(keyboard, "tilde", toggleConsole);
GlobalActionMap.bind(keyboard, "F12", toggleConsole);
GlobalActionMap.bindCmd(keyboard, "alt enter", "", "toggleFullScreen();");
//GlobalActionMap.bindCmd(keyboard, "F1", "", "contextHelp();");

//------------------------------------------------------------------------------
// debugging...
//------------------------------------------------------------------------------

$MFDebugRenderMode = 0;
function cycleDebugRenderMode(%val)
{
	if (!%val)
		return;
	if($MFDebugRenderMode == 0)
	{
		// Outline mode, including fonts so no stats
		$MFDebugRenderMode = 1;
		GLEnableOutline(true);
	}
	else if ($MFDebugRenderMode == 1)
	{
		// Interior debug mode
		$MFDebugRenderMode = 2;
		GLEnableOutline(false);
		setInteriorRenderMode(7);
		showInterior();
	}
	else if ($MFDebugRenderMode == 2)
	{
		// Back to normal
		$MFDebugRenderMode = 0;
		setInteriorRenderMode(0);
		GLEnableOutline(false);
		show();
	}
}

GlobalActionMap.bind(keyboard, "F9", cycleDebugRenderMode);
