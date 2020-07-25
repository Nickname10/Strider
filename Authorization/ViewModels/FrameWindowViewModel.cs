using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using Authorization.Annotations;

namespace Authorization.ViewModels
{
    public abstract class FrameWindowViewModel:INotifyPropertyChanged
    {
        private Page _currentPage;

        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                if (_currentPage == value)
                    return;
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        public abstract void OnWindowClosing(object sender, CancelEventArgs e);
        

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
