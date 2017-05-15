using BlackJack_21;
using System;
using System.Linq;
using Xunit;

namespace TestsForBlackJack
{
    public class UnitTest1
    {
        [Fact]
        public void AceOfSpades()
        {
            var target = new Card(Rank.Ace, Suit.Spades);

            string result = target.ToString();

            Assert.Equal("Ace of Spades", result);
        }
        [Fact]
        public void TwoOfClub()
        {
            var target = new Card(Rank.Two, Suit.Club);

            string result = target.ToString();

            Assert.Equal("Two of Club", result);
        }
        [Fact]
        public void CountFullDeckOfCards()
        {
            var target = new Deck();

            int result = target.cards.Count;

            Assert.Equal(52, result);
            Assert.Equal("Ace of Club", target.cards.First().ToString());
            Assert.Equal("Two of Club", target.cards[1].ToString());
        }

        [Fact]
        public void DeckToString()
        {
            var target = new Deck();

            string result = target.ToString();

            Assert.True(result.StartsWith("Ace of Club\nTwo of Club\nThree of Club"));
            Assert.Equal("Ace of Club", target.cards.First().ToString());
        }
        [Fact]
        public void DeckShuffleTest()
        {
            var deck = new Deck();
            var result = deck.ToString();
            deck.Shuffle();
            var resul2 = deck.ToString();

            Assert.NotEqual(result, resul2);
        }
    }
}
