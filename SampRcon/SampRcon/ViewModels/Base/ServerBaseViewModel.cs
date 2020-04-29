using Newtonsoft.Json;
using SampRcon.Models;
using System;
using Xamarin.Forms;

namespace SampRcon.ViewModels.Base
{
    [QueryProperty("CurrentServer", "currentServer")]
    public class ServerBaseViewModel : BaseViewModel
    {
        private Server _server = new Server();

        public string CurrentServer
        {
            set => Server = DeserializeServer(Uri.UnescapeDataString(value));
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
    }
}