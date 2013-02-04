//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Cat5 - profiles.cs
// GuiControlProfiles for Cat5' hud
//------------------------------------------------------------------------------

new GuiControlProfile(HudDefaultProfile)
{
	tab = false;
	canKeyFocus = false;
	hasBitmapArray = false;
	mouseOverSelected = false;

	// fill color
	opaque = false;
	fillColor = "255 255 255 100";
	fillColorHL = "255 255 255 100";
	fillColorNA = "255 255 255 100";

	// border color
	border = false;
	borderColor	= "255 255 255 200";
	borderColorHL = "255 255 255 200";
	borderColorNA = "255 255 255 200";

	// font
	fontType = "Arial";
	fontSize = 14;

	fontColor	= "255 255 255 200";
	fontColorHL = "255 255 255 200";
	fontColorNA = "255 255 255 200";
	fontColorSEL= "200 200 200";

	// bitmap information
	bitmap = "./pixmaps/simpleWindow";
	bitmapBase = "";
	textOffset = "0 0";

	// used by guiTextControl
	modal = true;
	justify = "left";
	autoSizeWidth = false;
	autoSizeHeight = false;
	returnTab = false;
	numbersOnly = false;
	cursorColor = "0 0 0 255";

	// sounds
	soundButtonDown = "";
	soundButtonOver = "";
};

new GuiControlProfile(HudBackgroundProfile1 : HudDefaultProfile)
{
	fillColor = "255 255 255 255";
	borderColor = "0 0 0 0";
};

new GuiControlProfile(HudBackgroundProfile2 : HudDefaultProfile)
{
	fillColor = "255 255 255 255";
	borderColor = "0 0 0 0";
};

new GuiControlProfile(HudBackgroundProfile3 : HudDefaultProfile)
{
	fillColor = "255 255 255 255";
	borderColor = "0 0 0 0";
};

new GuiControlProfile(HudWarningProfile : HudDefaultProfile)
{
	fontColor = "255 255 255 200";
	justify = "center";
};

new GuiControlProfile(HudWarningFlashProfile : HudDefaultProfile)
{
	fontColor = "0 255 0 255";
	justify = "center";
};

new GuiControlProfile (ChatHudEditProfile)
{
	opaque = false;
	fillColor = "255 255 255";
	fillColorHL = "128 128 128";
	border = false;
	borderThickness = 0;
	borderColor = "40 231 240";
	cursorColor = "40 231 240";
	fontColor = "40 231 240";
	fontColorHL = "40 231 240";
	fontColorNA = "128 128 128";
	textOffset = "0 2";
	autoSizeWidth = false;
	autoSizeHeight = true;
	tab = true;
	canKeyFocus = true;
};

new GuiControlProfile (ChatHudTextProfile)
{
	opaque = false;
	fillColor = "255 255 255";
	fillColorHL = "128 128 128";
	border = false;
	borderThickness = 0;
	borderColor = "40 231 240";
	fontColor = "40 231 240";
	fontColorHL = "40 231 240";
	fontColorNA = "128 128 128";
	textOffset = "0 0";
	autoSizeWidth = true;
	autoSizeHeight = true;
	tab = true;
	canKeyFocus = true;
};

new GuiControlProfile(HudChatMessageProfile : HudDefaultProfile)
{
	//fontType = "Arial";
	fontSize = 14;
	fontColor = "255 0 0";		// default color (death msgs, scoring, inventory)
	fontColors[1] = "0 255 0";	// client join/drop, tournament mode
	fontColors[2] = "0 100 255"; // gameplay, admin/voting, pack/deployable
	fontColors[3] = "150 250 0";	// team chat, spam protection message, client tasks
	fontColors[4] = "255 255 255";  // global chat
	fontColors[5] = "200 200 50 200";  // used in single player game
	// WARNING! Colors 6-9 are reserved for name coloring
	autoSizeWidth = true;
	autoSizeHeight = true;
};

new GuiControlProfile(HudScrollProfile : HudDefaultProfile)
{
	bitmap = "./pixmaps/simpleScroll";
	hasBitmapArray = true;
};

new GuiControlProfile(HudButtonProfile : HudDefaultProfile)
{
	justify = "center";
};

new GuiControlProfile(HudMediumTextProfile : HudDefaultProfile)
{
	fontSize = 36;
};

new GuiControlProfile(HudMetricsProfile : HudDefaultProfile)
{
	justify = "right";
};

new GuiControlProfile(HudProgressProfile : HudDefaultProfile)
{
	border = true;
	borderColor = "100 100 100 200";
	fillColor = "200 200 200 200";
};



