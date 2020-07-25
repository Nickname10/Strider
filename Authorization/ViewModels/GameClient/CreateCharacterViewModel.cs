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
    public class CreateCharacterViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> AvailableStrokeLength { get; }
        public ObservableCollection<string> AvailableStrokeSpace { get; }
        
        public string Name { get; set; }

        private string _selectedStrokeLength;
        
        public string SelectedStrokeLength
        {
            get => _selectedStrokeLength;
            set
            {
                
                _selectedStrokeLength = value;
                PreviewStrokeDashArray = _selectedStrokeLength + " " + _selectedStrokeSpace;
                OnPropertyChanged(nameof(PreviewStrokeDashArray));
            }
        }

        private string _selectedStrokeSpace;

        public string SelectedStrokeSpace
        {
            get => _selectedStrokeSpace;
            set
            {
                _selectedStrokeSpace = value;
                PreviewStrokeDashArray = _selectedStrokeLength + " " + _selectedStrokeSpace;
                OnPropertyChanged(nameof(PreviewStrokeDashArray));
            }
        }

        private Color? _selectedMainColor;
        public Color? SelectedMainColor
        {
            get => _selectedMainColor;
            set
            {
                _selectedMainColor = value;
                if (_selectedMainColor != null) PreviewMainColor = new SolidColorBrush(_selectedMainColor.Value);
                OnPropertyChanged(nameof(PreviewMainColor));
            }
        }

        private Color? _selectedSecondColor;
        public Color? SelectedSecondColor
        {
            get => _selectedSecondColor;
            set
            {
                _selectedSecondColor = value;
                if (_selectedSecondColor != null) PreviewSecondColor = new SolidColorBrush(_selectedSecondColor.Value);
                OnPropertyChanged(nameof(PreviewSecondColor));
            }
        }

        public RelayCommand BackCommand { get; }
        public RelayCommand CreateCharacterCommand { get; }

        public Brush PreviewMainColor { get; private set; } 
        public Brush PreviewSecondColor { get; private set; }
        public string PreviewStrokeDashArray { get; private set; }

        private readonly ClientViewModel _frame;
        private readonly LoginSessionClient _loginSessionNetworkClient;
        private readonly LoggedClient _loggedClient;
        public CreateCharacterViewModel(ClientViewModel frame, LoggedClient loggedClient, LoginSessionClient loginSessionNetworkClient)
        {
            _frame = frame;
            _loggedClient = loggedClient;
            _loginSessionNetworkClient = loginSessionNetworkClient;
            AvailableStrokeLength = new ObservableCollection<string>();
            AvailableStrokeSpace = new ObservableCollection<string>();
            for (var i = 0; i < 10; i++)
            {
                AvailableStrokeLength.Add(i.ToString());
                AvailableStrokeSpace.Add(i.ToString());
            }
            SelectedMainColor = Color.FromRgb(0, 255, 0);
            SelectedSecondColor = Color.FromRgb(255, 0, 0);
            SelectedStrokeSpace = "3";
            SelectedStrokeLength = "3";

            BackCommand = new RelayCommand(BackAction);
            CreateCharacterCommand = new RelayCommand(CreateCharacterAction);
        }

        private void BackAction(object obj)
        {
            _frame.ShowMenuPage();
        }

        private void CreateCharacterAction(object obj)
        {
            try
            {
                if (Name == null)
                {
                    MessageBox.Show("Sorry, your name is not valid", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Name = Name.Trim();
                if (Name == string.Empty)
                {
                    MessageBox.Show("Sorry, your name is not valid", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (_selectedMainColor != null && _selectedSecondColor != null)
                {
                    _loginSessionNetworkClient.CreateCharacter(new Character()
                    {
                        Name = this.Name,
                        MainRedColor = _selectedMainColor.Value.R,
                        MainGreenColor = _selectedMainColor.Value.G,
                        MainBlueColor = _selectedMainColor.Value.B,
                        StrokeRedColor = _selectedSecondColor.Value.R,
                        StrokeGreenColor = _selectedSecondColor.Value.G,
                        StrokeBlueColor = _selectedSecondColor.Value.B,
                        StrokeLength = byte.Parse(_selectedStrokeLength),
                        StrokeSpace = byte.Parse(_selectedStrokeSpace)
                    }, _loggedClient.SessionToken);
                    _frame.ShowMenuPage();
                    return;
                }
                
                MessageBox.Show("Please select color");
            }
            catch
            {
                MessageBox.Show("Creation error", "Ooops!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}