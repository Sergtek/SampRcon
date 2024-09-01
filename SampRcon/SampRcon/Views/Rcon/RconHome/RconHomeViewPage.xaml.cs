using SampRcon.ViewModels.Rcon.RconHome;
using SampRcon.Views.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampRcon.Views.Rcon.RconHome
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RconHomeViewPage : ContentPageBase
    {
        public RconHomeViewPage()
        {
            InitializeComponent();

            BindingContext = new RconHomeViewModel();
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            var selectedItem = ((StackLayout)sender).FindByName("itemId");
            var currentItem = ((Label)selectedItem).Text;
            var vm = (RconHomeViewModel)BindingContext;
            vm.NavigateToCommand.Execute(currentItem);
        }
    }
}