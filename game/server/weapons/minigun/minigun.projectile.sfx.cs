//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

datablock AudioProfile(MinigunProjectileImpactSound)
{
	filename = "share/sounds/rotc/impact1.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(MinigunProjectileFlybySound)
{
	filename = "share/sounds/rotc/flyby1.wav";
	description = AudioCloseLooping3D;
	preload = true;
};

datablock AudioProfile(MinigunProjectileMissedEnemySound)
{
	filename = "share/sounds/rotc/ricochet1-1.wav";
	alternate[0] = "share/sounds/rotc/ricochet1-1.wav";
	alternate[1] = "share/sounds/rotc/ricochet1-2.wav";
	alternate[2] = "share/sounds/rotc/ricochet1-3.wav";
	alternate[3] = "share/sounds/rotc/ricochet1-4.wav";
	alternate[4] = "share/sounds/rotc/ricochet1-5.wav";
	alternate[5] = "share/sounds/rotc/ricochet1-6.wav";	
	description = AudioClose3D;
	preload = true;
};
