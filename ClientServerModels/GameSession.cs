using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ClientServerModels
{
    [DataContract]
    public class GameSession
    {
        [DataMember] public int Id { get; set; }
        [Required] [DataMember] public string Name { get; set; }
        [Required] [DataMember] public MapSize Map { get; set; }
        [Required] [DataMember] public int MaxPlayers { get; set; }
        [DataMember] public int NumberPlayers { get; set; }
        [DataMember] public string MapSizeAsString { get; set; }
        [DataMember] public string Description { get; set; }

        public enum MapSize
        {
            Small,
            Medium,
            High,
            VeryHigh
        }

        public GameSession()
        {
        }

        public GameSession(string name, string mapSize, string maxPlayers, string description)
        {
            Name = name;
            MaxPlayers = int.Parse(maxPlayers);
            NumberPlayers = 0;
            MapSizeAsString = mapSize;
            Description = description;
            switch (mapSize)
            {
                case "Small":
                    Map = GameSession.MapSize.Small;
                    break;
                case "Medium":
                    Map = GameSession.MapSize.Medium;
                    break;
                case "High":
                    Map = GameSession.MapSize.High;
                    break;
                case "Very High":
                    Map = GameSession.MapSize.VeryHigh;
                    break;
                default:
                    Map = GameSession.MapSize.Small;
                    break;
            }
        }

        public bool Validate()
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(this);
            if (!Validator.TryValidateObject(this, context, results, true))
            {
                foreach (var error in results)
                {
                    return false;
                }

                return true;
            }
            else return true;
        }
    }
}