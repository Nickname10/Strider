using System.Windows.Controls;
using Authorization.LoginSessionService;
using Authorization.ViewModels.GameClient;
using ClientServerModels;

namespace Authorization.Pages.GameClientPages
{
    public partial class SelectCharacterPage : Page
    {
        public SelectCharacterViewModel ViewModel { get; }
        public SelectCharacterPage(ClientViewModel frame, LoggedClient loggedClient, LoginSessionClient loginSessionNetworkClient)
        {
            InitializeComponent();
            ViewModel = new SelectCharacterViewModel(frame, loggedClient, loginSessionNetworkClient);
            DataContext = ViewModel;
        }
        
    }
}