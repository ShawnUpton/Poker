using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker
{
    public class Table
    {

        public Table()
        {
            Players = new List<Player>();
        }

        public List<Player> Players { get; private set; }

        public Player AddPlayer(string name)
        {
            var player = new Player(name);
            Players.Add(player);
            return player;
        }

        public void GetWinner()
        {
            var winningPlayer = new Player(); 
   
            var bestHand = Players.Select(p => p.PlayerHand.Rank).Max();
            var winningPlayers = Players.Where(p => p.PlayerHand.Rank == bestHand);
                           
            if(winningPlayers.Count() == 1)
            {
                winningPlayer = winningPlayers.FirstOrDefault(); ;
                winningPlayer.SetWinner();
                return;   
            }
      
            //Need tie breakers

            //Check for highest Value          
            var highValue = winningPlayers.Select(p => p.PlayerHand.Value).Max();
            winningPlayers = winningPlayers.Where(p => p.PlayerHand.Value == highValue);

            if (winningPlayers.Count() == 1)
            {
                winningPlayer = winningPlayers.FirstOrDefault(); ;
                winningPlayer.SetWinner();
                return;
            }

            //Check for Kicker
            var highKicker = winningPlayers.Select(p => p.PlayerHand.Kicker).Max();
            winningPlayers = winningPlayers.Where(p => p.PlayerHand.Kicker == highKicker);
            if (winningPlayers.Count() == 1)
            {
                winningPlayer = winningPlayers.FirstOrDefault(); ;
                winningPlayer.SetWinner();
                return;
            }

            //No Winner

            return;
    
        }


    }
}
