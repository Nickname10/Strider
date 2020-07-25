using System.Runtime.Serialization;
using ClientServerModels.GameSessionModels.PlayerModels;

namespace ClientServerModels.GameSessionModels
{
    [DataContract]
    public class PlayerScoreItem
    {
        [DataMember] public string Nickname { get; set; }
        [DataMember] public double Score { get; set; }
    }
}