using System.Windows;
using System.Windows.Controls;
using Authorization.LoginSessionService;
using Authorization.ViewModels.GameClient;
using ClientServerModels;

namespace Authorization.Pages.GameClientPages
{
    public partial class ConnectPage : Page
    {
        public ConnectViewModel ViewModel { get; } 
        public ConnectPage(ClientViewModel frame, Window parentWindow, LoggedClient loggedClient,
            LoginSessionClient loginSessionNetworkClient, Character character)
        {
            InitializeComponent();
            ViewModel = new ConnectViewModel(frame, parentWindow, loggedClient, loginSessionNetworkClient, character);
            DataContext = ViewModel;
        }
    }
}