namespace ClientServerModels.GameSessionModels.PlayerModels
{
    public class MovingUpState:PlayerState
    {
        public override void Move()
        {
            Player.GlobalY -= Player.Speed;
        }
    }
}