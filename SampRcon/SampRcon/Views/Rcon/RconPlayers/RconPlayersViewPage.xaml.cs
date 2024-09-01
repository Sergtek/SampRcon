using SampRcon.ViewModels.Rcon.RconPlayers;
using SampRcon.Views.Base;
using Xamarin.Forms.Xaml;

namespace SampRcon.Views.Rcon.RconPlayers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RconPlayersViewPage : ContentPageBase
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