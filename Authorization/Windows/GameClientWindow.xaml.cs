using System.Windows;
using Authorization.LoginSessionService;
using Authorization.ViewModels.GameClient;
using ClientServerModels;

namespace Authorization.Windows
{
    public partial class GameClientWindow : Window
    {
        public GameClientWindow(LoggedClient loggedClient, LoginSessionClient loginSessionNetworkClient)
        {
            InitializeComponent();

            var viewModel = new ClientViewModel(this, loggedClient, loginSessionNetworkClient);
            DataContext = viewModel;
            viewModel.ShowMenuPage();
            Closing += viewModel.OnWindowClosing;
        }
    }
}
