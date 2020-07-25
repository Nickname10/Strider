using System.Runtime.Serialization;

namespace ClientServerModels
{
    [DataContract]
    public class LoggedClient
    {
        [DataMember] public string SessionToken { get; set; }
        [DataMember] public string Nickname { get; set; }
        [DataMember] public bool IsAdmin { get; set; }
        [DataMember] public int Points { get; set; }

        public LoggedClient()
        {
            SessionToken = null;
            Nickname = null;
            IsAdmin = false;
            Points = -1;
        }

        public LoggedClient(string sessionToken, string nickname, bool isAdmin, int points)
        {
            SessionToken = sessionToken;
            Nickname = nickname;
            IsAdmin = isAdmin;
            Points = points;
        }
    }
}