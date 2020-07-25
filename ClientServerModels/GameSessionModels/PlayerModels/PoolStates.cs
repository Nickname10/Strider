namespace ClientServerModels.GameSessionModels.PlayerModels
{
    public class PoolStates
    {
        public MovingUpState MovingUpState { get; }
        public MovingLeftState MovingLeftState { get; }
        public MovingRightState MovingRightState { get; }
        public MovingDownState MovingDownState { get; }

        public PoolStates(Player player)
        {
            MovingUpState = new MovingUpState() {Context = player};
            MovingLeftState = new MovingLeftState() {Context = player};
            MovingRightState = new MovingRightState() {Context = player};
            MovingDownState = new MovingDownState() {Context = player};
        }
    }
}
