using SampRcon.Models;
using SampRcon.ViewModels.Base;
using System;
using Xamarin.Forms;

namespace SampRcon.ViewModels.Rcon
{
    [QueryProperty("CurrentServer", "currentServer")]
    public class RconBaseViewModel : BaseViewModel
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
    }
}