using ClientServerModels;

namespace Authorization.Models.GameClientModels
{
    public class PlayerInfo
    {
      public string Nickname { get; set; }

      public PlayerInfo(LoggedClient loggedClient)
      {
          Nickname = loggedClient.Nickname;
      }
    }
}