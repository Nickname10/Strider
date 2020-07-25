using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using ClientServerModels;
using ClientServerModels.GameSessionModels;
using ClientServerModels.GameSessionModels.PlayerModels;
using GameServerServices.SessionsServices.GameSessionsService;

namespace GameServerServices.SessionsServices.Models
{
    public class GameMap
    {
        private readonly Random _random = new Random();
        private readonly int _maxGlobalX;
        private readonly int _maxGlobalY;
        private readonly GameSession _currentSession;
        public Food[] Foods { get; }
        public GameSessionClient[] GameSessionClients { get; }
        public Player[] Players { get; }
        public object SyncRootToPlayers { get; } = new object();
        private readonly int _tickTimeout;
        private readonly CancellationTokenSource _cancellationMapTick;

        public GameMap(GameSession gameSession)
        {
            GameSessionClients = new GameSessionClient[gameSession.MaxPlayers];
            Players = new Player[gameSession.MaxPlayers];
            _currentSession = gameSession;
            switch (gameSession.Map)
            {
                case GameSession.MapSize.Small:
                    _tickTimeout = 30;
                    Foods = new Food[80];
                    _maxGlobalX = 2000;
                    _maxGlobalY = 2000;
                    for (var i = 0; i < 80; i++)
                    {
                        Foods[i] = GenerateFood(i);
                    }

                    break;
                case GameSession.MapSize.Medium:
                    _tickTimeout = 25;
                    Foods = new Food[200];
                    _maxGlobalX = 3000;
                    _maxGlobalY = 3000;
                    for (var i = 0; i < 200; i++)
                    {
                        Foods[i] = GenerateFood(i);
                    }

                    break;
                case GameSession.MapSize.High:
                    _tickTimeout = 20;
                    Foods = new Food[300];
                    _maxGlobalX = 4000;
                    _maxGlobalY = 4000;
                    for (var i = 0; i < 300; i++)
                    {
                        Foods[i] = GenerateFood(i);
                    }

                    break;
                case GameSession.MapSize.VeryHigh:
                    _tickTimeout = 15;
                    Foods = new Food[500];
                    _maxGlobalX = 5000;
                    _maxGlobalY = 5000;
                    for (var i = 0; i < 500; i++)
                    {
                        Foods[i] = GenerateFood(i);
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameSession), gameSession, null);
            }

            _cancellationMapTick = new CancellationTokenSource();
            var mapRender = new Task(() => MapTick(_cancellationMapTick.Token), _cancellationMapTick.Token);
            mapRender.Start();
        }

        public Player GeneratePlayer(LoggedClient loggedClient, Character character, OperationContext operationContext)
        {
            try
            {
                var freeInd = int.MaxValue;
                for (var i = 0; i < GameSessionClients.Length; i++)
                {
                    if (GameSessionClients[i] != null) continue;
                    freeInd = i;
                    break;
                }

                lock (SyncRootToPlayers)
                {
                    var player = new Player(freeInd, _random.Next(_maxGlobalX), _random.Next(_maxGlobalY),
                        6, 4.5, character);
                    Players[freeInd] = player;
                    GameSessionClients[freeInd] = new GameSessionClient()
                    {
                        Player = player,
                        OperationContext = operationContext,
                        LoggedClient = loggedClient
                    };
                    GameSessionManager.GameSessions[_currentSession.Id].NumberPlayers++;
                    return player;
                }
            }
            catch
            {
                return null;
            }
        }

        private Food GenerateFood(int ind)
        {
            return new Food()
            {
                Id = ind,
                MainBlueColor = (byte) _random.Next(255),
                MainRedColor = (byte) _random.Next(255),
                MainGreenColor = (byte) _random.Next(255),
                StrokeRedColor = 0,
                StrokeBlueColor = 0,
                StrokeGreenColor = 0,
                StrokeLength = 1,
                StrokeSpace = 0,
                GlobalX = _random.Next(_maxGlobalX),
                GlobalY = _random.Next(_maxGlobalY),
                StrokeThickness = 0,
                Radius = 4
            };
        }

        public void DeletePlayer(string sessionToken)
        {
            lock (SyncRootToPlayers)
            {
                for (var i = 0; i < GameSessionClients.Length; i++)
                {
                    if (GameSessionClients[i]?.LoggedClient.SessionToken == sessionToken)
                    {
                        GameSessionClients[i].Player = null; //equals Players[GameSessionClients[i].Player.Id] = null;
                        GameSessionClients[i] = null;
                        GameSessionManager.GameSessions[_currentSession.Id].NumberPlayers--;
                        break;
                    }
                }
            }
        }

