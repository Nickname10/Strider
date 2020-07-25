using System.ServiceModel;
using ClientServerModels;
using ClientServerModels.GameSessionModels.PlayerModels;

namespace GameServerServices.SessionsServices.Models
{
    public class GameSessionClient
    {
        public LoggedClient LoggedClient { get; set; }
        public OperationContext OperationContext { get; set; }
        public Player Player { get; set; }
    }
}