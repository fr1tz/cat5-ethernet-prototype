//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

// List of master servers to query, each one 
// is tried in order until one responds...
$Pref::Master[0] = "2:master.garagegames.com:28002";
$Pref::Master[1] = "2:spica.dyndns.info:28002";

// List of AIMS pubservers
$Pref::AIMS::Server[0] = "spica.dyndns.info";

// Default server prefs when starting a server via the GUI
$Pref::Server::RegionMask = 2;
$Pref::Server::Name = "Lazy Hoster's Funhouse";
$Pref::Server::Info =
	"<just:center>" @
	"\n[ Default Arena description ]\n" @
	"<spush><font:Arial:16><color:AA0000>" @
	"Daily meetup time: 8pm CET<spop>\n" @
	"Scheduled matches: None";
$Pref::Server::ConnectionError =
	"You do not have the correct version of Cat5 or "@
	"the related art needed to connect to this server.";
$Pref::Server::Port = 30000;
$Pref::Server::Password = "";
$Pref::Server::AdminPassword = "";
$Pref::Server::MaxPlayers = 64;
$Pref::Server::TimeLimit = 20;	
$Pref::Server::KickBanTime = 300;
$Pref::Server::BanTime = 1800;	
$Pref::Server::FloodProtectionEnabled = 1;
$Pref::Server::MaxChatLen = 120;
$Pref::Server::Mutators = "";

$pref::backgroundSleepTime = "0";
$pref::Shadows = "0.3";
$pref::EnvironmentMaps = "1";
$pref::HostMultiPlayer = "1";
$pref::HudMessageLogSize = 40;
$pref::ChatHudLength = 1;

$Pref::IRC::Name = "newbie";
$Pref::IRC::Server = "irc.freenode.net";
$Pref::IRC::Channel = "#cat5";
$Pref::IRC::ToChat::Status = true;
$Pref::IRC::ToChat::Talk = true;
$Pref::IRC::ToChat::Topic = true;
$Pref::IRC::ToChat::Users = true;

$Pref::Recording::SpeedPreset1 = 0.1;
$Pref::Recording::SpeedPreset2 = 0.25;
$Pref::Recording::SpeedPreset3 = 0.5;
$Pref::Recording::SpeedPreset4 = 2;
$Pref::Recording::SpeedPreset5 = 4;
$Pref::Recording::SpeedPreset6 = 10;
$Pref::Recording::FreelookMoveSpeed = 40;

if($platform $= "windows")
	$pref::Player::Name = "LazyWindowsUser";
else if($platform $= "macos")
	$pref::Player::Name = "LazyMacUser";
else
	$pref::Player::Name = "LazyLinuxUser";
$pref::Player::DefaultFov = 110;
$pref::Player::ZoomSpeed = 200;
$pref::Player::MouseZoomSteps = 10;
$pref::Player::RenderMyPlayer = 0;
$pref::Player::RenderMyItems = 1;
$Pref::Player::Trails::Amount = 1.0;
$Pref::Player::Trails::Detail = 0.0;
$Pref::Player::Trails::Scale = 0.125;
$Pref::Player::Trails::Visibility = 0.9;

$Pref::Precipitation::Enabled = true;

$Pref::Hud::ShowPing = true;
$Pref::Hud::ShowPacketloss = false;
$Pref::Hud::ShowFPS = true;
$Pref::Hud::ShowFPSGraph = false;

$pref::Commander::PanSpeed = 10;
$pref::Commander::ZoomSpeed = 0.05;

$pref::Net::LagThreshold = 400;
$pref::Net::Port = 28000;
$Pref::Net::PacketSize = 450;
$Pref::Net::PacketRateToClient = 32;
$Pref::Net::PacketRateToServer = 32;

$pref::Input::MouseEnabled = 0;
$pref::Input::LinkMouseSensitivity = 1; // really used? -mag
$pref::Input::MouseSensitivity = 0.10;
$pref::Input::InvertMouse = 0;
$pref::Input::InvertMouseWhileFlying = 1;
$pref::Input::JoystickEnabled = 0;
$pref::Input::KeyboardTurnSpeed = 0.1;

$pref::sceneLighting::cacheSize = 20000;
$pref::sceneLighting::purgeMethod = "lastCreated";
$pref::sceneLighting::cacheLighting = 1;
$pref::sceneLighting::terrainGenerateLevel = 1;

$pref::ts::detailAdjust = 1.00;

$pref::Terrain::DynamicLights = 1;
$pref::Interior::TexturedFog = 0;

$pref::Video::displayDevice = "OpenGL";
$pref::Video::allowOpenGL = 1;
$pref::Video::allowD3D = 1;
$pref::Video::preferOpenGL = 1;
$pref::Video::appliedPref = 0;
$pref::Video::disableVerticalSync = 1;
$pref::Video::monitorNum = 0;
$pref::Video::windowedRes = "800 600";
$pref::Video::screenShotFormat = "PNG";

$pref::OpenGL::force16BitTexture = "0";
$pref::OpenGL::forcePalettedTexture = "0";
$pref::OpenGL::maxHardwareLights = 3;
$pref::OpenGL::textureAnisotropy = 1.0;
$pref::OpenGL::textureTrilinear = true;
$pref::VisibleDistanceMod = 1.0;

$pref::Audio::driver = "OpenAL";
$pref::Audio::forceMaxDistanceUpdate = 0;
$pref::Audio::environmentEnabled = 0;
$pref::Audio::masterVolume	= 0.5;
$pref::Audio::channelVolume1 = 0.5;
$pref::Audio::channelVolume2 = 0.5;
$pref::Audio::channelVolume3 = 0.5;
$pref::Audio::channelVolume4 = 0.5;
$pref::Audio::channelVolume5 = 0.5;
$pref::Audio::channelVolume6 = 0.5;
$pref::Audio::channelVolume7 = 0.5;
$pref::Audio::channelVolume8 = 0.5;
$pref::Audio::UseMumbleLink = 0;
