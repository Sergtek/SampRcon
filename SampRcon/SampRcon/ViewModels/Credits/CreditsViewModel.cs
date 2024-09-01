using SampRcon.Models;
using SampRcon.Resources;
using SampRcon.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SampRcon.ViewModels.Credits
{
    public class CreditsViewModel : BaseViewModel
    {
        private ObservableCollection<CreditsItem> _creditsItems = new ObservableCollection<CreditsItem>();
        public ObservableCollection<CreditsItem> CreditsItems
        {
            get => _creditsItems;
            set
            {
                _creditsItems = value;
                RaiseOnPropertyChanged();
            }
        }

        public string AppVersion
        {
            get => AppInfo.VersionString;
        }

        public string AppName
        {
            get => AppInfo.Name;
        }

        public ICommand OpenBrowserCommand => new Command<string>(async (url) => await OpenBrowser(new Uri(url)));

        public CreditsViewModel()
        {
            InitializeCreditsItems();
        }

        private async Task OpenBrowser(Uri uri)
        {
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }

        private void InitializeCreditsItems()
        {
            CreditsItems = new ObservableCollection<CreditsItem>
            {
                new() {
                    Title = AppResources.ResourceManager.GetString("CreditsOpenCodeLabel"),
                    LinkValue = AppResources.ResourceManager.GetString("CreditsOpenCodeSite"),
                    Url = "https://github.com/Sergtek/SampRcon"
                },
                new() {
                    LinkValue = "bqlqn:",
                    Url = "https://www.flaticon.com/authors/bqlqn",
                    Icon = "save.png"
                },
                new() {
                    LinkValue = "Freepik:",
                    Url = "https://www.flaticon.com/authors/freepik",
                    Icon = "remove.png"
                },
                new() {
                    LinkValue = "Freepik:",
                    Url = "https://www.flaticon.com/authors/freepik",
                    Icon = "rcon.png"
                },
                new() {
                    LinkValue = "Freepik:",
                    Url = "https://www.flaticon.com/authors/freepik",
                    Icon = "cmd.png"
                },
                new() {
                    LinkValue = "surang:",
                    Url = "https://www.flaticon.com/authors/surang",
                    Icon = "user.png"
                },
                new() {
                    LinkValue = "Pixel perfect:",
                    Url = "https://www.flaticon.com/authors/pixel-perfect",
                    Icon = "favoritesIcon.png"
                },
                new() {
                    LinkValue = "Pixel perfect:",
                    Url = "https://www.flaticon.com/authors/pixel-perfect",
                    Icon = "serversIcon.png"
                },
                new() {
                    LinkValue = "itim2101:",
                    Url = "https://www.flaticon.com/authors/itim2101",
                    Icon = "creditsIcon.png"
                }
            };
        }
    }
}