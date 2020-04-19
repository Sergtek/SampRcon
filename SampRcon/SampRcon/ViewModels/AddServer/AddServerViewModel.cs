using SampRcon.Models;
using SampRcon.Resources;
using SampRcon.Utils.SAMP;
using SampRcon.ViewModels.Base;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SampRcon.ViewModels.AddServer
{
    public class AddServerViewModel : BaseViewModel
    {
        private string _ipServer;
        private string _portServer;
        private string _errorAlertValue;
        private bool _errorAlertVisible;

        public string IpServer
        {
            get => _ipServer;
            set
            {
                _ipServer = value;
                RaiseOnPropertyChanged();
            }
        }

        public string PortServer
        {
            get => _portServer;
            set
            {
                _portServer = value;
                RaiseOnPropertyChanged();
            }
        }

        public string ErrorAlertValue
        {
            get => _errorAlertValue;
            set
            {
                _errorAlertValue = value;
                RaiseOnPropertyChanged();
            }
        }

        public bool ErrorAlertVisible
        {
            get => _errorAlertVisible;
            set
            {
                _errorAlertVisible = value;
                RaiseOnPropertyChanged();
            }
        }

        public ICommand SaveServerCommand => new Command(() => GetServerInfo());

        private void GetServerInfo()
        {
            var portInteger = 0;
            if (Int32.TryParse(PortServer, out portInteger))
            {
                Query sQuery = new Query(IpServer, portInteger);
                sQuery.Send('i');
                int count = sQuery.Receive();
                string[] info = sQuery.Store(count);

                if (info.Any())
                {
                    var server = new Server
                    {
                        IP = IpServer,
                        Port = PortServer,
                        Hostname = info[3]
                    };
                    new Action(async () => await SaveServer(server))();
                }
                else
                {
                    ErrorAlertValue = AppResources.ResourceManager.GetString("AddServerErrorValue");
                    ErrorAlertVisible = true;
                }
            }
        }

        private async Task BackNavigate()
        {
            await Shell.Current.Navigation.PopAsync();
        }

        private async Task SaveServer(Server server)
        {
            await App.Database.SaveItemAsync(server);
            await BackNavigate();
        }
    }
}