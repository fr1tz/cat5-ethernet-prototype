//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------


//------------------------------------------------------------------------------
// Hard coded images referenced from C++ code
//------------------------------------------------------------------------------

//	editor/SelectHandle.png
//	editor/DefaultHandle.png
//	editor/LockedHandle.png


//------------------------------------------------------------------------------
// Functions
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Mission Editor 
//------------------------------------------------------------------------------

function Editor::create()
{
	// Not much to do here, build it and they will come...
	// Only one thing... the editor is a gui control which
	// expect the Canvas to exist, so it must be constructed
	// before the editor.
	new EditManager(Editor)
	{
		profile = "GuiContentProfile";
		horizSizing = "right";
		vertSizing = "top";
		position = "0 0";
		extent = "640 480";
		minExtent = "8 8";
		visible = "1";
		setFirstResponder = "0";
		modal = "1";
		helpTag = "0";
		open = false;
	};
}


function Editor::onAdd(%this)
{
	// Basic stuff
	exec("./cursors.cs");

	// Tools
	exec("./editor.bind.cs");
	exec("./ObjectBuilderGui.gui");

	// New World Editor
	exec("./EditorGui.gui");
	exec("./EditorGui.cs");

	// World Editor
	exec("./WorldEditorToolbox.gui");	
	exec("./WorldEditorSettings.gui");

	// Terrain Editor
	exec("./TerrainEditorSettings.gui");
	exec("./TerrainEditorSoftSelection.gui");

	// Ignore Replicated fxStatic Instances.
	EWorldEditor.ignoreObjClass("fxShapeReplicatedStatic");

	// do gui initialization...
	EditorGui.init();

	//
	exec("./editorRender.cs");
}

function Editor::checkActiveLoadDone()
{
	if(isObject(EditorGui) && EditorGui.loadingMission)
	{
		Canvas.setContent(EditorGui);
		EditorGui.loadingMission = false;
		return true;
	}
	return false;
}

//------------------------------------------------------------------------------

function toggleEditor(%make)
{
	if (%make)
	{
        if($missionRunning)
		{
			if (!isObject(Editor))
			{
				Editor::create();
				MissionCleanup.add(Editor);
			}

			if (Canvas.getContent() == EditorGui.getId())
				Editor.close("Hud");
			else
				Editor.open();
		}
		else
            MessageBoxOK("Denied", "Must be hosting a map to edit it.");
	}
}

//------------------------------------------------------------------------------
//  The editor action maps are defined in editor.bind.cs
GlobalActionMap.bind(keyboard, "f11", toggleEditor);
