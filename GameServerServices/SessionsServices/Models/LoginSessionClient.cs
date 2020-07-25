using GameServerServices.DataBase.Models;

namespace GameServerServices.SessionsServices.Models
{
    public class LoginSessionClient
    {
        public User User { get; set; }
        public string SessionToken { get; set; }
    }
}