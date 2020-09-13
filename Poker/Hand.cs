using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.ComponentModel;

namespace Poker
{
    public enum HandRank
    {       
        HighCard,
        Pair,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        RoyalFlush
    }
    public class Hand
    {
        private const int CARDS_IN_HAND = 5;
        private const int HIGH_CARD_INDEX = 4;

        public HandRank Rank { get; private set; }

        public Card[] Cards { get; private set; }     
        
        /// <summary>
        ///// Value of hand will be used to determine winner in scenarios where players have the same HandRank
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// Kicker will be used to determine winner in scenarios where players have the same HandRank and Value
        /// </summary>
        public Rank Kicker { get; private set; }


        public void Deal(Card[] deck, int playerNum)
        {
            Cards = new Card[CARDS_IN_HAND];            
            var dealStart = playerNum > 1 ? (playerNum-1) * CARDS_IN_HAND : 0; 
            var dealEnd = dealStart + CARDS_IN_HAND;
            var handIterator = 0;
            for (var i = dealStart; i < dealEnd; i++)
            {
                Cards[handIterator] = deck[i];
                handIterator++;
            } 
        }

        public void Evaluate()
        {
            Rank = HandRank.HighCard;
            var sortedHand = SortHand();
            var rankGroup = sortedHand.GroupBy(card => card.Rank);
            if (IsRoyalFlush(sortedHand))
                return;
            if (IsStraigtFlush(sortedHand))
                return;
            if (IsFourOfAKind(rankGroup))
                return; 
            if (IsFullHouse(rankGroup))
                return;
            if (IsFlush(sortedHand))
                return;
            if (IsStraight(sortedHand))
                return;
            if (IsThreeOfAKind(rankGroup))
                return;
            if (IsTwoPair(rankGroup))
                return;
            if (IsPair(rankGroup))
                return;   
      
        }

        private Card[] SortHand()
        {
            return Cards.OrderBy(c => c.Rank).ToArray();

        }
        private bool IsPair(IEnumerable<IGrouping<Rank,Card>> rankGroup)
        {
            return IsGrouping(rankGroup, HandRank.Pair);
        }

        private bool IsTwoPair(IEnumerable<IGrouping<Rank, Card>> rankGroup)
        {
            if (rankGroup.Count(g => g.Count() == 2) == 2)
            {
                var firstPairRank = rankGroup.First(g => g.Count() == 2).Key;
                var secondPairRank = rankGroup.Last(g => g.Count() == 2).Key;                
                Value = ((int)firstPairRank * 2) + ((int)secondPairRank * 2);
                Kicker = rankGroup.OrderByDescending(g => g.Key).FirstOrDefault(g => g.Count() == 1).Key;
                Rank = HandRank.TwoPair;
                return true;
            }

            return false;
        }

        private bool IsThreeOfAKind(IEnumerable<IGrouping<Rank, Card>> rankGroup)
        {
            return IsGrouping(rankGroup, HandRank.ThreeOfAKind);
        }

        private bool IsStraight(Card[] hand)
        {
            //Special Case to check for low Ace straigt
            var endIndex = hand.Length;
            if (hand[HIGH_CARD_INDEX].Rank == Poker.Rank.Ace && hand[0].Rank == Poker.Rank.Two)
            {
                endIndex--;
            }

            if (Enumerable.Range(0, endIndex).All(i => (int)hand[i].Rank == (int)hand[0].Rank + i))
            {       
                Value = (int)hand[HIGH_CARD_INDEX].Rank; //Highest card wins tie
                Rank = HandRank.Straight;
                return true;
            }  
            return false;            
        }

        private bool IsFlush(Card[] hand)
        {
            if(hand.All(h => h.Suit == hand[0].Suit))
            {               
                Value = (int)hand[HIGH_CARD_INDEX].Rank; //Highest card wins tie
                Rank = HandRank.Flush;
                return true;
            }
            return false;
        }

        private bool IsFullHouse(IEnumerable<IGrouping<Rank, Card>> rankGroup)
        {
            if(IsPair(rankGroup) && IsThreeOfAKind(rankGroup))
            {
                Rank = HandRank.FullHouse;
                return true;
            }
            return false;
        }

        private bool IsFourOfAKind(IEnumerable<IGrouping<Rank, Card>> rankGroup)
        {
            return IsGrouping(rankGroup, HandRank.FourOfAKind);
        }

        private bool IsStraigtFlush(Card[] hand)
        {
            if(IsStraight(hand) && IsFlush(hand))
            {
                Value = (int)hand[HIGH_CARD_INDEX].Rank; //Highest card wins tie
                Rank = HandRank.StraightFlush;
                return true;
            }
            return false;
        }

        private bool IsRoyalFlush(Card[] hand)
        {
            if (IsStraight(hand) && IsFlush(hand) && hand[HIGH_CARD_INDEX].Rank == Poker.Rank.Ace)
            {
                Rank = HandRank.RoyalFlush;
                return true;
            }
            return false;
        }


        private bool IsGrouping(IEnumerable<IGrouping<Rank, Card>> rankGroup, HandRank rank)
        {
            var groupCouunt = 0;
            switch (rank)
            {
                case HandRank.Pair:
                    groupCouunt = 2;
                    break;     
                case HandRank.ThreeOfAKind:
                    groupCouunt = 3;
                    break;
                case HandRank.FourOfAKind:
                    groupCouunt = 4;
                    break;   
            }

            if (groupCouunt == 0)
                return false;

            
            if (rankGroup.Any(g => g.Count() == groupCouunt))
            {
                var cardRank = rankGroup.First(g => g.Count() == groupCouunt).Key;
                Value = (int)cardRank * groupCouunt;
                //Highest Card that is not part of the grouping wins tie, unless this is a full house then lower pair is the kicker
                Kicker = rankGroup.OrderByDescending(g => g.Key).FirstOrDefault(g => g.Count() == 1)?.Key 
                    ?? rankGroup.OrderByDescending(g => g.Key).FirstOrDefault(g => g.Count() == 2).Key; 
                Rank = rank;
                return true;
            }

            return false;
        }


    }
}
