using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack_21
{
    class Bank : PlayerBase
    {
        public Bank()
        {
            this.Score = new Score(isDealer: true);
        }
    }
}
