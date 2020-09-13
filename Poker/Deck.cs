using System;


namespace Poker
{
    public class Deck
    {
        private const int CARDS_IN_DECK = 52;
        private const int SHUFFLE_COUNT = 100;

        public Card[] Cards { get; private set; }
        public Deck() 
        {
            InitializeDeck();
        }

        private void InitializeDeck()
        {

            Cards = new Card[CARDS_IN_DECK];
            var i = 0;
            foreach(Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach(Rank value in Enum.GetValues(typeof(Rank)))
                {
                    Cards[i] = new Card(suit, value);           
                    i++;
                }
            }

            for (var n = 0; n < SHUFFLE_COUNT; n++)
            {
                ShuffleDeck();
            }
        }
        private void ShuffleDeck()
        {
            //using Fisher–Yates shuffle(https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle)
            Random r = new Random();
            for (int n = Cards.Length - 1; n > 0; --n)
            {
                int k = r.Next(n + 1);
                var temp = Cards[n];
                Cards[n] = Cards[k];
                Cards[k] = temp;
            }
        }       

    }
}
