using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack_21
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "♠ Blackjack Game";
            Console.WriteLine("Money : 1000");
            Console.WriteLine("Bet : 1");


            var game = new Game();
            game.Player.BalanceChanged += OnBalanceChanged;
            game.LastStateChanged += OnLastStateChanged;
            game.AllowedActionsChanged += OnAllowedActionsChanged;
            game.Bank.Score.Changed += OnHandChanged;
            game.Player.Score.Changed += OnHandChanged;
            game.Play(balance: 500, bet: 5);
            //var card = new Card(Rank.Ace,Suit.Spades);
            // Console.WriteLine(card.ToString());
            // var deck = new Deck();
            //Console.WriteLine(deck.ToString());
            //deck.Shuffle();
            //Console.WriteLine(deck);

            while (true)
            {
                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.Add:
                    case ConsoleKey.UpArrow:
                        game.Player.Bet += 5;
                        break;
                    case ConsoleKey.Subtract:
                    case ConsoleKey.DownArrow:
                        game.Player.Bet -= 5;
                        break;
                    case ConsoleKey.Enter:
                        if ((game.allowedActions & GameAction.Deal) == GameAction.Deal)
                        {
                            game.Deal();
                        }
                        else
                        {
                            game.Stand();
                        }

                        break;
                    case ConsoleKey.Spacebar:
                        if ((game.allowedActions & GameAction.Deal) == GameAction.Deal)
                        {
                            game.Deal();
                        }
                        else
                        {
                            game.Hit();
                        }

                        break;
                }
            }
        }

        private static void OnBalanceChanged(object sender, EventArgs e)
        {
            var player = (Player)sender;
            string line = string.Format("BET:  ${0} | BALANCE: ${1}", player.Bet, player.Balance).PadRight(45);
            Console.Write(line);
        }
        private static void OnLastStateChanged(object sender, EventArgs e)
        {
            var game = (Game)sender;
            Console.SetCursorPosition(Console.BufferWidth - 30, 1);
            Console.Write((game.laststate == GameState.DealerWon ? "DEALER WON!" : "           ").PadLeft(28));

            Console.SetCursorPosition(Console.BufferWidth - 30, 13);
            Console.Write((game.laststate == GameState.PlayerWon ? "PLAYER WON!" : "           ").PadLeft(28));
        }
        public static void OnAllowedActionsChanged(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            var game = (Game)sender;

            if ((game.allowedActions & GameAction.Hit) == GameAction.Hit)
            {
                sb.Append("HIT (Spacebar)");
            }

            if ((game.allowedActions & GameAction.Stand) == GameAction.Stand)
            {
                sb.Append((sb.Length > 0 ? ", " : string.Empty) + "STAND (Enter)");
            }

            if ((game.allowedActions & GameAction.Deal) == GameAction.Deal)
            {
                sb.Append((sb.Length > 0 ? ", " : string.Empty) + "DEAL (Enter)");
            }

            Console.SetCursorPosition(Console.BufferWidth - 31, 24);
            Console.WriteLine(sb.ToString().PadLeft(29));
        }
        private static void OnHandChanged(object sender, EventArgs e)
        {
            var hand = (Score)sender;
            var offsetTop = hand.IsDealer ? 1 : 13;
            var name = hand.IsDealer ? "DEALER" : "PLAYER";
            var value = hand.IsDealer ? hand.FaceValue : hand.TotalValue;

            Console.SetCursorPosition(2, hand.IsDealer ? 1 : 13);
            Console.Write(string.Format("{0}'s HAND ({1}):", name, value).PadRight(25));
        }
    }
}
