using SampRcon.Models;
using SampRcon.Resources;
using SampRcon.ViewModels.Base;
using System.Collections.ObjectModel;
using Xamarin.Essentials;

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

        public CreditsViewModel()
        {
            InitializeCreditsItems();
        }

        private void InitializeCreditsItems()
        {
            CreditsItems = new ObservableCollection<CreditsItem>
            {
                new CreditsItem
                {
                    Title = AppResources.ResourceManager.GetString("DonateLabel"),
                    LinkValue = AppResources.ResourceManager.GetString("DonateName"),
                    Url = "https://www.paypal.com/pools/c/8oehPDNLug"
                },                
                new CreditsItem
                {
                    Title = AppResources.ResourceManager.GetString("CreditsOpenCodeLabel"),
                    LinkValue = AppResources.ResourceManager.GetString("CreditsOpenCodeSite"),
                    Url = "https://github.com/nacompllo/SampRcon"
                },
                new CreditsItem
                {
                    Title = AppResources.ResourceManager.GetString("CreditsApisLabel"),
                    LinkValue = "SACNR Monitor",
                    Url = "https://monitor.sacnr.com/api.html"
                },
                new CreditsItem
                {
                    Title = AppResources.ResourceManager.GetString("CreditsQueryLabel"),
                    LinkValue = "SA-MP Wiki",
                    Url = "https://wiki.sa-mp.com/wiki/Query_Mechanism/Csharp"
                },
                new CreditsItem
                {
                    LinkValue = "bqlqn:",
                    Url = "https://www.flaticon.com/authors/bqlqn",
                    Icon = "save"
                },
                new CreditsItem
                {
                    LinkValue = "Freepik:",
                    Url = "https://www.flaticon.com/authors/freepik",
                    Icon = "remove"
                },
                new CreditsItem
                {
                    LinkValue = "Freepik:",
                    Url = "https://www.flaticon.com/authors/freepik",
                    Icon = "rcon"
                },
                new CreditsItem
                {
                    LinkValue = "Freepik:",
                    Url = "https://www.flaticon.com/authors/freepik",
                    Icon = "cmd"
                },
                new CreditsItem
                {
                    LinkValue = "surang:",
                    Url = "https://www.flaticon.com/authors/surang",
                    Icon = "user"
                },
                new CreditsItem
                {
                    LinkValue = "Pixel perfect:",
                    Url = "https://www.flaticon.com/authors/pixel-perfect",
                    Icon = "favoritesIcon"
                },
                new CreditsItem
                {
                    LinkValue = "Pixel perfect:",
                    Url = "https://www.flaticon.com/authors/pixel-perfect",
                    Icon = "serversIcon"
                },
                new CreditsItem
                {
                    LinkValue = "itim2101:",
                    Url = "https://www.flaticon.com/authors/itim2101",
                    Icon = "creditsIcon"
                }
            };
        }
    }
}