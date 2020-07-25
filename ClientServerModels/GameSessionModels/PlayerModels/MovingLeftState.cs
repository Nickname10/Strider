namespace ClientServerModels.GameSessionModels.PlayerModels
{
    public class MovingLeftState:PlayerState
    {
        public override void Move()
        {
            Player.GlobalX -= Player.Speed;
        }
    }
}