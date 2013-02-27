//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

datablock AudioProfile(PumpgunProjectileImpactSound)
{
	filename = "share/sounds/cat5/impact1a.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(PumpgunProjectileHitSound)
{
	filename = "share/sounds/cat5/impact1b.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(PumpgunProjectileFlybySound)
{
	filename = "share/sounds/cat5/silence.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

datablock AudioProfile(PumpgunProjectileMissedEnemySound)
{
	filename = "share/sounds/cat5/whoosh1.wav";
	description = AudioClose3D;
	preload = true;
};
