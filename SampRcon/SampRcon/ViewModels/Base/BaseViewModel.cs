using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SampRcon.ViewModels.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private bool _isRefreshing;

        protected bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                RaiseOnPropertyChanged();
            }
        }

        protected async Task ShowDialog(string title, string message, string confirmButton)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, confirmButton);
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaiseOnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}