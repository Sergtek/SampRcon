using Newtonsoft.Json;
using SampRcon.Models;
using SampRcon.Models.Mappers;
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
    public class ServersViewModel : ServerBaseViewModel
    {
        private const string _serversMasterEndpoint = "https://api.open.mp/servers";

        private HttpClient _client = new HttpClient { MaxResponseContentBufferSize = 1000000 };
        public HttpClient Client
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

        private List<Server> _auxiliarServersList;
        public List<Server> AuxiliarServersList
        {
            get => _auxiliarServersList;
            set
            {
                _auxiliarServersList = value;
                RaiseOnPropertyChanged();
            }
        }

        private List<string> _masterServers = new List<string> { string.Empty };
        public List<string> MasterServers
        {
            get => _masterServers;
            set
            {
                _masterServers = value;
                RaiseOnPropertyChanged();
            }
        }

        private string _players = "0";
        public string Players
        {
            get => _players;
            set
            {
                _players = value;
                RaiseOnPropertyChanged();
            }
        }

        private string _averagePlayers;
        public string AveragePlayers
        {
            get => _averagePlayers;
            set
            {
                _averagePlayers = value;
                RaiseOnPropertyChanged();
            }
        }

        private bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                RaiseOnPropertyChanged();
            }
        }

        public ICommand NavigateCommand => new Command<Server>(async (server) => await AuthRconNavigate(server));

        public ICommand SaveServerCommand => new Command<Server>(async (server) => await SaveServer(server));

        public ICommand NavigatePremiumCommand => new Command<string>(async (uri) => await OpenBrowser(new Uri(uri)));

        public Command RefreshItemsCommand { get; set; }

        public ICommand OpenBrowserCommand => new Command<string>(async (url) => await OpenBrowser(new Uri(url)));

        public ServersViewModel()
        {
            InitializeCommands();
            ServersList = new ObservableCollection<Server>();
            AuxiliarServersList = new List<Server>();

            if (ServersList.Count == 0)
            {
                RefreshItemsCommand.Execute(null);
            }
        }

        private void InitializeCommands()
        {
            RefreshItemsCommand = new Command(async () =>
            {
                if (ServersList.Any())
                {
                    ServersList.Clear();
                }
                await LoadMasterServers();
                IsRefreshing = false;
            });
        }

        private new async Task PageAppearing()
        {
            await base.PageAppearing();
        }

        private new async Task PageDisappearing()
        {
            await base.PageDisappearing();
        }

        private async Task LoadMasterServers()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                await GetServers();
            }
            catch (Exception ex)
            {
                new Action(async () => await ShowDialog("Error", ex.Message, "OK"))();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task SaveServer(Server server)
        {
            await App.Database.SaveItemAsync(server);
        }

        private async Task AuthRconNavigate(Server server)
        {
            var jsonServer = SerializeServer(server);
            await Shell.Current.GoToAsync($"/rconLogin?currentServer={jsonServer}");
        }

        private async Task<List<Server>> GetServers()
        {
            try
            {
                HttpResponseMessage response = await Client.GetAsync(_serversMasterEndpoint);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(content))
                    {
                        var result = JsonConvert.DeserializeObject<List<ServerResponse>>(content);
                        if (result != null)
                        {
                            foreach (var server in result)
                            {
                                var ip = new List<string>(server.Ip.Split(new char[] { ':' }));
                                server.Port = ip[1];
                                var serverMapped = server.MapToModel();
                                ServersList.Add(serverMapped);
                            }
                            Players = result.Sum(x => x.Pc).ToString();
                            var average = Convert.ToDouble(Players) / Convert.ToDouble(result.Count);
                            AveragePlayers = Math.Round(average, 1).ToString().Replace(",", ".");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}