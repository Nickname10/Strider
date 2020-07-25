using System.Windows;
using Authorization.Models.GameClientModels;
using Authorization.Pages.GameClientPages;
using ClientServerModels;

namespace Authorization.ViewModels.GameClient
{
    public class MenuViewModel
    {
        public PlayerInfo PlayerInfo { get; }
        public RelayCommand CreateSessionCommand { get; }
        public RelayCommand ConnectCommand { get; }
        public RelayCommand CreateCharacterCommand { get; }
        public RelayCommand ExitCommand { get; }
        public Visibility CreateSessionButtonVisibility { get; }
        private readonly ClientViewModel _frame;
        public MenuViewModel(ClientViewModel frame, LoggedClient loggedClient)
        {
            _frame = frame;
            PlayerInfo = new PlayerInfo(loggedClient);
            CreateSessionCommand = new RelayCommand(CreateSessionAction, obj => loggedClient.IsAdmin);
            ConnectCommand = new RelayCommand(ConnectAction);
            CreateCharacterCommand = new RelayCommand(CreateCharacterAction);
            CreateSessionButtonVisibility = loggedClient.IsAdmin ? Visibility.Visible : Visibility.Collapsed;
            ExitCommand = new RelayCommand(ExitAction);
        }

        private void CreateSessionAction(object obj)
        {
            _frame.ShowCreateSessionPage();
        }

        private void ConnectAction(object obj)
        {
            _frame.ShowSelectCharacterPage();
        }

        private void CreateCharacterAction(object obj)
        {
           _frame.ShowCreateCharacterPage();
        }

        private void ExitAction(object obj)
        {
            _frame.CloseWindow();
        }
    }
}