namespace Authorization.Models.GameClientModels
{
    public class GameSessionForm:Form
    {
        public string Name { get; set; }
        public string MapSize { get; set; }
        public string MaxPlayers { get; set; }
        public string Description { get; set; }
    }
}