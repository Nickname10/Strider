using System.Windows.Controls;
using Authorization.LoginSessionService;
using Authorization.ViewModels.GameClient;
using ClientServerModels;

namespace Authorization.Pages.GameClientPages
{
    public partial class CreateCharacterPage : Page
    {
        public CreateCharacterPage(ClientViewModel frame, LoggedClient loggedClient, LoginSessionClient loginSessionNetworkClient)
        {
            InitializeComponent();
            DataContext = new CreateCharacterViewModel(frame, loggedClient, loginSessionNetworkClient);
        }
    }
}
