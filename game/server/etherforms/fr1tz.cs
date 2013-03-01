//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

datablock MultiNodeLaserBeamData(EthFr1tz_LaserTrailOne)
{
	hasLine = false;
	lineColor	= "1.00 1.00 0.00 0.7";

	hasInner = true;
	innerColor = "1.0 1.0 1.0 1.0";
	innerWidth = "1.5";

	hasOuter = false;
	outerColor = "0.9 0.9 0.9 0.5";
	outerWidth = "0.6";

	//bitmap = "share/shapes/rotc/vehicles/team1scoutflyer/lasertrail";
	//bitmapWidth = 1;

	blendMode = 1;
	fadeTime = 300;

   //renderMode = $MultiNodeLaserBeamRenderMode::Horizontal;
};

datablock MultiNodeLaserBeamData(EthFr1tz_LaserTrailTwo)
{
   allowColorization = true;

	hasLine = false;
	lineColor	= "1.00 1.00 0.00 0.7";

	hasInner = false;
	innerColor = "1.0 1.0 1.0 1.0";
	innerWidth = "1.5";

	hasOuter = true;
	outerColor = "0.9 0.9 0.9 0.5";
	outerWidth = "3";

	//bitmap = "share/shapes/rotc/vehicles/team1scoutflyer/lasertrail";
	//bitmapWidth = 1;

	blendMode = 1;
	fadeTime = 300;

   //renderMode = $MultiNodeLaserBeamRenderMode::Horizontal;
};

datablock MultiNodeLaserBeamData(EthFr1tz_LaserTrailThree)
{
   allowColorization = true;

	hasLine = true;
	lineColor	= "0.9 0.9 0.9 0.5";

	hasInner = true;
	innerColor = "0.9 0.9 0.9 0.5";
	innerWidth = "1.5";

	hasOuter = false;
	outerColor = "0.9 0.9 0.9 0.1";
	outerWidth = "0.10";

	//bitmap = "share/shapes/rotc/vehicles/team1scoutflyer/lasertrail";
	//bitmapWidth = 1;

	blendMode = 1;
	fadeTime = 600;

   //renderMode = $MultiNodeLaserBeamRenderMode::Horizontal;
};

datablock EtherformData(EthFr1tz : EthStandard)
{
	shapeFile = "share/shapes/cat5/etherforms/fr1tz.dts";

	// laser trail...
	//laserTrail[0] = EthFr1tz_LaserTrailOne;
	//laserTrail[1] = EthFr1tz_LaserTrailTwo;
	//laserTrail[2] = EthFr1tz_LaserTrailThree;
};
