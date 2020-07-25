using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Shapes;
using Authorization.GameSessionsService;
using Authorization.Models.GameSessionModels;
using ClientServerModels;
using ClientServerModels.GameSessionModels;
using ClientServerModels.GameSessionModels.PlayerModels;
using MessageBox = Xceed.Wpf.Toolkit.MessageBox;


namespace Authorization.ViewModels.SessionClient
{
    public class SessionViewModel : FrameWindowViewModel, IGameSessionsCallback
    {
        public int CanvasHeight { get; } = 600;
        public int CanvasWidth { get; } = 800;


        public RelayCommand KeyWCommand { get; }
        public RelayCommand KeyACommand { get; }
        public RelayCommand KeySCommand { get; }
        public RelayCommand KeyDCommand { get; }
        public RelayCommand KeyEnterCommand { get; }
        public RelayCommand SendMessageCommand { get; }

        public ObservableCollection<string> ChatMessages { get; }
        public ObservableCollection<Ellipse> GameObjectsView { get; }
        public ObservableCollection<PlayerScore> ScoreTable { get; }

        private readonly GameSessionsClient _gameSessionNetworkClient;
        private readonly Window _sessionWindow;
        private readonly Window _parentWindow;
        private readonly LoggedClient _loggedClient;
        private readonly Camera _camera;
        private readonly GameSession _gameSession;
        private Player _player;


        private PlayerState _playerLastState;
        public string Message { get; set; }
        private readonly object _syncRootToGameResources = new object();
        private readonly object _syncRootToAction = new object();

        // this enumeration is only necessary in order not to re-send packets to the server
        private enum PlayerState
        {
            MovingLeft, MovingRight, MovingDown, MovingUp
        }

        public SessionViewModel(GameSession selectedSession,
            Character selectedCharacter,
            Window parentWindow,
            Window currentWindow,
            LoggedClient loggedClient)
        {
            _gameSession = selectedSession;
            _sessionWindow = currentWindow;
            _parentWindow = parentWindow;
            _loggedClient = loggedClient;
            _gameSessionNetworkClient = new GameSessionsClient(new System.ServiceModel.InstanceContext(this));
            _player = _gameSessionNetworkClient.Connect(_gameSession.Id, selectedCharacter, _loggedClient);
            // default moving up state convention
            _playerLastState = PlayerState.MovingUp;
            _camera = new Camera(_player, CanvasWidth, CanvasHeight);

            ChatMessages = new ObservableCollection<string>();
            ChatMessages.CollectionChanged += (sender, e) =>
                OnPropertyChanged(nameof(ChatMessages));

            GameObjectsView = new ObservableCollection<Ellipse>();
            ScoreTable = new ObservableCollection<PlayerScore>();

            KeyWCommand = new RelayCommand(KeyWAction);
            KeyACommand = new RelayCommand(KeyAAction);
            KeySCommand = new RelayCommand(KeySAction);
            KeyDCommand = new RelayCommand(KeyDAction);
            KeyEnterCommand = new RelayCommand(KeyEnterAction);
            SendMessageCommand = new RelayCommand(SendMessageAction);
            
        }

        private void RefreshView(Player[] players, Food[] foods, PlayerScoreItem[] playerScoreItems)
        {
            GameObjectsView.Clear();
            ScoreTable.Clear();
            lock (_syncRootToGameResources)
            {
                playerScoreItems = playerScoreItems.OrderByDescending(t => t.Score).ToArray();
                for (var i = 0; i < playerScoreItems.Length; i++)
                {
                    ScoreTable.Add(new PlayerScore(playerScoreItems[i], i + 1));
                }

                _player = players.FirstOrDefault(t => t.Id == _player.Id);
                _camera.ChangeCoordinates(_player);
                foreach (var i in players)
                {
                    if (i != null)
                        GameObjectsView.Add(new GameObjectModel(i, _camera).View);
                }

                foreach (var i in foods)
                {
                    if (i != null)
                        GameObjectsView.Add(new GameObjectModel(i, _camera).View);
                }
            }

            OnPropertyChanged(nameof(GameObjectsView));
            OnPropertyChanged(nameof(ScoreTable));
        }

        private void KeyWAction(object obj)
        {
            lock (_syncRootToAction)
            {
                if (_playerLastState != PlayerState.MovingUp)
                {
                    _playerLastState = PlayerState.MovingUp;
                    _gameSessionNetworkClient.ChangeStateToMovingUp(_gameSession.Id, _player.Id);
                }
            }
        }

        private void KeyAAction(object obj)
        {
            lock (_syncRootToAction)
            {
                if (_playerLastState != PlayerState.MovingLeft)
                {
                    _playerLastState = PlayerState.MovingLeft;
                    _gameSessionNetworkClient.ChangeStateToMovingLeft(_gameSession.Id, _player.Id);
                }
            }
        }

        private void KeySAction(object obj)
        {
            lock (_syncRootToAction)
            {
                if (_playerLastState != PlayerState.MovingDown)
                {
                    _playerLastState = PlayerState.MovingDown;
                    _gameSessionNetworkClient.ChangeStateToMovingDown(_gameSession.Id, _player.Id);
                }
            }
        }

        private void KeyDAction(object obj)
        {
            lock (_syncRootToAction)
            {
                if (_playerLastState != PlayerState.MovingRight)
                {
                    _playerLastState = PlayerState.MovingRight;
                    _gameSessionNetworkClient.ChangeStateToMovingRight(_gameSession.Id, _player.Id);
                }
            }
        }

        private void KeyEnterAction(object obj)
        {
            SendMessageAction(null);
        }

        private void SendMessageAction(object obj)
        {
            if (!string.IsNullOrEmpty(Message))
                _gameSessionNetworkClient.SendMessage(_loggedClient.SessionToken, _gameSession.Id, Message);
            Message = string.Empty;
            OnPropertyChanged(nameof(Message));
        }

        public void MessageCallback(string message)
        {
            ChatMessages.Add(message);
        }

        public void RefreshCoordinatesCallback(Food[] foods, Player[] players, PlayerScoreItem[] playerScoreItems)
        {
            RefreshView(players, foods, playerScoreItems);
        }

        public void DestroyCallback()
        {
            _sessionWindow.Close();
            MessageBox.Show("You were destroyed!",
                "ooops!",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        public override void OnWindowClosing(object sender, CancelEventArgs e)
        {
            _gameSessionNetworkClient.Disconnect(_loggedClient.SessionToken);
            _parentWindow.Show();
        }
    }
}