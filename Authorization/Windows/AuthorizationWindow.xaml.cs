using System.Windows;
using Authorization.ViewModels.Authorization;

namespace Authorization.Windows
{
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
            var viewModel = new AuthorizationViewModel(this);
            DataContext = viewModel;
            viewModel.ShowLoginPage();
            
        }
    }
}