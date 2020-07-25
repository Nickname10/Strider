using System.Windows.Controls;
using Authorization.LoginSessionService;
using Authorization.ViewModels.GameClient;
using ClientServerModels;

namespace Authorization.Pages.GameClientPages
{
    public partial class CreateSessionPage : Page
    {
        public CreateSessionPage(ClientViewModel frame, LoggedClient loggedClient, LoginSessionClient loginSessionNetworkClient)
        {
            InitializeComponent();
            DataContext = new CreateSessionViewModel(frame, loggedClient, loginSessionNetworkClient);
        }
    }
}
