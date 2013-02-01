//------------------------------------------------------------------------------
// Cat5
// Copyright notices are in the file named COPYING.
//------------------------------------------------------------------------------

package Duel {

function onServerCreated()
{
    Parent::onServerCreated();
    $Server::ModString = "duel";
   	exec("./game.cs");
}

}; // package Duel
