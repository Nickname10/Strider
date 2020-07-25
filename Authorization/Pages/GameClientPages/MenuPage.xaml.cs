using System.Windows.Controls;
using Authorization.ViewModels.GameClient;
using ClientServerModels;

namespace Authorization.Pages.GameClientPages
{

    public partial class MenuPage : Page
    {
        public MenuPage(ClientViewModel frame, LoggedClient loggedClient)
        {
            InitializeComponent();
            DataContext = new MenuViewModel(frame, loggedClient);
        }
    }
}
