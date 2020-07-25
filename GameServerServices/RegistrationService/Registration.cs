using GameServerServices.DataBase;
using GameServerServices.DataBase.Models;

namespace GameServerServices.RegistrationService
{
    public class Registration : IRegistration
    {
        public bool Register(string email, string nickname, string password)
        {
            var workUnit = WorkUnit.Instance;
            workUnit.Users.Add(new User()
            {
                Email = email,
                PasswordHash = password,
                Nickname = nickname,
                IsAdmin = false,
                Points = 0
            });

            workUnit.SaveAsync();
            return true;
        }
    }
}