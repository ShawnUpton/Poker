using NUnit.Framework;
using System.Runtime.CompilerServices;
using Poker;
using Poker.Models;
using System.Linq;

namespace PokerHandRank.Test
{
    public class Tests
    {
        private EvaluateHandViewModel _model;
        [SetUp]
        public void Setup()
        {
            _model = new EvaluateHandViewModel();
        }

        [Test]
        public void Test1()
        {
       
            var hands = new Card[][]
            {
                new Card[] {
                    new Card(Suit.Clubs, Rank.Eight),
                    new Card(Suit.Clubs, Rank.Ten),
                    new Card(Suit.Clubs, Rank.Three),
                    new Card(Suit.Clubs, Rank.Nine),
                    new Card(Suit.Clubs, Rank.Ace),
                },
                new Card[] {
                    new Card(Suit.Clubs, Rank.Three),
                    new Card(Suit.Diamonds, Rank.Three),
                    new Card(Suit.Hearts, Rank.Two),
                    new Card(Suit.Spades, Rank.Three),
                    new Card(Suit.Spades, Rank.Two),
                },
                new Card[] {
                    new Card(Suit.Clubs, Rank.Eight),
                    new Card(Suit.Diamonds, Rank.Eight),
                    new Card(Suit.Spades, Rank.Eight),
                    new Card(Suit.Hearts, Rank.Eight),
                    new Card(Suit.Clubs, Rank.Ace),
                }
            };

            _model.PlayCustomHands(hands);

            Assert.IsFalse(_model.Players[0].IsWinner);
            Assert.IsFalse(_model.Players[1].IsWinner);
            //Four of kind wins
            Assert.IsTrue(_model.Players[2].IsWinner);
          
        }

        [Test]
        public void Test2()
        {

            var hands = new Card[][]
            {
                new Card[] {
                    new Card(Suit.Clubs, Rank.Eight),
                    new Card(Suit.Clubs, Rank.Ten),
                    new Card(Suit.Clubs, Rank.Three),
                    new Card(Suit.Clubs, Rank.Nine),
                    new Card(Suit.Clubs, Rank.Ace),
                },
                new Card[] {
                    new Card(Suit.Diamonds, Rank.Eight),
                    new Card(Suit.Diamonds, Rank.Ten),
                    new Card(Suit.Diamonds, Rank.Three),
                    new Card(Suit.Diamonds, Rank.Nine),
                    new Card(Suit.Diamonds, Rank.Ace),
                }
            };

            _model.PlayCustomHands(hands);

            //No winner
            Assert.IsTrue(!_model.Players.Any(p => p.IsWinner));


        }

    }
}