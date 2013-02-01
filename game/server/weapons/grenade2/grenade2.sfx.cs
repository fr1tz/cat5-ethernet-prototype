//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Cat5 - grenade2.sfx.cs
// Sound effects for the grenade2
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// 3D Sounds
//------------------------------------------------------------------------------

datablock AudioProfile(Grenade2ThrowSound)
{
   filename = "share/sounds/rotc/throw1.wav";
   description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(Grenade2BounceSound)
{
   filename = "share/sounds/rotc/bounce1.wav";
   description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(Grenade2ExplodeSound)
{
   filename = "share/sounds/rotc/explosion7.wav";
   description = AudioFar3D;
	preload = true;
};

datablock AudioProfile(Grenade2ProjectileSound)
{
	filename = "share/sounds/rotc/missile1.wav";
	description = AudioDefaultLooping3d;
	preload = true;
};

datablock AudioProfile(Grenade2ChargeSound)
{
	filename = "share/sounds/rotc/charge3.wav";
	description = AudioCloseLooping3D;
	preload = true;
};
