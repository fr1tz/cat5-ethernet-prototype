//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

if(!isObject(MOTDconn))
{
	new HttpObject(MOTDconn);
	$MOTD::Text = "";
}

function MotdWindow::onAdd(%this)
{
	%this.refresh();
}

function MotdWindow::onWake(%this)
{
	%this.setMotd();
}

function MotdWindow::refresh(%this)
{
	$MOTD::Text = "";
	MotdText.setText("--- FETCHING http://cat5.wasted.ch/motd ---\n");
	MOTDconn.get("cat5.wasted.ch:80", "/motd");
}

function MotdWindow::setMotd()
{
	//echo("MotdWindow::setMotd()");

	if($MOTD::Text $= "")
		return;

	MotdText.setText($MOTD::Text);

	if(MotdWindow.isAwake())
	{
		RootMenuMotdButton.setText("MOTD");
		hilightControl(RootMenuMotdButton, false);
		$Pref::MOTD::Text = $MOTD::Text;
	}
	else if($MOTD::Text !$= $Pref::MOTD::Text)
	{
		RootMenuMotdButton.setText("MOTD *updated*");
		hilightControl(RootMenuMotdButton, true);
	}	
}

// To prevent console spam
function MotdWindow::onAddedAsWindow(%this)
{
}

// To prevent console spam
function MotdWindow::onRemovedAsWindow(%this)
{
}

//------------------------------------------------------------------------------

function MOTDconn::onLine(%this, %line)
{
	//error("MOTDconn::onLine()");
	$MOTD::Text = $MOTD::Text @ %line @ "\n";
}

function MOTDconn::onDNSResolved(%this)
{
	//error("MOTDconn::onDNSResolved()");
	MotdText.addText("--- HOST FOUND ---\n", true);
}

function MOTDconn::onDNSFailed(%this)
{
	//error("MOTDconn::onDNSFailed()");
	MotdText.addText("--- HOST NOT FOUND ---\n", true);
}

function MOTDconn::onConnected(%this)
{
	//error("MOTDconn::onConnected()");
	MotdText.addText("--- BEGIN TRANSMISSION ---\n", true);
}

function MOTDconn::onConnectFailed(%this)
{
	//error("MOTDconn::onConnectFailed()");
	MotdText.addText("--- CONNECTION FAILED ---\n", true);
}

function MOTDconn::onDisconnect(%this)
{
	//error("MOTDconn::onDisconnect()");
	if($MOTD::Text $= "")
		MotdText.addText("\n--- TRANSMISSION EMPTY ---", true);
	else
		MotdWindow.setMotd();
}
