// Mission script
// This script is executed from the mission init script in game/server/missions

function sMissionCallback_onBotDestroyed(%data, %obj)
{
   // Replace lost bot
   sMission_addBot(%obj.teamId, %obj.zClass);
}

function sMissionCallback_onNewRound()
{
   schedule(1000, $Server::Game, "sMission_addBots");
}

function sMission_addBot(%team, %class)
{
   %bot = aiAdd(%team, %class);
   xxx_aiStartMove(%bot);
   xxx_aiStartFire(%bot);
}

function sMission_addBots()
{
   for(%team = 1; %team <= 2; %team++)
   {
      sMission_addBot(%team, 1);
      sMission_addBot(%team, 2);
      sMission_addBot(%team, 4);
      sMission_addBot(%team, 5);
   }
}
