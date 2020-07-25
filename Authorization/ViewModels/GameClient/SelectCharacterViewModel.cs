using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using Authorization.Annotations;
using Authorization.LoginSessionService;
using ClientServerModels;
using MessageBox = Xceed.Wpf.Toolkit.MessageBox;

namespace Authorization.ViewModels.GameClient
{
    public class SelectCharacterViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Character> AvailableCharacters { get; private set; }
        private Character _selectedCharacter;
        public Brush PreviewStrokeFill { get; set; }
        public Brush PreviewFill { get; set; }
        public string PreviewStrokeDashArray { get; set; }

        public Character SelectedCharacter
        {
            get => _selectedCharacter;
            set
            {
                _selectedCharacter = value;
                PreviewFill = new SolidColorBrush(Color.FromRgb(
                    _selectedCharacter.MainRedColor,
                    _selectedCharacter.MainGreenColor,
                    _selectedCharacter.MainBlueColor));
                PreviewStrokeFill = new SolidColorBrush(Color.FromRgb(
                    _selectedCharacter.StrokeRedColor,
                    _selectedCharacter.StrokeGreenColor,
                    _selectedCharacter.StrokeBlueColor));
                PreviewStrokeDashArray = _selectedCharacter.StrokeLength.ToString() +
                                         " " +
                                         _selectedCharacter.StrokeSpace.ToString();
                OnPropertyChanged(nameof(PreviewFill));
                OnPropertyChanged(nameof(PreviewStrokeFill));
                OnPropertyChanged(nameof(PreviewStrokeDashArray));
                OnPropertyChanged();
            }
        }

        public RelayCommand SelectCommand { get; }
        public RelayCommand SelectDefaultCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand BackCommand { get; }

        private readonly ClientViewModel _frame;
        private readonly LoggedClient _loggedClient;
        private readonly LoginSessionClient _loginSessionNetworkClient;

        public SelectCharacterViewModel(ClientViewModel frame, LoggedClient loggedClient,
            LoginSessionClient loginSessionNetworkClient)
        {
            _frame = frame;
            _loggedClient = loggedClient;
            _loginSessionNetworkClient = loginSessionNetworkClient;

            AvailableCharacters = new ObservableCollection<Character>(
                _loginSessionNetworkClient.GetCharacters(_loggedClient.SessionToken));

            SelectCommand = new RelayCommand(SelectAction, o => SelectedCharacter != null);
            SelectDefaultCommand = new RelayCommand(SelectDefaultAction);
            DeleteCommand = new RelayCommand(DeleteAction);
            BackCommand = new RelayCommand(BackAction);
        }

        private void SelectAction(object obj)
        {
           _frame.ShowConnectPage(SelectedCharacter);
        }

        private void SelectDefaultAction(object obj)
        {
            SelectedCharacter = new Character()
            {
                Name = "Default",
                MainRedColor = 0,
                MainBlueColor = 0,
                MainGreenColor = 0,
                StrokeRedColor = 0,
                StrokeGreenColor = 0,
                StrokeBlueColor = 0,
                StrokeLength = 10,
                StrokeSpace = 0
            };
            _frame.ShowConnectPage(SelectedCharacter);
        }

        public void RefreshCharacterList()
        {
            AvailableCharacters = new ObservableCollection<Character>(
                _loginSessionNetworkClient.GetCharacters(_loggedClient.SessionToken));
            OnPropertyChanged(nameof(AvailableCharacters));
        }

        private void DeleteAction(object obj)
        {
            if (!_loginSessionNetworkClient.DeleteCharacter(SelectedCharacter, _loggedClient.SessionToken))
                MessageBox.Show("Delete error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            AvailableCharacters = new ObservableCollection<Character>(
                _loginSessionNetworkClient.GetCharacters(_loggedClient.SessionToken));

            OnPropertyChanged(nameof(AvailableCharacters));
        }

        private void BackAction(object obj)
        {
            _frame.ShowMenuPage();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}