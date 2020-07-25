namespace ClientServerModels.GameSessionModels.PlayerModels
{
    public class MovingRightState:PlayerState
    {
        public override void Move()
        {
            Player.GlobalX += Player.Speed;
        }
    }
}