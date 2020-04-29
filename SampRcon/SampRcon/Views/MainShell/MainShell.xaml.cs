using SampRcon.Views.AddServer;
using SampRcon.Views.Credits;
using SampRcon.Views.Favorites;
using SampRcon.Views.Rcon;
using SampRcon.Views.Servers;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampRcon.Views.MainShell
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainShell : Shell
    {
        Dictionary<string, Type> routes = new Dictionary<string, Type>();
        public Dictionary<string, Type> Routes { get { return routes; } }

        public MainShell()
        {
            InitializeComponent();
            RegisterRoutes();
            BindingContext = this;
        }

        private void RegisterRoutes()
        {
            routes.Add("serversView", typeof(ServersViewPage));
            routes.Add("favoritesView", typeof(FavoritesViewPage));
            routes.Add("addserverView", typeof(AddServerViewPage));
            routes.Add("authenticationrconview", typeof(AuthenticationRconViewPage));
            routes.Add("rconview", typeof(RconViewPage));
            routes.Add("creditsview", typeof(CreditsViewPage));
            routes.Add("serversInfoView", typeof(ServersInfoViewPage));

            foreach (var item in routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }
    }
}