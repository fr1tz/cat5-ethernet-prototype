//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Cat5 - aiPlayer.cs
// code for (currently stupid) practice ai opponents
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// AIPlayer callbacks
// The AIPlayer class implements the following callbacks:
//
//	 PlayerData::onStuck(%this,%obj)
//	 PlayerData::onUnStuck(%this,%obj)
//	 PlayerData::onStop(%this,%obj)
//	 PlayerData::onMove(%this,%obj)
//	 PlayerData::onReachDestination(%this,%obj)
//	 PlayerData::onTargetEnterLOS(%this,%obj)
//	 PlayerData::onTargetExitLOS(%this,%obj)
//	 PlayerData::onAdd(%this,%obj)
//
// Since the AIPlayer doesn't implement it's own datablock, these callbacks
// all take place in the PlayerData namespace.
//-----------------------------------------------------------------------------

function aiAdd(%teamid, %class)
{
	if( !isObject($aiPlayers) )
	{
		$aiPlayers = new SimSet();
		MissionCleanup.add($aiPlayers);
	}
	
	if( !isObject($aiPlayersPseudoClient) )
	{
		$aiPlayersPseudoClient = new ScriptObject();
		$aiPlayersPseudoClient.handicap = 1.0;
		GameConnection::defaultLoadout($aiPlayersPseudoClient);
		GameConnection::updateLoadout($aiPlayersPseudoClient);
		MissionCleanup.add($aiPlayersPseudoClient);
	}
	

	%nameadd = $aiPlayers.count();

	%spawnSphere = pickSpawnSphere(%teamid);
	
	if($Game::GameType == $Game::GridWars)
	{
		if(%teamid == 1)
			%playerData = RedInfantryCat;
		else
			%playerData = BlueInfantryCat;
	}
	else if($Game::GameType == $Game::Infantry)
	{
		if(%teamid == 1)
			%playerData = GreenInfantryCat;
		else
			%playerData = RedInfantryCat;
	}
	else
	{
      if(%class == 1)
	      %playerData = FrmGrunt;
      else if(%class == 2)
	      %playerData = FrmShotgunner;
      else if(%class == 3)
	      %playerData = FrmMinigunner;
      else if(%class == 4)
	      %playerData = FrmSpecialist;
      else if(%class == 5)
         %playerData = FrmHunter;
	}

	%player = new AiPlayer() {
		dataBlock = %playerData;
		client = $aiPlayersPseudoClient;
		path = "";
		teamId = %teamid;
	};
	MissionCleanup.add(%player);

	if($Server::Game.tagMode == $Server::Game.alwaystag)
		%player.setTagged();

	%pos = getRandomHorizontalPos(%spawnSphere.position,%spawnSphere.radius);
	%player.setShapeName("Target" @ %nameadd);
	%player.setTransform(%pos);
   %player.zClass = %class;

	%player.aiWeapon = %weaponNum;
   if(%weaponNum == 3 || %weaponNum == 4)
   	%player.aiCharge = 1100;
   else
   	%player.aiCharge = 100;

	$aiPlayers.add(%player);
	$aiPlayersPseudoClient.weapons[0] = %weaponNum;
	$aiPlayersPseudoClient.numWeapons = 1;

	%player.useWeapon(1);

	return %player;
}

//-----
// called by user
//-----

function aiStartMove() {
	for( %i = 0; %i < $aiPlayers.getCount(); %i++ ) {
		xxx_aiStartMove($aiPlayers.getObject(%i));
	}
}
function aiStartFire() {
	for( %i = 0; %i < $aiPlayers.getCount(); %i++ ) {
		xxx_aiStartFire($aiPlayers.getObject(%i));
	}
}
function aiStartFight() {
	for( %i = 0; %i < $aiPlayers.getCount(); %i++ ) {
		xxx_aiStartMove($aiPlayers.getObject(%i));
		xxx_aiStartFire($aiPlayers.getObject(%i));
	}
}
function aiKill() {
	for( %i = 0; %i < $aiPlayers.getCount(); %i++ ) {
		%player = $aiPlayers.getObject(%i);
		%player.kill();
	}
	$aiPlayers.empty();
}

function aiWayneStopFire()
{

}


//-----
// called non-interactively
//-----

function xxx_aiStartMove(%player)
{
	%player.updateMove = schedule(1000,%player,"xxx_aiUpdateMove",%player);
}

