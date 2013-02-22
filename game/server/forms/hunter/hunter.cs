//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//exec("./standardcat.sfx.cs");
//exec("./standardcat.gfx.cs");

datablock TSShapeConstructor(HunterDts)
{
	baseShape = "share/shapes/cat5/hunter.dts";
	sequenceBaseDir = "share/shapes/rotc/players/a/";

	// movement when standing...
	sequence0  = "tl/root.dsq root";
	sequence1  = "nm/run.dsq run";
	sequence2  = "nm/back.dsq back";
	sequence3  = "nm/side.dsq side";

	// movement when marching...
	sequence4  = "tl/root.dsq rootMarching";
	sequence5  = "nm/run.dsq runMarching";
	sequence6  = "nm/back.dsq backMarching";
	sequence7  = "nm/side.dsq sideMarching";

	// movement when crouched...
	sequence8  = "tl/root.dsq rootCrouched";
	sequence9  = "nm/run.dsq runCrouched";
	sequence10  = "nm/back.dsq backCrouched";
	sequence11  = "nm/side.dsq sideCrouched";

	// movement when prone...
	sequence12 = "fb/rootprone.dsq rootProne";
	sequence13 = "fb/rootprone.dsq rootProne";
	sequence14 = "fb/rootprone.dsq rootProne";
	sequence15 = "fb/rootprone.dsq rootProne";

	// arm threads...
	sequence16 = "a/holdnoweapon.dsq look";
	sequence17 = "a/discdeflect_left_base.dsq discdeflect_left_base";
	sequence18 = "a/holdgun_onehand.dsq look2";
	sequence52 = "a/holdblaster.dsq holdblaster";
	sequence53 = "ub/aimblaster.dsq aimblaster";
	sequence19 = "ub/holdrifle.dsq holdrifle";
	sequence51 = "ub/aimrifle.dsq aimrifle";
	sequence20 = "ub/holdshield.dsq holdshield";
	sequence46 = "a/holdspear.dsq holdspear";
	sequence47 = "a/holdaimspear.dsq holdaimspear";

	// other...
	sequence21 = "nm/diehead.dsq death1";
	sequence22 = "nm/diechest.dsq death2";
	sequence23 = "nm/dieback.dsq death3";
	sequence24 = "nm/diesidelf.dsq death4";
	sequence25 = "nm/diesidert.dsq death5";
	sequence26 = "nm/dieleglf.dsq death6";
	sequence27 = "nm/dielegrt.dsq death7";
	sequence28 = "nm/dieslump.dsq death8";
	sequence29 = "nm/dieknees.dsq death9";
	sequence30 = "nm/dieforward.dsq death10";
	sequence31 = "nm/diespin.dsq death11";

	sequence32 = "nm/headside.dsq headside";
	sequence33 = "nm/recoilde.dsq light_recoil";
	sequence34 = "nm/sitting.dsq sitting";
	sequence35 = "fb/cel_headbang.dsq celsalute";
	sequence36 = "nm/tauntbest.dsq celwave";
	sequence37 = "nm/standjump.dsq standjump";

	sequence38 = "nm/head.dsq head";
	sequence39 = "nm/fall.dsq fall";
	sequence40 = "nm/land.dsq land";
	sequence41 = "nm/jump.dsq jump";

	sequence42 = "fb/cel_hail.dsq celhail";

	sequence43 = "ub/throwsidearm.dsq throwSidearm";
	sequence44 = "ub/aimarmcannon.dsq aimarmcannon";
	sequence48 = "ub/aimspear.dsq aimSpear";
	sequence49 = "ub/throwSpear.dsq throwSpear";
	sequence50 = "ub/discdeflect_left_anim.dsq discdeflect_left_anim";
	sequence54 = "ub/throwinterceptor.dsq throwInterceptor";

	sequence45 = "fb/flyer.dsq flyer";

	sequence55  = "b/slide.dsq slide";
};


