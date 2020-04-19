using SampRcon.ViewModels.Rcon;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampRcon.Views.Rcon
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthenticationRconViewPage : ContentPage
    {
        public AuthenticationRconViewPage()
        {
            InitializeComponent();

            BindingContext = new AuthenticationRconViewModel();
            var vm = (AuthenticationRconViewModel)BindingContext;
            CreateBindings(vm);
        }

        private void CreateBindings(AuthenticationRconViewModel vm)
        {

        }
    }
}