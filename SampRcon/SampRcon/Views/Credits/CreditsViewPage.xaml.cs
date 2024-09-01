using SampRcon.ViewModels.Credits;
using SampRcon.Views.Base;
using Xamarin.Forms.Xaml;

namespace SampRcon.Views.Credits
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreditsViewPage : ContentPageBase
    {
        public CreditsViewPage()
        {
            InitializeComponent();

            BindingContext = new CreditsViewModel();
        }
    }
}