using Newtonsoft.Json;
using SampRcon.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SampRcon.ViewModels.Base
{
    [QueryProperty("CurrentServer", "currentServer")]
    [QueryProperty("CurrentRconPassword", "currentRconPassword")]
    public class ServerBaseViewModel : BaseViewModel
    {
        private Server _server = new Server();
        private string _rconPasswordKey;
        private string _rconPassword;
        private bool _rememberRconPassword;

        public string CurrentRconPassword
        {
            set => RconPassword = Uri.UnescapeDataString(value);
        }

        public string CurrentServer
        {
            set => Server = DeserializeServer(Uri.UnescapeDataString(value));
        }

        public bool RememberRconPassword
        {
            get => _rememberRconPassword;
            set
            {
                _rememberRconPassword = value;
                RaiseOnPropertyChanged();
            }
        }

        public Server Server
        {
            get => _server;
            set
            {
                _server = value;
                RaiseOnPropertyChanged();
            }
        }

        public string RconPasswordKey
        {
            get => _rconPasswordKey;
            set
            {
                _rconPasswordKey = value;
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

        public ICommand GetRconPasswordCommand => new Command(async () => await GetRconPassword());

        protected string SerializeServer(Server server)
        {
            var jsonServer = JsonConvert.SerializeObject(server);
            return jsonServer;
        }

        protected Server DeserializeServer(string jsonServer)
        {
            var server = JsonConvert.DeserializeObject<Server>(jsonServer);
            return server;
        }        
        
        protected List<Server> DeserializePremiumServers(string jsonServers)
        {
            var premiumServers = JsonConvert.DeserializeObject<List<Server>>(jsonServers);
            return premiumServers;
        }

        protected async Task GetRconPassword()
        {
            try
            {
                _rconPasswordKey = $"{Server.ID}_rconPassword";
                var oauthToken = await SecureStorage.GetAsync(_rconPasswordKey);
                if (oauthToken != null)
                {
                    RconPassword = oauthToken;
                    RememberRconPassword = true;
                }
            }
            catch (Exception ex)
            {
                new Action(async () => await ShowDialog("Error", ex.Message, "OK"))();
            }
        }

        protected async Task SetRconPassword()
        {
            try
            {
                await SecureStorage.SetAsync(RconPasswordKey, RconPassword);
            }
            catch (Exception ex)
            {
                new Action(async () => await ShowDialog("Error", ex.Message, "OK"))();
            }
        }

        protected void RemoveRconPassword(string key)
        {
            SecureStorage.Remove(key);
        }
    }
}