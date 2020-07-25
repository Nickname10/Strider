using System;
using System.Windows;
using System.Windows.Controls;
using Authorization.Models.AuthorizationModels;
using Authorization.Windows;
using MessageBox = Xceed.Wpf.Toolkit.MessageBox;

namespace Authorization.ViewModels.Authorization
{
    public class LoginViewModel
    {
        public RelayCommand LoginCommand { get; }
        public RelayCommand RegisterCommand { get; }
        public LoginForm LoginForm { get; set; } = new LoginForm();
        private readonly AuthorizationViewModel _frame;
        public LoginViewModel(AuthorizationViewModel frame)
        {
            _frame = frame;
            LoginCommand = new RelayCommand(LoginAction);
            RegisterCommand = new RelayCommand(RegisterAction);
        }
        private void RegisterAction(object obj)
        {
            _frame.ShowRegisterPage();
        }
        private void LoginAction(object obj)
        {
            var password = (obj as PasswordBox)?.Password;
            LoginForm.Password = HashCalculator.Calculator.CalculateMd5(password);
            try
            {
                if (LoginForm.Validate())
                {
                    var client = new LoginSessionService.LoginSessionClient("NetTcpBinding_ILoginSession");
                    var loggedClient = client.Connect(LoginForm.Email, LoginForm.Password);
                    if (loggedClient == null) throw new Exception("Wrong mail or password");
                    new GameClientWindow(loggedClient, client).Show();
                    _frame.Window.Close();
                    
                }
                else
                {
                    throw new Exception("Wrong mail or password");
                }
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message,
                    "Authorization error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }


        }
    }
}