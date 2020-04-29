using SampRcon.ViewModels.Servers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampRcon.Views.Servers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServersInfoViewPage : ContentPage
    {
        public ServersInfoViewPage()
        {
            InitializeComponent();

            BindingContext = new ServersInfoViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((ServersInfoViewModel)BindingContext).RefreshCommand.Execute(null);
        }
    }
}