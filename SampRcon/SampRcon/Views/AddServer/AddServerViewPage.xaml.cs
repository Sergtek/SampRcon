using SampRcon.ViewModels.AddServer;
using SampRcon.Views.Base;
using Xamarin.Forms.Xaml;

namespace SampRcon.Views.AddServer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddServerViewPage : ContentPageBase
    {
        public AddServerViewPage()
        {
            InitializeComponent();

            BindingContext = new AddServerViewModel();
        }
    }
}