//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

datablock AudioProfile(BounceGunFireSound)
{
	filename = "share/sounds/rotc/fire2.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(BounceGunProjectileSound)
{
	filename = "share/sounds/rotc/charge5.wav";
	description = AudioCloseLooping3D;
	preload = true;	
};

datablock AudioProfile(BounceGunProjectileExplosionSound)
{
	filename = "share/sounds/rotc/explosion2.wav";
	description = AudioDefault3D;
	preload = true;	
};

datablock AudioProfile(BounceGunProjectileImpactSound)
{
	filename = "share/sounds/rotc/explosion2.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(BounceGunProjectileHitSound)
{
	filename = "share/sounds/rotc/explosion2.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(BounceGunProjectileBounceSound)
{
	filename = "share/sounds/rotc/bounce1.wav";
//	alternate[0] = "share/sounds/rotc/impact3-1.wav";
//	alternate[1] = "share/sounds/rotc/impact3-2.wav";
//	alternate[2] = "share/sounds/rotc/impact3-3.wav";
//	alternate[3] = "share/sounds/rotc/ricochet2-1.wav";
//	alternate[4] = "share/sounds/rotc/ricochet2-1.wav";
//	alternate[5] = "share/sounds/rotc/ricochet2-1.wav";
	description = AudioClose3D;
	preload = true;
};

datablock AudioProfile(BounceGunProjectileMissedEnemySound)
{
	filename = "share/sounds/rotc/flyby1.wav";
	description = AudioClose3D;
	preload = true;
};


