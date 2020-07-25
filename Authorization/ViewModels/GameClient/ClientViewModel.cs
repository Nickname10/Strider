using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Authorization.LoginSessionService;
using Authorization.Pages.GameClientPages;
using ClientServerModels;
using Xceed.Wpf.AvalonDock.Layout;

namespace Authorization.ViewModels.GameClient
{
    public class ClientViewModel : FrameWindowViewModel
    {
        private readonly Page _createSessionPage;
        private readonly Page _createCharacterPage;
        private readonly Page _menuPage;
        private readonly SelectCharacterPage _selectCharacterPage;
        private readonly Window _window;
        private readonly LoggedClient _loggedClient;
        private readonly LoginSessionClient _loginSessionNetworkClient;
        public ClientViewModel(Window window,LoggedClient loggedClient, LoginSessionClient loginSessionNetworkClient)
        {
            _loginSessionNetworkClient = loginSessionNetworkClient;
            _window = window;
            _loggedClient = loggedClient;
            _selectCharacterPage = new SelectCharacterPage(this, loggedClient, _loginSessionNetworkClient);
            _createCharacterPage = new CreateCharacterPage(this, loggedClient, _loginSessionNetworkClient);
            _createSessionPage = new CreateSessionPage(this, loggedClient, _loginSessionNetworkClient);
            _menuPage = new MenuPage(this, loggedClient);
        }

        public void ShowConnectPage(Character selectedCharacter)
        {
            CurrentPage = new ConnectPage(this, _window, _loggedClient, _loginSessionNetworkClient, selectedCharacter );
        }

        public void ShowSelectCharacterPage()
        {
            _selectCharacterPage.ViewModel.RefreshCharacterList();
            CurrentPage = _selectCharacterPage;
        }

        public void ShowCreateSessionPage()
        {
            CurrentPage = _createSessionPage;
        }
        public void ShowCreateCharacterPage()
        {
            CurrentPage = _createCharacterPage;
        }

        public void ShowMenuPage()
        {
            CurrentPage = _menuPage;
        }

        public override void OnWindowClosing(object sender, CancelEventArgs e)
        {
            _loginSessionNetworkClient.Disconnect(_loggedClient.SessionToken);
        }

        public void CloseWindow()
        {
            _window.Close();
        }
    }
}