        public void StopTicks()
        {
            _cancellationMapTick.Cancel();
        }

        private void MapTick(CancellationToken token)
        {
            try
            {
                var playerScoreItems = new List<PlayerScoreItem>();
                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        foreach (var i in GameSessionClients)
                            i?.OperationContext
                                .GetCallbackChannel<IGameSessionCallbacks>().DestroyCallback();
                        return;
                    }

                    Thread.Sleep(_tickTimeout);
                    lock (SyncRootToPlayers)
                    {
                        // create score table
                        playerScoreItems.Clear();
                        playerScoreItems.AddRange(
                            from i in GameSessionClients
                            where i != null
                            select new PlayerScoreItem()
                                {Nickname = i.LoggedClient.Nickname, Score = i.Player.Radius * 10});

                        for (var i = 0; i < GameSessionClients.Length; i++)
                        {
                            if (GameSessionClients[i] == null) continue;
                            var player = GameSessionClients[i].Player;
                            player.Move();

                            //check bounds
                            if (player.GlobalX > _maxGlobalX)
                            {
                                player.GlobalX = _maxGlobalX;
                            }

                            if (player.GlobalY > _maxGlobalY)
                            {
                                player.GlobalY = _maxGlobalY;
                            }

                            if (player.GlobalY < 0)
                            {
                                player.GlobalY = 0;
                            }

                            if (player.GlobalX < 0)
                            {
                                player.GlobalX = 0;
                            }

                            // check collision
                            var visibleFoods = Foods.Where(food =>
                                {
                                    if (food == null) return false;
                                    return (player.GlobalX - 900 < food.GlobalX && food.GlobalX < player.GlobalX + 900)
                                           &&
                                           (player.GlobalY - 600 < food.GlobalY && food.GlobalY < player.GlobalY + 600);
                                }
                            ).ToArray();
                            var visiblePlayers = Players.Where(p =>
                            {
                                if (p == null) return false;
                                return (player.GlobalX - 900 < p.GlobalX && p.GlobalX < player.GlobalX + 900)
                                       &&
                                       (player.GlobalY - 600 < p.GlobalY && p.GlobalY < player.GlobalY + 600);
                            }).ToArray();
                            for (var j = 0; j < visibleFoods.Length; j++)
                            {
                                if (visibleFoods[j] == null) continue;
                                if (player.GlobalX - player.Radius < visibleFoods[j].GlobalX
                                    && visibleFoods[j].GlobalX < player.GlobalX + player.Radius
                                    && player.GlobalY - player.Radius < visibleFoods[j].GlobalY
                                    && visibleFoods[j].GlobalY < player.GlobalY + player.Radius)
                                {
                                    player.Grove(visibleFoods[j]);
                                    Foods[visibleFoods[j].Id] = null;
                                    visibleFoods[j] = null;
                                }
                            }

                            for (var j = 0; j < visiblePlayers.Length; j++)
                            {
                                if (visiblePlayers[j] == null) continue;
                                if (player.GlobalX - player.Radius < visiblePlayers[j].GlobalX
                                    && visiblePlayers[j].GlobalX < player.GlobalX + player.Radius
                                    && player.GlobalY - player.Radius < visiblePlayers[j].GlobalY
                                    && visiblePlayers[j].GlobalY < player.GlobalY + player.Radius &&
                                    visiblePlayers[j].Id != player.Id && player.Radius > visiblePlayers[j].Radius)
                                {
                                    //send destroy message and delete on session
                                    var deletePlayerId = visiblePlayers[j].Id;
                                    Players[deletePlayerId] = null;
                                    player.Grove(visiblePlayers[j]);
                                    visiblePlayers[j] = null;
                                    GameSessionClients[deletePlayerId].OperationContext
                                        .GetCallbackChannel<IGameSessionCallbacks>().DestroyCallback();
                                    GameSessionClients[deletePlayerId] = null;
                                    GameSessionManager.GameSessions[_currentSession.Id].NumberPlayers--;
                                }
                            }

                            //client render
                            try
                            {
                                GameSessionClients[i].OperationContext.GetCallbackChannel<IGameSessionCallbacks>()
                                    .RefreshCoordinatesCallback(visibleFoods,
                                        visiblePlayers.Where(t => t != null).ToArray(),
                                        playerScoreItems.ToArray());
                            }
                            catch
                            {
                                GameSessionClients[i].Player = null;
                                GameSessionClients[i] = null;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}