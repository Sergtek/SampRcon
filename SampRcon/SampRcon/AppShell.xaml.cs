using SampRcon.Views.AddServer;
using SampRcon.Views.Credits;
using SampRcon.Views.Favorites;
using SampRcon.Views.Rcon.RconCommands;
using SampRcon.Views.Rcon.RconHome;
using SampRcon.Views.Rcon.RconLogin;
using SampRcon.Views.Rcon.RconPlayers;
using SampRcon.Views.Servers;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampRcon
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        Dictionary<string, Type> routes = new Dictionary<string, Type>();
        public Dictionary<string, Type> Routes { get { return routes; } }

        public string AppVersion
        {
            get => AppInfo.VersionString;
        }

        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
            BindingContext = this;
        }

        private void RegisterRoutes()
        {
            routes.Add("serversView", typeof(ServersViewPage));
            routes.Add("favoritesView", typeof(FavoritesViewPage));
            routes.Add("newServerView", typeof(AddServerViewPage));
            routes.Add("creditsView", typeof(CreditsViewPage));
            routes.Add("rconLogin", typeof(RconLoginViewPage));
            routes.Add("rconHome", typeof(RconHomeViewPage));
            routes.Add("rconCommands", typeof(RconCommandsViewPage));
            routes.Add("rconPlayers", typeof(RconPlayersViewPage));

            foreach (var item in routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }
    }
}