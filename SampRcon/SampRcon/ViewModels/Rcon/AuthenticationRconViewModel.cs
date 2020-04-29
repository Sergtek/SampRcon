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
    public class AuthenticationRconViewModel : ServerBaseViewModel
    {
        private string _errorAlertValue;
        private bool _errorAlertVisible;

        public string RconPassword
        {
            get => Server.RconPassword;
            set
            {
                Server.RconPassword = value;
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
            if (Int32.TryParse(Server.Port, out portInteger))
            {
                var sQuery = new RCONQuery(Server.IP, portInteger, Server.RconPassword);
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
                var count = sQuery.Receive();
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
            var jsonServer = SerializeServer(Server);
            ShellNavigationState state = Shell.Current.CurrentState;

            await Shell.Current.GoToAsync($"{state.Location}/rconview?currentServer={jsonServer}");
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