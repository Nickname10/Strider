
namespace ClientServerModels.GameSessionModels.PlayerModels
{
    public abstract class PlayerState
    {
        protected Player Player;

        public Player Context
        {
            set => Player = value;
        }

        public abstract void Move();
    }
}