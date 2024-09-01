using SampRcon.Models;
using SampRcon.ViewModels.Favorites;
using SampRcon.Views.Base;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampRcon.Views.Favorites
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoritesViewPage : ContentPageBase
    {
        public FavoritesViewPage()
        {
            InitializeComponent();

            BindingContext = new FavoritesViewModel();
            var vm = (FavoritesViewModel)BindingContext;
            CreateBindings(vm);
        }

        private void CreateBindings(FavoritesViewModel vm)
        {
            serversCollection.SetBinding(CollectionView.ItemsSourceProperty, nameof(vm.FavoritesServers));
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var server = GetServerFromSwipeClick((Element)sender);
            var vm = (FavoritesViewModel)BindingContext;
            vm.NavigateAuthRconCommand.Execute(server);
        }

        private void RconSwipeItem_Invoked(object sender, System.EventArgs e)
        {
            var server = GetServerFromSwipeClick(((SwipeItem)sender).Parent);
            var vm = (FavoritesViewModel)BindingContext;
            vm.NavigateAuthRconCommand.Execute(server);
        }

        private void DeleteSwipeItem_Invoked(object sender, System.EventArgs e)
        {
            var server = GetServerFromSwipeClick(((SwipeItem)sender).Parent);
            var vm = (FavoritesViewModel)BindingContext;
            vm.DeleteServerCommand.Execute(server);
        }

        private Server GetServerFromSwipeClick(Element swipeItem)
        {
            var selectedIp = ((Label)swipeItem.FindByName("ipServer")).Text;
            var vm = (FavoritesViewModel)BindingContext;
            var server = vm.FavoritesServers.Where(x => x.IP == selectedIp).FirstOrDefault();
            return server;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((FavoritesViewModel)BindingContext).RefreshCommand.Execute(null);
        }
    }
}