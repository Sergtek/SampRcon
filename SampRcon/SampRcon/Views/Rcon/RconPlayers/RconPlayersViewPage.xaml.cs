using SampRcon.ViewModels.Rcon.RconPlayers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampRcon.Views.Rcon.RconPlayers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RconPlayersViewPage : ContentPage
    {
        public RconPlayersViewPage()
        {
            InitializeComponent();

            BindingContext = new RconPlayersViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((RconPlayersViewModel)BindingContext).RefreshCommand.Execute(null);
        }
    }
}