using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack_21
{
    public class Card
    {
        public Card()
        {
        }

        public Card(Rank rank, Suit suite)
        {
            this.Rank = rank;
            this.Suit = suite;
            this.IsFaceUp = true;
            this.NameOfCards = "";
            this.Value = 0;
        }
        public Rank Rank { get; set; }

        public Suit Suit { get; set; }
        public int Value { get; set; }
        public bool IsFaceUp { get; set; }
        public string NameOfCards { get; set; }

        public void Flip()
        {
            this.IsFaceUp = !this.IsFaceUp;
        }
        public override string ToString()
        {
            string suite = "";
            string rank = "";
            switch(this.Suit)
            {
                case Suit.Club:
                    suite = "Club";
                    break;
                case Suit.Diamond:
                    suite = "Diamond";
                    break;
                case Suit.Heart:
                    suite = "Heart";
                    break;
                case Suit.Spades:
                    suite = "Spades";
                    break;
            }
            switch(this.Rank)
            {
                case Rank.Ace:
                    rank = "Ace";
                    break;
                case Rank.King:
                    rank = "King";
                    break;
                case Rank.Queen:
                    rank = "Queen";
                    break;
                case Rank.Jack:
                    rank = "Jack";
                    break;
                case Rank.Ten:
                    rank = "Ten";
                    break;
                case Rank.Nine:
                    rank = "Nine";
                    break;
                case Rank.Eight:
                    rank = "Eight";
                    break;
                case Rank.Seven:
                    rank = "Seven";
                    break;
                case Rank.Six:
                    rank = "Six";
                    break;
                case Rank.Five:
                    rank = "Five";
                    break;
                case Rank.Four:
                    rank = "Four";
                    break;
                case Rank.Three:
                    rank = "Three";
                    break;
                case Rank.Two:
                    rank = "Two";
                    break;
            }
            NameOfCards = rank + " of " + suite;
            return NameOfCards;
        }
    }
}
