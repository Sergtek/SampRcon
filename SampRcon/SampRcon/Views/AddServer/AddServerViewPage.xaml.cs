using SampRcon.ViewModels.AddServer;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampRcon.Views.AddServer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddServerViewPage : ContentPage
    {
        public AddServerViewPage()
        {
            InitializeComponent();

            BindingContext = new AddServerViewModel();
            var vm = (AddServerViewModel)BindingContext;
            CreateBindings(vm);
        }

        private void CreateBindings(AddServerViewModel vm)
        {

        }
    }
}