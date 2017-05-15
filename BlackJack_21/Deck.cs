using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace BlackJack_21
{
    public class Deck
    {
        public List<Card> cards = new List<Card>();
       
        public Deck()

        {
            this.Initialize();
        }
        public ReadOnlyCollection<Card> Cards
        {
            get { return this.cards.AsReadOnly(); }
        }
        public Card DrawACard()
        {
            if(cards.Count<0)
            {
                this.Initialize();
                this.Shuffle();
            }
            Card cardToreturn = cards[cards.Count - 1];
            cards.RemoveAt(cards.Count - 10);
            return cardToreturn;
        }

        internal void GiveAdditionalCard(object hand)
        {
            throw new NotImplementedException();
        }
        public void Initialize()
        {
            this.cards.Clear();
            this.cards.AddRange(
                Enumerable.Range(1, 4)
                    .SelectMany(s => Enumerable.Range(1, 13).Select(n => new Card((Rank)n, (Suit)s))));
        }
        public int GetAmountOfRemainingCrads()
        {
            return cards.Count;
        }
        public void Shuffle()
        {
            Random random = new Random();
            int n = cards.Count();
            while(n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Card card = cards[k];
                cards[k] = cards[n];
                cards[n] = card;
            }
        }
        public override string ToString()
        {
            string value = "";
            foreach (Card card in cards)
             {
                // Console.WriteLine("Card {0}:{1} of {2}",card.NameOfCards,card.Rank,card.Suit);
                var r = card.Rank.ToString();
                var s = card.Suit.ToString();
                value += r + " of " + s + "\n";
             }
            return value;
        }
        public void Deal(Score score)
        {
            if (this.cards.Count < 2)
            {
                throw new InvalidOperationException();
            }

            var card = this.cards.First();
            score.AddCard(card);
            this.cards.Remove(card);

            card = this.cards.First();

            if (score.IsDealer)
            {
                card.Flip();
            }

            score.AddCard(card);
            this.cards.Remove(card);
        }
        public void GiveAdditionalCard(Score score)
        {
            if (this.cards.Count < 1)
            { 
                throw new InvalidOperationException();
            }

            score.AddCard(this.cards.First());
            this.cards.RemoveAt(0);
        }
    }
}
