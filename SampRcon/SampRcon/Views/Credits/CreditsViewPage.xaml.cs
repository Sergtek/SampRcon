using SampRcon.ViewModels.Credits;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampRcon.Views.Credits
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreditsViewPage : ContentPage
    {
        public CreditsViewPage()
        {
            InitializeComponent();

            BindingContext = new CreditsViewModel();
        }
    }
}