using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Authorization.Pages.AuthorizationPages;

namespace Authorization.ViewModels.Authorization
{
    public class AuthorizationViewModel : FrameWindowViewModel
    {
        private readonly Page _loginPage;
        private readonly Page _registerPage;
        public Window Window { get; }
        public AuthorizationViewModel(Window window)
        {
            _loginPage = new LoginPage(this);
            _registerPage = new RegisterPage(this);
            Window = window;
        }

        public void ShowLoginPage()
        {
            CurrentPage = _loginPage;
        }
        public void ShowRegisterPage()
        {
            CurrentPage = _registerPage;
        }

        public override void OnWindowClosing(object sender, CancelEventArgs e)
        {}
    }
}