using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Poker;

namespace Poker.Models
{
    public class EvaluateHandViewModel: Table
    {
        public EvaluateHandViewModel()
        {
        }

        [Range(1, 8, ErrorMessage = "Must use 1 - 8 players.")]
        [DisplayName("Number of Players")]        
        public int PlayerCount { get; set; }  
   
        
        public void PlayPoker()
        {
            DealCards();      
            EvaluateHands();
        }

        public void DealCards()
        {
            var deck = new Deck();
            for (int i = 1; i <= PlayerCount; i++)
            {
                var player = AddPlayer($"Player {i}");
                player.DealHand(deck.Cards, i);
            }
        }

        public void EvaluateHands()
        {
            foreach(var player in Players)
            {
                player.PlayerHand.Evaluate();                
            }

            GetWinner();
        }

        public void GetWinner()
        {
            SetWinner();
        }
    
    }
}
