using System.Globalization;

namespace Authorization.Models.GameSessionModels
{
    public class PlayerScore
    {
        public int Position { get; }
        public string Score { get; }
        public string Nickname { get; }

        public PlayerScore(ClientServerModels.GameSessionModels.PlayerScoreItem playerScore, int position)
        {
            Nickname = playerScore.Nickname;
            Position = position;
            Score = playerScore.Score.ToString(CultureInfo.CurrentCulture);
        }
    }
}