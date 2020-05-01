using SampRcon.Models;
using SampRcon.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Linq;
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

        public ICommand NavigateInfoServerCommand => new Command<Server>(async (server) => await ServerInfoNavigate(server));

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

            foreach (var localServer in localServers)
            {
                var serverExist = FavoritesServers.Where(x => x.ServerID == localServer.ServerID).Any();
                if (!serverExist)
                {
                    FavoritesServers.Add(localServer);
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
            ShellNavigationState state = Shell.Current.CurrentState;
            await Shell.Current.GoToAsync($"{state.Location}/{uri}");
        }

        private async Task AuthRconNavigate(Server server)
        {
            var jsonServer = SerializeServer(server);
            ShellNavigationState state = Shell.Current.CurrentState;

            await Shell.Current.GoToAsync($"{state.Location}/authenticationrconview?currentServer={jsonServer}");
        }

        private async Task ServerInfoNavigate(Server server)
        {
            var jsonServer = SerializeServer(server);
            ShellNavigationState state = Shell.Current.CurrentState;

            await Shell.Current.GoToAsync($"{state.Location}/serversInfoView?currentServer={jsonServer}");
        }

        private async Task SaveServer(Server server)
        {
            IsRefreshing = true;
            await App.Database.SaveItemAsync(server);
            IsRefreshing = false;
        }
    }
}