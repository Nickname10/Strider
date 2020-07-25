using System.Windows.Controls;
using Authorization.ViewModels.Authorization;

namespace Authorization.Pages.AuthorizationPages
{
    public partial class RegisterPage : Page
    {
        public RegisterPage(AuthorizationViewModel frame)
        {
            InitializeComponent();
            DataContext = new RegisterViewModel(frame);
        }
    }
}