//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------


datablock DecalData(FrmMinigunnerBloodDecal)
{
	sizeX = "3.0";
	sizeY = "3.0";
	textureName = "share/textures/cat5/bluedecal1";
	SelfIlluminated = true;
};

datablock DebrisData(FrmMinigunnerDebris)
{
	explodeOnMaxBounce = false;

	elasticity = 0.35;
	friction = 0.5;

	lifetime = 4.0;
	lifetimeVariance = 1.0;

	minSpinSpeed = 60;
	maxSpinSpeed = 600;

	numBounces = 5;
	bounceVariance = 0;

	staticOnMaxBounce = true;
	gravModifier = 4.0;

	useRadiusMass = true;
	baseRadius = 0.5;

	velocity = 18.0;
	velocityVariance = 12.0;

   terminalVelocity = 0;

   emitters[0] = StandardCatDebris_SmokeEmitter;
   //emitters[1] = StandardCatDebris_FireEmitter;

   decals[0] = FrmMinigunnerBloodDecal;
};
