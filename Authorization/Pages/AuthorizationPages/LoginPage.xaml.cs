using System.Windows.Controls;
using Authorization.ViewModels.Authorization;

namespace Authorization.Pages.AuthorizationPages
{
    public partial class LoginPage : Page
    {
        public LoginPage(AuthorizationViewModel frame)
        {
            InitializeComponent();
            DataContext = new LoginViewModel(frame);
        }
    }
}