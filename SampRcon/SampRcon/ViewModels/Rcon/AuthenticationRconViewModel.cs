using SampRcon.Resources;
using SampRcon.Utils.SAMP;
using SampRcon.ViewModels.Base;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SampRcon.ViewModels.Rcon
{
    [QueryProperty("IpServer", "ipServer")]
    [QueryProperty("PortServer", "portServer")]
    public class AuthenticationRconViewModel : BaseViewModel
    {
        private string _currentIpServer;
        private string _currentPortServer;
        private string _rconPassword;
        private string _errorAlertValue;
        private bool _errorAlertVisible;

        public string IpServer
        {
            set => CurrentIpServer = Uri.UnescapeDataString(value);
        }

        public string PortServer
        {
            set => CurrentPortServer = Uri.UnescapeDataString(value);
        }

        public string CurrentIpServer
        {
            get => _currentIpServer;
            set
            {
                _currentIpServer = value;
                RaiseOnPropertyChanged();
            }
        }

        public string CurrentPortServer
        {
            get => _currentPortServer;
            set
            {
                _currentPortServer = value;
                RaiseOnPropertyChanged();
            }
        }

        public string RconPassword
        {
            get => _rconPassword;
            set
            {
                _rconPassword = value;
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

        public ICommand ConnectServerCommand => new Command(ConnectServer);

        private void ConnectServer()
        {
            var portInteger = 0;
            if (Int32.TryParse(CurrentPortServer, out portInteger))
            {
                var sQuery = new RCONQuery(CurrentIpServer, portInteger, RconPassword);
                var appVersion = AppInfo.VersionString;
                var device = DeviceInfo.Model;
                var manufacturer = DeviceInfo.Manufacturer;
                var deviceName = DeviceInfo.Name;
                var version = DeviceInfo.VersionString;
                var platform = DeviceInfo.Platform;
                var idiom = DeviceInfo.Idiom;
                var deviceType = DeviceInfo.DeviceType;
                var resxMessage = AppResources.ResourceManager.GetString("WelcomeMessage");
                var welcomeMessage = String.Format(resxMessage, GetLocalAddress(), deviceName, appVersion, platform, version, manufacturer, device);
                var typeCommand = "echo";
                sQuery.Send($"{typeCommand} {welcomeMessage}");
                int count = sQuery.Receive();
                string[] info = sQuery.Store(count);

                var serverResponse = info.FirstOrDefault();
                if (!string.IsNullOrEmpty(serverResponse) && serverResponse != "Invalid RCON password.")
                {
                    new Action(async () => await RconNavigate())();
                }
                else
                {
                    ErrorAlertValue = AppResources.ResourceManager.GetString("RconAuthenticationErrorAlert");
                    ErrorAlertVisible = true;
                }
            }
        }

        private async Task RconNavigate()
        {
            ShellNavigationState state = Shell.Current.CurrentState;
            await Shell.Current.GoToAsync($"{state.Location}/rconview?ipServer={CurrentIpServer}&portServer={CurrentPortServer}&rconPassword={RconPassword}");
        }

        private string GetLocalAddress()
        {
            var IpAddress = Dns.GetHostAddresses(Dns.GetHostName()).FirstOrDefault();

            if (IpAddress != null)
                return IpAddress.ToString();

            return AppResources.ResourceManager.GetString("IpNotLocateError");
        }
    }
}