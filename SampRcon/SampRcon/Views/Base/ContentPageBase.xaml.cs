using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampRcon.Views.Base
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContentPageBase : ContentPage
    {
        public ContentPageBase()
        {
            InitializeComponent();
        }
    }
}