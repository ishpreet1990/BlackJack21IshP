using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack_21
{
    class Player : PlayerBase
    {
        public decimal balance;
        public decimal bet;
        public Player()
        {
            this.Score = new Score(isDealer: false);
        }

        public event EventHandler BalanceChanged;
        public decimal Balance
        {
            get
            {
                return this.balance;
            }

            set
            {
                if (this.balance != value)
                {
                    this.balance = value;

                    if (this.BalanceChanged != null)
                    {
                        this.BalanceChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        public decimal Bet
        {
            get
            {
                return this.bet;
            }

            set
            {
                if (this.bet == value)
                {
                    return;
                }

                if (value > this.balance + this.bet && this.balance > 0)
                {
                    this.bet += this.balance;
                    this.Balance = 0;
                }
                else if (value < 0 && this.bet > 0)
                {
                    var temp = this.bet + this.balance;
                    this.bet = 0;
                    this.Balance = temp;
                }
                else if (value >= 0 && value <= this.balance + this.bet)
                {
                    var temp = this.balance + this.bet;
                    this.bet = value;
                    this.Balance = temp - value;
                }
            }
        }
    }
}
