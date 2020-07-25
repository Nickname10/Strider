using System;
using System.Collections.Generic;
using System.Windows;
using Authorization.LoginSessionService;
using Authorization.Models.GameClientModels;
using ClientServerModels;
using MessageBox = Xceed.Wpf.Toolkit.MessageBox;

namespace Authorization.ViewModels.GameClient
{
    public class CreateSessionViewModel
    {
        public RelayCommand BackCommand { get; }
        public RelayCommand CreateSessionCommand { get; }
        public GameSessionForm GameSessionForm { get; }
        public List<string> AvailableMapSizes { get; }
        public List<string> AvailableCountPlayers { get; }
        private readonly ClientViewModel _frame;
        private readonly LoggedClient _loggedClient;
        private readonly LoginSessionClient _loginSessionNetworkClient;

        public CreateSessionViewModel(ClientViewModel frame, LoggedClient loggedClient,
            LoginSessionClient loginSessionNetworkClient)
        {
            _frame = frame;
            _loggedClient = loggedClient;
            _loginSessionNetworkClient = loginSessionNetworkClient;
            BackCommand = new RelayCommand(BackAction);
            CreateSessionCommand = new RelayCommand(CreateSessionAction);
            AvailableCountPlayers = new List<string>() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"};
            AvailableMapSizes = new List<string>()
            {
                "Small",
                "Medium",
                "High",
                "Very High"
            };
            GameSessionForm = new GameSessionForm()
            {
                MapSize = AvailableMapSizes[1],
                MaxPlayers = AvailableCountPlayers[5],
            };
        }

        private void BackAction(object obj)
        {
            _frame.ShowMenuPage();
        }

        public void CreateSessionAction(object obj)
        {
            try
            {
                if (GameSessionForm.Validate())
                {
                    var gameSession = new GameSession(GameSessionForm.Name,
                        GameSessionForm.MapSize, GameSessionForm.MaxPlayers, GameSessionForm.Description);
                    if (_loginSessionNetworkClient.CreateGameSession(gameSession, _loggedClient.SessionToken))
                    {
                        _frame.ShowMenuPage();
                    }
                    else
                    {
                        throw new Exception("Creation error");
                    }
                }
                else
                {
                    throw new Exception("Wrong form");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}