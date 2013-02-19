//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

datablock AudioProfile(MinigunProjectileImpactSound)
{
	filename = "share/sounds/cat5/silence.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(MinigunProjectileFlybySound)
{
	filename = "share/sounds/cat5/silence.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

datablock AudioProfile(MinigunProjectileMissedEnemySound)
{
	filename = "share/sounds/cat5/silence.wav";
	alternate[0] = "share/sounds/cat5/silence.wav";
	alternate[1] = "share/sounds/cat5/silence.wav";
	alternate[2] = "share/sounds/cat5/silence.wav";
	alternate[3] = "share/sounds/cat5/silence.wav";
	alternate[4] = "share/sounds/cat5/silence.wav";
	alternate[5] = "share/sounds/cat5/silence.wav";
	description = AudioClose3D;
	preload = true;
};
