using SampRcon.ViewModels.Rcon.RconLogin;
using SampRcon.Views.Base;
using Xamarin.Forms.Xaml;

namespace SampRcon.Views.Rcon.RconLogin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RconLoginViewPage : ContentPageBase
    {
        public RconLoginViewPage()
        {
            InitializeComponent();

            BindingContext = new RconLoginViewModel();
        }

        protected override void OnAppearing()
        {
            var vm = (RconLoginViewModel)BindingContext;
            vm.GetRconPasswordCommand.Execute(null);
            base.OnAppearing();
        }
    }
}