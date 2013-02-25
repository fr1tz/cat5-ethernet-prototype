//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

function OptionsWindow::showWindow(%this, %window)
{
   OptPlayerWindow.visible = false;
   OptGraphicsWindow.visible = false;
   OptAudioWindow.visible = false;
   OptNetworkWindow.visible = false;
   OptControlsWindow.visible = false;
   %window.visible= true;
   %window.onAddedAsWindow();
}











