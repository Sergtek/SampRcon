using SampRcon.Models;
using SampRcon.ViewModels.SACNR;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampRcon.Views.Servers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServersViewPage : ContentPage
    {
        public ServersViewPage()
        {
            InitializeComponent();

            BindingContext = new ServersViewModel();
            var vm = (ServersViewModel)BindingContext;
            CreateBindings(vm);
        }

        private void CreateBindings(ServersViewModel vm)
        {
            serversCollection.SetBinding(CollectionView.ItemsSourceProperty, nameof(vm.ServersList));
        }

        private void SaveSwipeItem_Invoked(object sender, System.EventArgs e)
        {
            var server = GetServerFromSwipeClick(((SwipeItem)sender).Parent);
            var vm = (ServersViewModel)BindingContext;
            vm.SaveServerCommand.Execute(server);
        }

        private void RconSwipeItem_Invoked(object sender, System.EventArgs e)
        {
            var server = GetServerFromSwipeClick(((SwipeItem)sender).Parent);
            var vm = (ServersViewModel)BindingContext;
            vm.NavigateCommand.Execute(server);
        }

        private Server GetServerFromSwipeClick(Element swipeItem)
        {
            var selectedIp = ((Label)swipeItem.FindByName("ipServer")).Text;
            var vm = (ServersViewModel)BindingContext;
            var server = vm.ServersList.Where(x => x.IP == selectedIp).FirstOrDefault();
            return server;
        }

        private void SwipeItem_Invoked(object sender, EventArgs e)
        {
            var selectedServer = serversCollection.SelectedItem = (sender as SwipeView).BindingContext;
            ((ServersViewModel)BindingContext).NavigateInfoServerCommand.Execute(selectedServer);
        }
    }
}