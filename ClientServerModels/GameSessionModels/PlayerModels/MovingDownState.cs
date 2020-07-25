namespace ClientServerModels.GameSessionModels.PlayerModels
{
    public class MovingDownState:PlayerState
    {
        public override void Move()
        {
            Player.GlobalY += Player.Speed;
        }
    }
}