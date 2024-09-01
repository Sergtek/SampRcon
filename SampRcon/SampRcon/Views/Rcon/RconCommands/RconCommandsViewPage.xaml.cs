using SampRcon.ViewModels.Rcon.RconCommands;
using SampRcon.Views.Base;
using Xamarin.Forms.Xaml;

namespace SampRcon.Views.Rcon.RconCommands
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RconCommandsViewPage : ContentPageBase
    {
        public RconCommandsViewPage()
        {
            InitializeComponent();

            BindingContext = new RconCommandsViewModel();
            sendCommand.Clicked += SendCommand_Clicked;
        }

        private void SendCommand_Clicked(object sender, System.EventArgs e)
        {
            var vm = (RconCommandsViewModel)BindingContext;
            commandsCollection.ScrollTo(vm.LogValue.Count);
        }
    }
}