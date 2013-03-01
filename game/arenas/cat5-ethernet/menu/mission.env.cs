// Mission environment script
// This script is executed from the mission init script in game/server/missions

//------------------------------------------------------------------------------
// Lights
//------------------------------------------------------------------------------

$sgLightEditor::lightDBPath = $Server::MissionDirectory @ "lights/";
$sgLightEditor::filterDBPath = $Server::MissionDirectory @ "filters/";
sgLoadDataBlocks($sgLightEditor::lightDBPath);
sgLoadDataBlocks($sgLightEditor::filterDBPath);

//------------------------------------------------------------------------------
// Material mappings
//------------------------------------------------------------------------------

//%mapping = createMaterialMapping("dark_grey_blue_grid");
//%mapping.sound = $MaterialMapping::Sound::Metal;
//%mapping.color = "0 1 0 1.0 0.0";

//%mapping = createMaterialMapping("malloc/dark_blue_grid");
//%mapping.sound = $MaterialMapping::Sound::Metal;
//%mapping.color = "0 1 0 1.0 0.0";
//%mapping.envmap = "share/textures/malloc/dark_blue_grid 0.5";

//------------------------------------------------------------------------------
// Precipitation
//------------------------------------------------------------------------------

datablock PrecipitationData(MissionRain)
{
   dropTexture = "share/textures/cat5-testmap1/raindrops";
   //splashTexture = "share/textures/cat5-testmap1/raindrops.splash";
   dropSize = 0.40;
   //splashSize = 0.2;
   useTrueBillboards = false;
   splashMS = 200;
};

