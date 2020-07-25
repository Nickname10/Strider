using System.Windows;
using Authorization.ViewModels.SessionClient;
using ClientServerModels;

namespace Authorization.Windows
{
    public partial class SessionWindow : Window
    {
        public SessionWindow(GameSession selectedSession, Character selectedCharacter,Window parentWindow, LoggedClient loggedClient)
        {
            InitializeComponent();
            var viewModel = new SessionViewModel(selectedSession, selectedCharacter,  parentWindow, this, loggedClient);
            DataContext = viewModel;
            Closing += viewModel.OnWindowClosing;
        }
    }
}