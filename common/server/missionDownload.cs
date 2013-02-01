//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Mission Loading
// The server portion of the client/server mission loading process
//-----------------------------------------------------------------------------

//--------------------------------------------------------------------------
// Loading Phases:
// Phase 0: Send Cookie requests
// Phase 1: Transmit Material Mappings
//          Transmit Datablocks
//	         Transmit targets
// Phase 2: Transmit Ghost Objects
// Phase 3: Start Game
//
// The server invokes the client MissionStartPhase[1-3] function to request
// permission to start each phase.  When a client is ready for a phase,
// it responds with MissionStartPhase[1-3]Ack.

function GameConnection::loadMission(%this)
{
	// Send over the information that will display the server info
	// when we learn it got there, we'll send the data blocks
	%this.currentPhase = 0;
	if (%this.isAIControlled())
	{
		// Cut to the chase...
		%this.onClientEnterGame();
	}
	else
	{
		echo("*** Sending cookie requests to client.");

      if(isObject(%this.cookies))
         %this.cookies.delete();
      %this.cookies = new Array();
      %this.prepareCookies(%this.cookies);
      for(%i = 0; %i < %this.cookies.count(); %i++)
      {
         //error("Request for" SPC %this.cookies.getKey(%i));
         commandToClient(%this, 'CookieRequest', %this.cookies.getKey(%i));
      }

		echo("*** Sending mission load to client: " @ $Server::MissionFile);

		commandToClient(%this, 'MissionStartPhase1', $missionSequence,
			$Server::MissionEnvironmentFile);

		%this.onClientLoadMission();
	}
}

function serverCmdMissionStartPhase1Ack(%client, %seq)
{
	// Make sure to ignore calls from a previous mission load
	if (%seq != $missionSequence || !$MissionRunning)
		return;
	if (%client.currentPhase != 0)
		return;
	%client.currentPhase = 1;

   // Let the game know the cookies are ready
   %client.onCookiesReceived(%client.cookies);
   %client.cookies.delete();

	// Start with the CRC
	%client.setMissionCRC( $missionCRC );

   // Send the material mappings
	sendMaterialMappingsToClient(%client);

	// Send over the datablocks...
	// OnDataBlocksDone will get called when have confirmation
	// that they've all been received.
	%client.transmitDataBlocks($missionSequence);
}

function GameConnection::onDataBlocksDone( %this, %missionSequence )
{
	// Make sure to ignore calls from a previous mission load
	if (%missionSequence != $missionSequence)
		return;
	if (%this.currentPhase != 1)
		return;
	%this.currentPhase = 1.5;

	// On to the next phase
	commandToClient(%this, 'MissionStartPhase2', $missionSequence, $Server::MissionFile);
}

function serverCmdMissionStartPhase2Ack(%client, %seq)
{
	// Make sure to ignore calls from a previous mission load
	if (%seq != $missionSequence || !$MissionRunning)
		return;
	if (%client.currentPhase != 1.5)
		return;
	%client.currentPhase = 2;

	// Update mod paths, this needs to get there before the objects.
	%client.transmitPaths();

	// Start ghosting objects to the client
	%client.activateGhosting();
	
}

function GameConnection::clientWantsGhostAlwaysRetry(%client)
{
	if($MissionRunning)
		%client.activateGhosting();
}

function GameConnection::onGhostAlwaysFailed(%client)
{

}

function GameConnection::onGhostAlwaysObjectsReceived(%client)
{
	// Ready for next phase.
	commandToClient(%client, 'MissionStartPhase3', $missionSequence, $Server::MissionFile);
}

function serverCmdMissionStartPhase3Ack(%client, %seq)
{
	// Make sure to ignore calls from a previous mission load
	if(%seq != $missionSequence || !$MissionRunning)
		return;
	if(%client.currentPhase != 2)
		return;
	%client.currentPhase = 3;

	// Server is ready to drop into the game
	%client.startMission();
	%client.onClientEnterGame();
}
