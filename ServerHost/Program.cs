using System;
using System.ServiceModel;
using GameServerServices.RegistrationService;
using GameServerServices.SessionsServices.GameSessionsService;
using GameServerServices.SessionsServices.LoginSessionService;

namespace ServerHost
{
    class Program
    {
        static void Main()
        {
            using (var registerServiceHost = new ServiceHost(typeof(Registration)))
            using (var loginSessionServiceHost  = new ServiceHost(typeof(LoginSession)))
            using (var gameSessionServiceHost = new ServiceHost(typeof(GameSessions)))
            {
                registerServiceHost.Open();
                loginSessionServiceHost.Open();
                gameSessionServiceHost.Open();
                
                Console.WriteLine("Game server was started");
                Console.ReadLine();
            }
        }
    }
}