using SampRcon.Models;
using SampRcon.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SampRcon.ViewModels.Favorites
{
    public class FavoritesViewModel : ServerBaseViewModel
    {
        private ObservableCollection<Server> _favoritesServers;
        public ObservableCollection<Server> FavoritesServers
        {
            get => _favoritesServers;
            set
            {
                _favoritesServers = value;
                RaiseOnPropertyChanged();
            }
        }

        public ICommand RefreshCommand => new Command(async () => await GetAllServers());

        public ICommand DeleteServerCommand => new Command<Server>(async (server) => await DeleteServer(server));

        public ICommand NavigateCommand => new Command<string>(async (uri) => await NavigateTo(uri));

        public ICommand NavigateAuthRconCommand => new Command<Server>(async (server) => await AuthRconNavigate(server));

        public ICommand SaveServerCommand => new Command<Server>(async (server) => await SaveServer(server));

        public FavoritesViewModel()
        {
            FavoritesServers = new ObservableCollection<Server>();
            new Action(async () => await GetAllServers())();
        }

        private async Task GetAllServers()
        {
            IsRefreshing = true;
            var localServers = await App.Database.GetItemsAsync();

            foreach(var server in localServers)
            {
                var serverExist = FavoritesServers.Where(x => x.ID == server.ID).Any();
                if (!serverExist)
                {
                    FavoritesServers.Add(server);
                }
                else
                {
                    var currentServer = FavoritesServers.Where(x => x.ID == server.ID).FirstOrDefault();
                    currentServer = server;
                }
            }
            IsRefreshing = false;
        }

        private async Task DeleteServer(Server server)
        {
            RemoveRconPassword($"{Server.ID}_rconPassword");
            await App.Database.DeleteItemAsync(server);
            FavoritesServers.Remove(server);
        }

        private async Task NavigateTo(string uri)
        {
            var stackCount = Shell.Current.Navigation.NavigationStack.Count;
            if (stackCount == 1)
            {
                await Shell.Current.GoToAsync($"{uri}");
            }
        }

        private async Task AuthRconNavigate(Server server)
        {
            var regexItem = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)");
            var hostname = server.Hostname != null ? server.Hostname : string.Empty;
            if (regexItem.IsMatch(hostname))
            {
                var cleanHostname = RemoveSpecialCharacters(server.Hostname);
                server.Hostname = cleanHostname.Trim();
            }

            var jsonServer = SerializeServer(server);
            var stackCount = Shell.Current.Navigation.NavigationStack.Count;
            if (stackCount == 1)
            {
                await Shell.Current.GoToAsync($"rconLogin?currentServer={jsonServer}");
            }
        }

        private async Task SaveServer(Server server)
        {
            IsRefreshing = true;
            await App.Database.SaveItemAsync(server);
            IsRefreshing = false;
        }
    }
}