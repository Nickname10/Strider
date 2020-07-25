using System.ServiceModel;
using ClientServerModels;
using ClientServerModels.GameSessionModels.PlayerModels;

namespace GameServerServices.SessionsServices.GameSessionsService
{

    [ServiceContract(CallbackContract = typeof(IGameSessionCallbacks))]
    public interface IGameSessions
    {
        [OperationContract]
        Player Connect(int gameSessionId, Character character, LoggedClient loggedClient);

        [OperationContract(IsOneWay = true)]
        void Disconnect(string sessionToken);

        [OperationContract(IsOneWay = true)]
        void SendMessage(string sessionToken, int gameSessionId, string message);

        [OperationContract(IsOneWay = true)]
        void ChangeStateToMovingUp(int gameSessionId, int playerId);

        [OperationContract(IsOneWay = true)]
        void ChangeStateToMovingLeft(int gameSessionId, int playerId);

        [OperationContract(IsOneWay = true)]
        void ChangeStateToMovingRight(int gameSessionId, int playerId);

        [OperationContract(IsOneWay = true)]
        void ChangeStateToMovingDown(int gameSessionId, int playerId);
    }
}