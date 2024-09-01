using SampRcon.Resources;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SampRcon.ViewModels.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private bool _isProVersion;
        public bool IsProVersion
        {
            get => _isProVersion;
            set
            {
                _isProVersion = value;
                RaiseOnPropertyChanged();
            }
        }

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

        public ICommand PageAppearingCommand => new Command(async () => await PageAppearing());

        public ICommand PageDisappearingCommand => new Command(async () => await PageDisappearing());

        public async Task PageAppearing()
        {

        }

        public async Task PageDisappearing()
        {

        }

        protected async Task ShowDialog(string title, string message, string confirmButton)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, confirmButton);
        }

        protected async Task<bool> ShowDialogResult(string title, string message, string confirmButton, string cancelButton)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, confirmButton, cancelButton);
        }

        protected async Task OpenBrowser(Uri uri)
        {
            await Browser.OpenAsync(uri, BrowserLaunchMode.External);
        }

        public static string RemoveSpecialCharacters(string input)
        {
            if (input != null)
            {
                Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
                return r.Replace(input, string.Empty);
            }
            return string.Empty;
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