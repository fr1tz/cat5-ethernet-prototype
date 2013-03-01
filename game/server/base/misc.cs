//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Cat5 - misc.cs
// various code snippets which are too small to jusitfy an own file for them
//------------------------------------------------------------------------------

datablock ParticleEmitterNodeData(DefaultEmitterNode)
{
	timeMultiple = 1;
};

datablock ParticleEmitterNodeData(DoubleTimeEmitterNode)
{
	timeMultiple = 2;
};

datablock ParticleEmitterNodeData(HalfTimeEmitterNode)
{
	timeMultiple = 0.5;
};

// hook into the mission editor...
function fxLightData::create(%data)
{
	// The mission editor invokes this method when it wants to create
	// an object of the given datablock type.
	%obj = new fxLight() {
		dataBlock = %data;
	};
	return %obj;
}

datablock AudioProfile(BipMessageSoundOne)
{
	filename = "share/sounds/cat5/beep1a.wav";
	description = AudioCritical2D;
	preload = true;
};

datablock AudioProfile(BipMessageSoundTwo)
{
	filename = "share/sounds/cat5/beep1b.wav";
	description = AudioCritical2D;
	preload = true;
};

datablock AudioProfile(BeepMessageSound)
{
	filename = "share/sounds/cat5/silence.wav";
	description = AudioCritical2D;
	preload = true;
};

datablock AudioProfile(ChatMessageSound)
{
	filename = "share/sounds/cat5/silence.wav";
	description = AudioCritical2D;
	preload = true;
};

datablock AudioProfile(VictorySound)
{
	filename = "share/sounds/cat5/ditty1up.ogg";
	description = AudioCritical2D;
	preload = true;
};

datablock AudioProfile(DefeatSound)
{
	filename = "share/sounds/cat5/ditty1down.ogg";
	description = AudioCritical2D;
	preload = true;
};

datablock AudioProfile(DamageSoundOne)
{
	filename = "share/sounds/cat5/silence.wav";
	description = AudioCritical2D;
	preload = true;
};

datablock AudioProfile(DamageSoundTwo)
{
	filename = "share/sounds/cat5/silence.wav";
	description = AudioCritical2D;
	preload = true;
};

function serverCmdTaggedToTarget(%client)
{
	if( isObject(%client.player) )
	{
		%player = %client.player;
		
		// check if player's able to select target...
		if( getSimTime() < %player.targetSelectTime + 2000 )
			return;

		// want to target current target = clear current target...
		%oldTarget = %player.getCurrTarget();
		%newTarget = %player.getCurrTagged();
		if( %newTarget == %oldTarget )
			%newTarget = 0;

		// notify old target that he's not targeted anymore...
//		if( isObject(%oldTarget) )
//		{
//			commandToClient(%oldTarget.client,'DecreaseLockCount');
//			%oldTarget.client.play2D(TargetLockDecreaseSound);
//		}

		// notify new target that he's now targeted
		if( isObject(%newTarget) )
		{
			%newTarget.client.play2D(TargetLockAquiredSound);
		}

		%player.setCurrTarget(%newTarget);
		%player.client.play2D(TargetLockAquiredSound);
		%player.targetSelectTime = getSimTime();
	}
}

function createExplosion(%data, %pos, %norm)
{
	%visibleDistance = getVisibleDistance();
	%count = ClientGroup.getCount();
	for(%i = 0; %i < %count; %i++)
	{
		%client = ClientGroup.getObject(%i);
		if(%client.ingame $= "")
			continue;

		%control = %client.getControlObject();
		%dist = VectorLen(VectorSub(%pos, %control.getPosition()));

		// can the player potentially see it?
		if(%dist <= %visibleDistance)
		{
			createExplosionOnClient(%client, %data, %pos, %norm);
		}	
		else
		{
			// Perhaps the player can hear it?
			// (The 'play3D' engine method does the distance check.)
			%soundProfile = %data.soundProfile;
			if(isObject(%soundProfile))
				%client.play3D(%soundProfile, %pos);
		}		
	}
}

function setTimeScale(%x)
{
	$timeScale = %x;
	%count = ClientGroup.getCount();
	for (%i = 0; %i < %count; %i++)
	{
		%cl = ClientGroup.getObject(%i);
		if( !%cl.isAIControlled() )
			commandToClient(%cl, 'SetTimeScale', %x);
	}
}

function getVisibleDistance()
{
	%sky = nameToID("Sky");
	if(%sky != -1)
	{
		return %sky.visibleDistance;
	}
	else
	{
		error("getVisibleDistance(): 'Sky' not found to get visible distance from");
		return 1000;
	}
}