datablock PlayerData(FrmHunter)
{
   allowColorization = true;

	className = Cat;
	
	thirdPersonOnly = true;
	
	targetLockTimeMS = 200;
	
	hudImageNameFriendly = "~/client/ui/hud/pixmaps/black.png";
	hudImageNameEnemy = "~/client/ui/hud/pixmaps/black.png";

	useEyePoint = true;
	renderFirstPerson = true;
	emap = true;

    eyeOffset = "0 -0.2 -0.02";
    cameraDefaultFov = 110.0;
	cameraMinFov	  = 30.0;
	cameraMaxFov	  = 130.0;
	cameraMinDist = 1;
	cameraMaxDist = 5;

	shapeFile = "share/shapes/cat5/hunter.dts";
 
	//cloakTexture = "share/shapes/rotc/effects/explosion_white.png";
	shapeFxTexture[0] = "share/textures/rotc/connection2.png";
	shapeFxTexture[1] = "share/textures/rotc/connection.png";
	shapeFxTexture[2] = "share/textures/rotc/barrier.green.png";
	shapeFxTexture[3] = "share/textures/rotc/armor.white.png";
	shapeFxTexture[4] = "share/textures/rotc/armor.orange.png";
	shapeFxTexture[5] = "share/textures/rotc/bounce.orange.hit.png";
	shapeFxTexture[6] = "share/textures/rotc/bounce.orange.miss.png";

	shapeFxColor[0] = "1.0 1.0 1.0 1.0";  
	shapeFxColor[1] = "1.0 0.0 0.0 1.0";  
	shapeFxColor[2] = "1.0 0.5 0.0 1.0";  
	shapeFxColor[3] = "0.0 0.5 1.0 1.0";  

	computeCRC = true;

	canObserve = true;
	cmdCategory = "Clients";

	renderWhenDestroyed = false;
	debrisShapeName = "share/shapes/cat5/shapedebris1.dts";
	debris = StandardCatDebris;

	aiAvoidThis = true;

	minLookAngle = 0;
	maxLookAngle = 0;
	minLookAngleMarching = 0;
	maxLookAngleMarching = 0;
	minLookAngleCrouched = 0;
	maxLookAngleCrouched = 0;
	minLookAngleProne = 0;
	maxLookAngleProne =0 ;

	maxFreelookAngle = 3.0;

	mass = 90;
	drag = 0.0;
	density = 10;
	gravityMod = 1.0;

	maxDamage = 100;
	damageBuffer = 0;
	maxEnergy = 100;

	repairRate = 0.8;
	damageBufferRechargeRate = 0.15;
	damageBufferDischargeRate = 0.15;
	energyRechargeRate = 0.4;

	skidSpeed = 30 * $Server::Game.slowpokemod;
	skidFactor = 0.4;

	flyForce = 10 * 90 * $Server::Game.slowpokemod;

	runForce = 200 * 90 * $Server::Game.slowpokemod; // formerly 48 * 90
	runEnergyDrain = 2;
	minRunEnergy = 5;
	
	slideForce = 20 * 90 * $Server::Game.slowpokemod;
	slideEnergyDrain = 0;
	minSlideEnergy = 0;

	maxForwardSpeed = 20 * $Server::Game.slowpokemod;
	maxBackwardSpeed = 20 * $Server::Game.slowpokemod;
	maxSideSpeed = 20 * $Server::Game.slowpokemod;
	maxForwardSpeedSliding = 30 * $Server::Game.slowpokemod;
	maxBackwardSpeedSliding = 25 * $Server::Game.slowpokemod;
	maxSideSpeedSliding = 12 * $Server::Game.slowpokemod;
	maxForwardSpeedMarching = 12 * $Server::Game.slowpokemod;
	maxBackwardSpeedMarching = 12 * $Server::Game.slowpokemod;
	maxSideSpeedMarching = 12 * $Server::Game.slowpokemod;
//	maxForwardSpeedCrouched = 15; NOT USED
//	maxBackwardSpeedCrouched = 12; NOT USED
//	maxSideSpeedCrouched = 10; NOT USED
//	maxForwardSpeedProne = 3; NOT USED
//	maxBackwardSpeedProne = 3; NOT USED
//	maxSideSpeedProne = 2; NOT USED

	maxUnderwaterForwardSpeed = 8.4 * $Server::Game.slowpokemod;
	maxUnderwaterBackwardSpeed = 7.8 * $Server::Game.slowpokemod;
	maxUnderwaterSideSpeed = 7.8 * $Server::Game.slowpokemod;
	// [todo: insert values for other body poses here?]

	jumpForce = 8 * 90;  // 12 * 90
	jumpEnergyDrain = 0;
   reJumpForce = 10 * 90; // script field
   reJumpEnergyDrain = 20; // script field
	minJumpEnergy = 9999;
	jumpDelay = 0;
	
	recoverDelay = 9;
	recoverRunForceScale = 1.2;

	minImpactSpeed = 30; //
	speedDamageScale = 3.0; // dynamic field: impact damage multiplier

	boundingBox = "1.2 1.2 2.7";
	pickupRadius = 0.75;

	// Controls over slope of runnable/jumpable surfaces
	maxStepHeight = 1.0;
	maxStepHeightMarching = 1.0;
	maxStepHeightCrouched = 1.0;
	maxStepHeightProne = 0.2;
	runSurfaceAngle  = 40;
	runSurfaceAngleMarching  = 40;
	runSurfaceAngleCrouched  = 40;
	runSurfaceAngleProne  = 50;

	jumpSurfaceAngle = 30;

	minJumpSpeed = 20;
	maxJumpSpeed = 30;

	horizMaxSpeed = 200;
	horizResistSpeed = 30;
	horizResistFactor = 0.35;

	upMaxSpeed = 65;
	upResistSpeed = 25;
	upResistFactor = 0.3;

	footstepSplashHeight = 0.35;

	// Damage location details
	boxNormalHeadPercentage		 = 0.83;
	boxNormalTorsoPercentage		= 0.49;
	boxHeadLeftPercentage			= 0;
	boxHeadRightPercentage		  = 1;
	boxHeadBackPercentage			= 0;
	boxHeadFrontPercentage		  = 1;

	// footprints
	//decalData	= StandardCatFootprint;
	//decalOffset = 0.25;

	// foot puffs
	footPuffEmitter = StandardCatFootPuffEmitter;
	footPuffNumParts = 5;
	footPuffRadius = 0.2;
	
	// ground connection beam
	//groundConnectionBeam = StandardCatGroundConnectionBeam;

   numShapeTrails = 10;

	// slide emitters
	//slideContactParticleFootEmitter = GreenSlideEmitter;
	slideContactParticleTrailEmitter[0] = CatSlideContactTrailEmitter;
	slideParticleFootEmitter = CatSlideFootEmitter;
	//slideParticleTrailEmitter[0] = RedSlideEmitter;
	skidParticleFootEmitter = CatSkidFootEmitter;
	skidParticleTrailEmitter[0] = CatSkidTrailEmitter0;
	skidParticleTrailEmitter[1] = CatSkidTrailEmitter1;
	
	// damage info eyecandy...
//	damageBufferParticleEmitter = GreenCatDamageBufferEmitter;
//	repairParticleEmitter = GreenCatRepairEmitter;
//	bufferRepairParticleEmitter = GreenCatBufferRepairEmitter;
//	damageParticleEmitter = GreenCatDamageEmitter;
//	bufferDamageParticleEmitter = GreenCatBufferDamageEmitter;
	damageDebris = BlueCatDamageDebris;
//	bufferDamageDebris = CatBufferDamageDebris;

	// not implemented in engine...
	// dustEmitter = StandardCatLiftoffDustEmitter;

	splash = StandardCatSplash;
	splashVelocity = 4.0;
	splashAngle = 67.0;
	splashFreqMod = 300.0;
	splashVelEpsilon = 0.60;
	bubbleEmitTime = 0.4;
	splashEmitter[0] = StandardCatFoamDropletsEmitter;
	splashEmitter[1] = StandardCatFoamEmitter;
	splashEmitter[2] = StandardCatBubbleEmitter;
	mediumSplashSoundVelocity = 10.0;
	hardSplashSoundVelocity = 20.0;
	exitSplashSoundVelocity = 5.0;

	//NOTE:  some sounds commented out until wav's are available

	// Footstep Sounds
	LeftFootSoftSound		= StandardCatLeftFootSoftSound;
	LeftFootHardSound		= StandardCatLeftFootHardSound;
	LeftFootMetalSound	  = StandardCatLeftFootMetalSound;
	LeftFootSnowSound		= StandardCatLeftFootSnowSound;
	LeftFootShallowSound	= StandardCatLeftFootShallowSplashSound;
	LeftFootWadingSound	 = StandardCatLeftFootWadingSound;
	RightFootSoftSound	  = StandardCatRightFootSoftSound;
	RightFootHardSound	  = StandardCatRightFootHardSound;
	RightFootMetalSound	 = StandardCatRightFootMetalSound;
	RightFootSnowSound	  = StandardCatRightFootSnowSound;
	RightFootShallowSound  = StandardCatRightFootShallowSplashSound;
	RightFootWadingSound	= StandardCatRightFootWadingSound;
	FootUnderwaterSound	 = StandardCatFootUnderwaterSound;
	//FootBubblesSound	  = FootBubblesSound;
	//movingBubblesSound	= ArmorMoveBubblesSound;
	//waterBreathSound	  = WaterBreathMaleSound;

	impactSoftSound		= StandardCatImpactSoftSound;
	impactHardSound		= StandardCatImpactHardSound;
	impactMetalSound	  = StandardCatImpactMetalSound;
	impactSnowSound		= StandardCatImpactSnowSound;

	//impactWaterEasy		= ImpactLightWaterEasySound;
	//impactWaterMedium	 = ImpactLightWaterMediumSound;
	//impactWaterHard		= ImpactLightWaterHardSound;

	groundImpactMinSpeed	 = 10.0;
	groundImpactShakeFreq	= "4.0 4.0 4.0";
	groundImpactShakeAmp	 = "1.0 1.0 1.0";
	groundImpactShakeDuration = 0.8;
	groundImpactShakeFalloff = 10.0;

	//exitingWater			= ExitingWaterLightSound;
	slideSound = PlayerSlideSound;
	slideContactSound = PlayerSlideContactSound;
	skidSound = PlayerSkidSound;

	observeParameters = "0.5 4.5 4.5";
};

if($Server::Game.lowhealth)
	FrmHunter.maxDamage = 50;

function FrmHunter::onAdd(%this, %obj)
{
	Parent::onAdd(%this, %obj);
	%obj.mountImage(StandardCatLightImage, 3);
   %obj.mountImage(CrossbowImage, 0, -1, true);
}


