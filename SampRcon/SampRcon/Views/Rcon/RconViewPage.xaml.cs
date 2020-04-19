using SampRcon.ViewModels.Rcon;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampRcon.Views.Rcon
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RconViewPage : ContentPage
    {
        public RconViewPage()
        {
            InitializeComponent();

            BindingContext = new RconViewModel();
            var vm = (RconViewModel)BindingContext;
            CreateBindings(vm);
        }

        private void CreateBindings(RconViewModel vm)
        {

        }
    }
}