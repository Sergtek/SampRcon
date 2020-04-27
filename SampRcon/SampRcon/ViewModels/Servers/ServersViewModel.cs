using SampRcon.Mappers;
using SampRcon.Models;
using SampRcon.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SampRcon.ViewModels.SACNR
{
    public class ServersViewModel : BaseViewModel
    {
        private const string _serversMasterEndpoint = "https://monitor.sacnr.com/list/masterlist.txt";
        private const string _serverEndpoint = "https://monitor.sacnr.com/api/?IP=&Port=&Action=info&Format=JSON";
        private HttpClient _client = new HttpClient() { MaxResponseContentBufferSize = 1000000 };
        private HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new HttpClient();
                }
                return _client;
            }
        }

        private ObservableCollection<Server> _serversList;
        public ObservableCollection<Server> ServersList
        {
            get => _serversList;
            set
            {
                _serversList = value;
                RaiseOnPropertyChanged();
            }
        }

        private List<string> _ipServers = new List<string> { string.Empty };
        public List<string> IpServers
        {
            get => _ipServers;
            set
            {
                _ipServers = value;
                RaiseOnPropertyChanged();
            }
        }

        public ICommand RefreshCommand => new Command(async () => await ExecuteRefreshCommand());
        public ICommand NavigateCommand => new Command<Server>(async (server) => await AuthRconNavigate(server));
        public ICommand SaveServerCommand => new Command<Server>(async (server) => await SaveServer(server));


        public ServersViewModel()
        {
            ServersList = new ObservableCollection<Server>();
            new Action(async () => await ExecuteRefreshCommand())();
        }

        private async Task ExecuteRefreshCommand()
        {
            IsRefreshing = true;
            await GetAllServers();
            IsRefreshing = false;
        }

        private async Task GetAllServers()
        {
            try
            {
                var result = await ProcessURLAsync(_serversMasterEndpoint, Client);
                IpServers = new List<string>(result.Split('\n'));
                var numberServers = IpServers.Count;
                var localServersCount = ServersList.Count;
                var maxNewServers = 9;
                var newServersAdded = 0;

                for (var i = localServersCount; i < numberServers && newServersAdded <= maxNewServers; i++)
                {
                    var currentServerFromList = ServersList.Where(x => x.IP == IpServers[i]);
                    var existServer = currentServerFromList.Any();

                    var splitServer = IpServers[i].Split(':');
                    var serverIp = splitServer.First();
                    var serverPort = splitServer[1];
                    var serverEndpoint = _serverEndpoint;
                    serverEndpoint = serverEndpoint.Replace("IP=&Port=", $"IP={serverIp}&Port={serverPort}");
                    var serverData = await ProcessURLAsync(serverEndpoint, Client);
                    var currentServer = DeserializeServer(serverData);
                    var mapServer = currentServer.MapToModel();

                    if (existServer && currentServerFromList.FirstOrDefault().Equals(mapServer))
                    {
                        continue;
                    }

                    if (existServer)
                    {
                        var server = ServersList[i];
                        server = mapServer;
                    }
                    else
                    {
                        newServersAdded++;
                        ServersList.Add(mapServer);
                    }
                }
            }
            catch (Exception ex)
            {
                new Action(async () => await ShowDialog("Error", ex.Message, "OK"))();
            }
        }

        private async Task SaveServer(Server server)
        {
            await App.Database.SaveItemAsync(server);
        }

        private async Task AuthRconNavigate(Server server)
        {
            var jsonServer = SerializeServer(server);
            ShellNavigationState state = Shell.Current.CurrentState;

            await Shell.Current.GoToAsync($"{state.Location}/authenticationrconview?currentServer={jsonServer}");
        }

        private async Task<string> ProcessURLAsync(string url, HttpClient client)
        {
            var result = await Client.GetStringAsync(url);
            return result;
        }
    }
}