namespace BlackJack_21
{
    public abstract class PlayerBase
    {
        protected PlayerBase()
        {
        }

        public Score Score{ get; protected set; }
    }
}