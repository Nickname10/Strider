using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using Authorization.Annotations;
using Authorization.LoginSessionService;
using Authorization.Windows;
using ClientServerModels;
using MessageBox = Xceed.Wpf.Toolkit.MessageBox;


namespace Authorization.ViewModels.GameClient
{
    public class ConnectViewModel : INotifyPropertyChanged
    {
        public RelayCommand RefreshCommand { get; set; }
        public RelayCommand BackCommand { get; set; }
        public RelayCommand ConnectCommand { get; set; }
        public RelayCommand DeleteSessionCommand { get; set; }

        public List<GameSession> GameSessions { get; set; }
        public GameSession SelectedSession { get; set; }

        public Visibility DeleteButtonVisibility { get; }

        private readonly ClientViewModel _frame;
        private readonly Window _parentWindow;
        private readonly LoggedClient _loggedClient;
        private readonly LoginSessionClient _loginSessionNetworkClient;
        private readonly Character _character;

        public ConnectViewModel(ClientViewModel frame, 
            Window parentWindow, 
            LoggedClient loggedClient,
            LoginSessionClient loginSessionNetworkClient,
            Character character)
        {
            _frame = frame;
            _parentWindow = parentWindow;
            _loggedClient = loggedClient;
            _loginSessionNetworkClient = loginSessionNetworkClient;
            _character = character;
            RefreshSessions();
            RefreshCommand = new RelayCommand(obj => RefreshSessions());
            BackCommand = new RelayCommand(BackAction);
            ConnectCommand = new RelayCommand(ConnectAction, o => SelectedSession != null);
            DeleteButtonVisibility = _loggedClient.IsAdmin ? Visibility.Visible : Visibility.Collapsed;
            DeleteSessionCommand = new RelayCommand(DeleteSessionAction, o=> SelectedSession!=null
                                                                             &&_loggedClient.IsAdmin);
        }

        public void RefreshSessions()
        {
            GameSessions = new List<GameSession>(_loginSessionNetworkClient.RefreshSessionList());
            GameSessions = GameSessions.Where(t => t != null).ToList();
            
            OnPropertyChanged(nameof(GameSessions));
        }

        private void BackAction(object obj)
        {
            _frame.ShowSelectCharacterPage();
        }

        private void ConnectAction(object obj)
        {
            if (_loginSessionNetworkClient.TryConnectToGameSession(SelectedSession.Id))
            {
                (new SessionWindow(SelectedSession, _character, _parentWindow, _loggedClient)).Show();
                _parentWindow.Hide();
            }
            else
            {
                MessageBox.Show("Sorry, session is full",
                    "Connection error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Asterisk);
            }
        }

        private void DeleteSessionAction(object obj)
        {
            if (_loginSessionNetworkClient.DeleteGameSession(SelectedSession.Id, _loggedClient.SessionToken))
            {
                RefreshSessions();
            }
            else
            {
                MessageBox.Show("Sorry you can't delete this session", 
                    "ooops!",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}