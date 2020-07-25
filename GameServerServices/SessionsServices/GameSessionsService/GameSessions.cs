using System;
using System.Linq;
using System.ServiceModel;
using System.Text;
using ClientServerModels;
using ClientServerModels.GameSessionModels.PlayerModels;
using GameServerServices.DataBase;
using GameServerServices.DataBase.Models;
using GameServerServices.SessionsServices.Models;
using Character = ClientServerModels.Character;

namespace GameServerServices.SessionsServices.GameSessionsService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class GameSessions : IGameSessions
    {
        public Player Connect(int gameSessionId, Character character, LoggedClient loggedClient)
        {
            try
            {
                // if client isn't in client table we throw the exception
                if (ClientTable.Instance.SearchClient(loggedClient.SessionToken) == null) throw new Exception();
                return GameSessionManager.GameMaps[gameSessionId]
                    .GeneratePlayer(loggedClient, character, OperationContext.Current);
            }
            catch
            {
                return null;
            }
        }

        public void Disconnect(string sessionToken)
        {
            foreach (var gameMap in GameSessionManager.GameMaps.Where(t => t != null))
            {
                gameMap.DeletePlayer(sessionToken);
            }
        }


        public void SendMessage(string sessionToken, int gameSessionId, string message)
        {
            var user = ClientTable.Instance.SearchClient(sessionToken).User;
            var dateTime = DateTime.Now;

            var answer = new StringBuilder();
            answer.Append(dateTime.ToShortTimeString());
            answer.Append(" ");
            answer.Append(user.Nickname);
            answer.Append(": ");
            answer.Append(message);

            foreach (var i in GameSessionManager.GameMaps[gameSessionId].GameSessionClients)
            {
                i?.OperationContext.GetCallbackChannel<IGameSessionCallbacks>().MessageCallback(answer.ToString());
            }
            user.Messages.Add(new Message()
            {
                DateTime = dateTime,
                Text = message
            });
            WorkUnit.Instance.Users.Update(user.Id, user);
            WorkUnit.Instance.SaveAsync();
        }

        public void ChangeStateToMovingUp(int gameSessionId, int playerId)
        {
            try
            {
                lock (GameSessionManager.GameMaps[gameSessionId].SyncRootToPlayers)
                {
                    GameSessionManager.GameMaps[gameSessionId].GameSessionClients[playerId].Player.SetMovingUpState();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public void ChangeStateToMovingLeft(int gameSessionId, int playerId)
        {
            try
            {
                lock (GameSessionManager.GameMaps[gameSessionId].SyncRootToPlayers)
                {
                    GameSessionManager.GameMaps[gameSessionId].GameSessionClients[playerId].Player.SetMovingLeftState();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public void ChangeStateToMovingRight(int gameSessionId, int playerId)
        {
            try
            {
                lock (GameSessionManager.GameMaps[gameSessionId].SyncRootToPlayers)
                {
                    GameSessionManager.GameMaps[gameSessionId].GameSessionClients[playerId].Player
                        .SetMovingRightState();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public void ChangeStateToMovingDown(int gameSessionId, int playerId)
        {
            try
            {
                lock (GameSessionManager.GameMaps[gameSessionId].SyncRootToPlayers)
                {
                    GameSessionManager.GameMaps[gameSessionId].GameSessionClients[playerId].Player.SetMovingDownState();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}