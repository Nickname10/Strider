using System.ServiceModel;
using ClientServerModels.GameSessionModels;
using ClientServerModels.GameSessionModels.PlayerModels;

namespace GameServerServices.SessionsServices.GameSessionsService
{
    [ServiceContract]
    public interface IGameSessionCallbacks
    {
        [OperationContract(IsOneWay = true)]
        void MessageCallback(string message);

        [OperationContract(IsOneWay = true)]
        void RefreshCoordinatesCallback(Food[] foods, Player[] players, PlayerScoreItem[] playerScoreItems);

        [OperationContract(IsOneWay = true)]
        void DestroyCallback();
    }
}