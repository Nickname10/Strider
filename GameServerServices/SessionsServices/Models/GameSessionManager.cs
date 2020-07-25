using ClientServerModels;

namespace GameServerServices.SessionsServices.Models
{
    public static class GameSessionManager
    {
        private static int _maxGameSessions = 100; // default 100 sessions are available to create
        // key session id was created for fast search access to map and use in GameSessionsService
        public static GameMap[] GameMaps { get; private set; } = new GameMap[_maxGameSessions]; 
        // need for fast refresh client info 
        public static GameSession[] GameSessions { get; private set; } = new GameSession[_maxGameSessions];
        private static int _lastGameSessionId; // need for generate id session
        public static void AddSession(GameSession gameSession)
        {
            gameSession.Id = _lastGameSessionId++;
            if (gameSession.Id > 99)
            {
                IncreaseMaxGameSessionsCount();
            }
            GameSessions[gameSession.Id] = gameSession;
            GameMaps[gameSession.Id] = new GameMap(gameSession);
            
        }

        private static void IncreaseMaxGameSessionsCount()
        {
            _maxGameSessions += 100;
            var resizedGameSessions = new GameSession[_maxGameSessions];
            var resizedGameMaps = new GameMap[_maxGameSessions];
            GameSessions.CopyTo(resizedGameSessions, 0);
            GameMaps.CopyTo(resizedGameMaps,0);
            GameSessions = resizedGameSessions;
            GameMaps = resizedGameMaps;
        }
      
       
    }
}