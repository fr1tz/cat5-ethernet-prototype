//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Cat5 - booze.cs
// Stuff for the in-game drinking game
//------------------------------------------------------------------------------

//datablock AudioProfile(SaufenSaufenSaufenSound)
//{
//	filename = "share/sounds/rotc/events/saufen.wav";
//	description = Audio2D;
//	preload = true;
//};

$SAUFEN_SAUFEN_SAUFEN = false;
$SAUFEN_INTERVAL = 4*60*1000;
$SAUFEN_DIVIDEND = 400;

function startDrinkingGame()
{
	$SAUFEN_SAUFEN_SAUFEN = true;
	
	$Team1.boozeAmount = 0;
	$Team2.boozeAmount = 0;
	
	$saufThread = schedule($SAUFEN_INTERVAL,0,"saufenSaufenSaufen");
}

function endDrinkingGame()
{
	$SAUFEN_SAUFEN_SAUFEN = false;
	cancel($saufThread);
}

function saufenSaufenSaufen()
{
	// re-calculate b00ze amount
	$Team1.boozeAmount = $Team1.boozeAmount / $Team1.numPlayers / $SAUFEN_DIVIDEND;
	$Team2.boozeAmount = $Team2.boozeAmount / $Team2.numPlayers / $SAUFEN_DIVIDEND;
	
	serverPlay2D(SaufenSaufenSaufenSound);
	
	centerPrintAll("BIERMACHT! BIERMACHT! BIERMACHT!\n\n" @
						"All" SPC $Team1.name SPC "are required to drink" SPC $Team1.boozeAmount SPC "shots\n" @
						"and all" SPC $Team2.name SPC "are required to drink" SPC $Team2.boozeAmount SPC "shots\n" @
						"\n resume play after this message has disappeared!"
						,60);

	// re-start...
	startDrinkingGame();
}
