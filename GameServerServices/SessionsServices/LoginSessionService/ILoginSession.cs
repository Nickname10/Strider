using System.ServiceModel;
using ClientServerModels;


namespace GameServerServices.SessionsServices.LoginSessionService
{
    [ServiceContract]
    public interface ILoginSession
    {
        [OperationContract]
        LoggedClient Connect(string email, string password);

        [OperationContract]
        bool CreateGameSession(GameSession gameSession, string sessionToken);

        [OperationContract]
        bool DeleteGameSession(int gameSessionId, string sessionToken);

        [OperationContract]
        bool CreateCharacter(ClientServerModels.Character character, string sessionToken);

        [OperationContract]
        bool DeleteCharacter(ClientServerModels.Character character, string sessionToken);

        [OperationContract]
        GameSession[] RefreshSessionList();

        [OperationContract]
        void Disconnect(string sessionToken);

        [OperationContract]
        Character[] GetCharacters(string sessionToken);

        //this method in this interface because we trying to connect in Main menu and connect in next window
        [OperationContract]
        bool TryConnectToGameSession(int gameSessionId);
    }
}