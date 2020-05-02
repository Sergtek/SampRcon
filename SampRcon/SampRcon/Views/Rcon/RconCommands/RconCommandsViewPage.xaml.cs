using SampRcon.ViewModels.Rcon.RconCommands;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampRcon.Views.Rcon.RconCommands
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RconCommandsViewPage : ContentPage
    {
        public RconCommandsViewPage()
        {
            InitializeComponent();

            BindingContext = new RconCommandsViewModel();
        }
    }
}