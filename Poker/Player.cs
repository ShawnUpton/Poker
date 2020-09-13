using System;


namespace Poker
{
    public class Player
    {
        public Player()
        {        
        }
        public Player(string name)
        {
            Name = name;
        }
        public string Name { get; set; }

        public bool IsWinner { get; private set; }

        public Hand PlayerHand { get; private set; } 

        public void DealHand(Card[] deck, int playerNum)
        {
            var playerHand = new Hand();
            playerHand.Deal(deck, playerNum);
            PlayerHand = playerHand;
        }

        public void SetCustomHand(Card[] hand)
        {
            var playerHand = new Hand();
            playerHand.Custom(hand);
            PlayerHand = playerHand;
        }

        public void SetWinner()
        {
            IsWinner = true;
        }

        





    }
}