function xxx_aiStartFire(%player)
{
	%player.setImageLoaded(0,true);
	%player.targetUpdateThread = schedule(100,%player,"xxx_aiUpdateTarget",%player);
	%player.fireThread = schedule((getRandom(3)+1)*1000,%player,"xxx_aiFire",%player);
}

function xxx_aiFire(%player)
{
	%target = %player.getAimObject();
	if(isObject(%target))
	{
		%x = 0;
		%y = 0;
		%z = 0;

	  %enemypos = %player.getAimLocation();
	  %mypos = %player.getPosition();
	  %dist = VectorDist(%mypos, %enemypos);

	  %z = %dist/30;

		%offset = %x SPC %y SPC %z;

		%player.setAimObject(%target, %offset);
		%player.setImageTrigger(0,true);
	}
	
	%player.fireReleaseThread = schedule(%player.aiCharge,%player,xxx_aiFireRelease,%player);
}

function xxx_aiFireRelease(%player)
{
	%player.setImageTrigger(0,false);
	%player.fireThread = schedule(getRandom(1000),%player,xxx_aiFire,%player);

    // try to throw disc
	%player.setImageTrigger(3, true);
    %player.fireReleaseThread = %player.schedule(0, setImageTrigger, 3, false);
}

function xxx_aiUpdateTarget(%player)
{
	%target = 0; //$aiTarget;

	%position = %player.getPosition();
	%radius = 500;

	InitContainerRadiusSearch(%position, %radius, $TypeMasks::PlayerObjectType);
	while ((%targetObject = containerSearchNext()) != 0)
	{
		if( %targetObject.teamId > 0
		 && %targetObject.getDamageState() $= "Enabled"
		 && %targetObject.teamId != %player.teamId )
		{
			%target = %targetObject;
			break;
		}
	}
	
	if(%target != 0)
		%player.setAimObject(%target, "0 0 1");
	
	%player.targetUpdateThread = schedule(2500,%player,xxx_aiUpdateTarget,%player);
}

function xxx_aiFindMoveDestination(%player)
{
   %pos = %player.getPosition();
   %dist = 500;
   %dest = "";
	InitContainerRadiusSearch(%pos, %dist, $TypeMasks::TacticalZoneObjectType);
	while((%srchObj = containerSearchNext()) != 0)
   {
      if(%srchObj.aiIgnore) continue;
      if(%srchObj.getTeamId() == %player.getTeamId()) continue;

      %d = VectorLen(VectorSub(%srchObj.getPosition(), %pos));
      if(%d < %dist)
      {
         %dest = %srchObj;
         %dist = %d;
         //error(%dist);
      }
   }
   //error(%pos SPC "->" SPC %dest.getPosition());
   %player.zMoveDestination = %dest;
}

function xxx_aiUpdateMove(%player)
{
//	%player.moveThread = schedule((getRandom(1)+1)*1000,%player,xxx_aiUpdateMove,%player);
	%player.moveThread = schedule(500,%player,xxx_aiUpdateMove,%player);

   %findNewMoveDestination = false;
   if(!isObject(%player.zMoveDestination))
      %findNewMoveDestination = true;
   else if(%player.zMoveDestination.getTeamId() == %player.getTeamId())
      %findNewMoveDestination = true;

   if(%findNewMoveDestination)
   {
      xxx_aiFindMoveDestination(%player);
      if(!isObject(%player.zMoveDestination))
         return;
   }

   %currPos = %player.getPosition();
   %targetPos = %player.zMoveDestination.getPosition();
   //%targetPos = %player.getAimObject().getPosition();
   //%targetPos = "-45 89 20";
   %targetVec = VectorNormalize(VectorSub(%targetPos, %currPos));
   %newPos = VectorAdd(%currPos, VectorScale(%targetVec, 10));

   %player.setMoveDestination(%currPos);
   %n = 50;
   while(%n-- > 0)
   {
      %dest = getTerrainLevel(getRandomHorizontalPos(%newPos, 10));
      InitContainerRadiusSearch(%dest, 0.0001, $TypeMasks::TacticalZoneObjectType);
      %obj = containerSearchNext();
      //error(%obj SPC "->" SPC %obj.aiIgnore);
		if(%obj == 0 || %obj.aiIgnore) continue;

	   %heightdiff = getTerrainHeight(getTerrainLevel(%pos)) - getTerrainHeight(%dest);
	   if(%heightdiff == 0) {
		   %player.setMoveDestination(%dest);
         break;
	   }
   }
}

