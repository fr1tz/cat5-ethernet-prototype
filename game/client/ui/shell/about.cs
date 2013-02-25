//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

function AboutWindow::onAddedAsWindow(%this)
{
   //
}

function AboutWindow::show(%this, %title, %filename)
{
   AboutWindowLabel.setText(%title);

	%fo = new FileObject();
	%fo.openForRead(%filename);
	%text = "";
	while(!%fo.isEOF())
   {
      %line = %fo.readLine();
		%text = %text @ %line @ "\n";
   }

	%fo.delete();

	AboutText.setText("<font:Cat5:14>" @ %text);

   addWindow(AboutWindow);
}

function AboutWindow::showReadme(%this)
{
   %this.show("Readme", "README");
}

function AboutWindow::showNews(%this)
{
   %this.show("Changelog", "NEWS");
}

function AboutWindow::showCredits(%this)
{
   %this.show("Credits", "CREDITS");
}

function AboutWindow::showCopying(%this)
{
   %this.show("Legal", "COPYING");
}






