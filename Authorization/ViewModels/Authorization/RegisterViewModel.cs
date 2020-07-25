using System.Windows;
using System.Windows.Controls;
using Authorization.Models.AuthorizationModels;
using HashCalculator;

namespace Authorization.ViewModels.Authorization
{
    public class RegisterViewModel
    {
        public RegisterForm RegisterForm { get; set; } = new RegisterForm();
        public RelayCommand BackCommand { get; }
        public RelayCommand RegisterCommand { get; }
        private readonly AuthorizationViewModel _frame;
        public RegisterViewModel(AuthorizationViewModel frame)
        {
            _frame = frame;
            BackCommand = new RelayCommand(BackAction);
            RegisterCommand = new RelayCommand(RegisterAction);
        }
        private void BackAction(object obj)
        {
            _frame.ShowLoginPage();
        }
        private void RegisterAction(object obj)
        {
            if (!(obj is StackPanel passwordsStackPanel)) return;

            foreach (var i in passwordsStackPanel.Children)
            {
                if (!(i is StackPanel childStackPanel)) continue;
                foreach (var j in childStackPanel.Children)
                {
                    if (!(j is PasswordBox passwordBox)) continue;
                    if (passwordBox.Name == "PasswordTextInput") RegisterForm.Password = Calculator.CalculateMd5(passwordBox.Password);
                    else RegisterForm.ConfirmPassword = Calculator.CalculateMd5(passwordBox.Password);
                }
            }
            if (RegisterForm.Validate())
            {
                var client = new RegistrationService.RegistrationClient("NetTcpBinding_IRegistration");
                if (client.Register(RegisterForm.Email, RegisterForm.Nickname, RegisterForm.Password))
                {
                    _frame.ShowLoginPage();
                }
                else
                {
                    MessageBox.Show("Register server error");
                }
                client.Close();
            }

        }

    }
}