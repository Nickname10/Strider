using System;
using System.Linq;
using System.ServiceModel;
using ClientServerModels;
using GameServerServices.DataBase;
using GameServerServices.SessionsServices.Models;
using HashCalculator;
using Character = ClientServerModels.Character;

namespace GameServerServices.SessionsServices.LoginSessionService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class LoginSession : ILoginSession
    {
        private readonly Random _random = new Random();

        public LoggedClient Connect(string email, string password)
        {
            try
            {
                var workUnit = WorkUnit.Instance;
                var currentUser = workUnit.Users.Get().FirstOrDefault(t => t.Email == email && t.PasswordHash == password);
                if (currentUser == null)
                {
                    return null;
                }

                var sessionToken = Calculator.CalculateMd5(currentUser.Nickname + _random.Next(int.MaxValue));
                ClientTable.Instance.AddLoginSessionClient(
                    new LoginSessionClient()
                    {
                        User = currentUser,
                        SessionToken = sessionToken
                    });
                return new LoggedClient()
                {
                    SessionToken = sessionToken,
                    IsAdmin = currentUser.IsAdmin,
                    Nickname = currentUser.Nickname,
                    Points = currentUser.Points,
                };
            }
            catch
            {
                return null;
            }
        }

        public bool CreateGameSession(GameSession gameSession, string sessionToken)
        {
            ;
            var client = ClientTable.Instance.SearchClient(sessionToken);
            if (client != null)
            {
                if (client.User.IsAdmin && gameSession.Validate())
                {
                    GameSessionManager.AddSession(gameSession);
                    return true;
                }

                return false;
            }

            return false;
        }

        public bool DeleteGameSession(int gameSessionId, string sessionToken)
        {
            try
            {
                var client = ClientTable.Instance.SearchClient(sessionToken);
                if (client != null && client.User.IsAdmin)
                {
                    GameSessionManager.GameSessions[gameSessionId] = null;
                    GameSessionManager.GameMaps[gameSessionId].StopTicks();
                    GameSessionManager.GameMaps[gameSessionId] = null;
                    return true;
                }
                return false; 
            }
            catch
            {
                return false;
            }

        }

        public bool CreateCharacter(ClientServerModels.Character character, string sessionToken)
        {
            try
            {
                var client = ClientTable.Instance.SearchClient(sessionToken);
                WorkUnit.Instance.Characters.Add(new DataBase.Models.Character(character, client.User));
                WorkUnit.Instance.SaveAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCharacter(Character character, string sessionToken)
        {
            try
            {
                var client = ClientTable.Instance.SearchClient(sessionToken);
                var deleteCharacter = client.User.Characters.FirstOrDefault(t =>
                    t.Name == character.Name);
                if (deleteCharacter != null)
                {
                    WorkUnit.Instance.Characters.Remove(deleteCharacter.Id);
                }
                else return false;

                WorkUnit.Instance.SaveAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }


        public GameSession[] RefreshSessionList()
        {
            return GameSessionManager.GameSessions;
        }

        public void Disconnect(string sessionToken)
        {
            ClientTable.Instance.RemoveLoginSessionClient(sessionToken);
        }

        public Character[] GetCharacters(string sessionToken)
        {
            var client = ClientTable.Instance.SearchClient(sessionToken);
            var dbCharacters = client.User.Characters;
            var characters = new Character[dbCharacters.Count];
            var i = 0;
            foreach (var dbCharacter in dbCharacters)
            {
                characters[i++] = new Character()
                {
                    Name = dbCharacter.Name,
                    MainRedColor = dbCharacter.MainRedColor,
                    MainGreenColor = dbCharacter.MainGreenColor,
                    MainBlueColor = dbCharacter.MainBlueColor,
                    StrokeRedColor = dbCharacter.StrokeRedColor,
                    StrokeGreenColor = dbCharacter.StrokeGreenColor,
                    StrokeBlueColor = dbCharacter.StrokeBlueColor,
                    StrokeLength = dbCharacter.StrokeLength,
                    StrokeSpace = dbCharacter.StrokeSpace
                };
            }

            return characters;
        }
        
        public bool TryConnectToGameSession(int gameSessionId)
        {
            try
            {
                if (GameSessionManager.GameSessions[gameSessionId].NumberPlayers <
                    GameSessionManager.GameSessions[gameSessionId].MaxPlayers)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }
    }
}