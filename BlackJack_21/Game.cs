using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack_21
{
    class Game
    {
        public GameAction allowedActions;
        public GameState laststate;
        public Deck deck;
        public Game()
        {
            this.Bank = new Bank();
            this.Player = new Player();
            this.laststate = GameState.Unknown;
            this.allowedActions = GameAction.None;
        }
        public Player Player { get; set;}
        public Bank Bank { get; set; }

        public EventHandler LastStateChanged;
        public EventHandler AllowedActionsChanged;

        public GameAction AllowedActions
        {
            get
            {
                return this.allowedActions;
            }

            private set
            {
                if (this.allowedActions != value)
                {
                    this.allowedActions = value;

                    if (this.AllowedActionsChanged != null)
                    {
                        this.AllowedActionsChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        public GameState LastState
        {
            get
            {
                return this.laststate;
            }

            private set
            {
                if (this.laststate != value)
                {
                    this.laststate = value;

                    if (this.LastStateChanged != null)
                    {
                        this.LastStateChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        public void Play(decimal balance, decimal bet)
        {
            this.Player.Balance = balance;
            this.Player.Bet = bet;
            this.AllowedActions = GameAction.Deal;

            if (this.AllowedActionsChanged != null)
            {
                this.AllowedActionsChanged(this, EventArgs.Empty);
            }
        }

        public void Deal()
        {
            if ((this.allowedActions & GameAction.Deal) != GameAction.Deal)
            {
                throw new InvalidOperationException();
            }

            this.laststate = GameState.Unknown;

            if (this.deck == null)
            {
                this.deck = new Deck();
            }
            else
            {
                this.deck.Initialize();
            }

            this.deck.Shuffle();
            this.Bank.Score.Clear();
            this.Player.Score.Clear();

            this.deck.Deal(this.Bank.Score);
            this.deck.Deal(this.Player.Score);

            if (this.Player.Score.SoftValue == 21)
            {
                if (this.Bank.Score.SoftValue == 21)
                {
                    this.laststate = GameState.Draw;
                }
                else
                {
                    this.Player.Balance += this.Player.Bet / 2;
                    this.laststate = GameState.PlayerWon;
                }

                this.Bank.Score.Show();
                this.allowedActions = GameAction.Deal;
            }
            else if (this.Player.Score.TotalValue == 21)
            {
                this.Player.Balance -= this.Player.Bet / 2;
                this.Bank.Score.Show();
                this.laststate = GameState.DealerWon;
                this.allowedActions = GameAction.Deal;
            }
            else
            {
               
                this.allowedActions = GameAction.Hit | GameAction.Stand;
            }
        }
        public void Hit()
        {
            if ((this.allowedActions & GameAction.Hit) != GameAction.Hit)
            {
                throw new InvalidOperationException();
            }

            this.deck.GiveAdditionalCard(this.Player.Score);

            if (this.Player.Score.TotalValue > 21)
            {
                this.Player.Balance -= this.Player.Bet;
                this.Bank.Score.Show();
                this.laststate = GameState.DealerWon;
                this.allowedActions = GameAction.Deal;
            }
        }
        public void Stand()
        {
            if ((this.allowedActions & GameAction.Stand) != GameAction.Stand)
            {
                throw new InvalidOperationException();
            }

            while (this.Bank.Score.SoftValue < 17)
            {
                this.deck.GiveAdditionalCard(this.Bank.Score);
            }

            if (this.Bank.Score.TotalValue > 21 || this.Player.Score.TotalValue > this.Bank.Score.TotalValue)
            {
                this.Player.Balance += this.Player.Bet;
                this.laststate = GameState.PlayerWon;
            }
            else if (this.Bank.Score.TotalValue == this.Player.Score.TotalValue)
            {
                this.laststate = GameState.Draw;
            }
            else
            {
                this.Player.Balance -= this.Player.Bet;
                this.laststate = GameState.DealerWon;
            }

            this.Bank.Score.Show();
            this.allowedActions = GameAction.Deal;
        }

    }
}
