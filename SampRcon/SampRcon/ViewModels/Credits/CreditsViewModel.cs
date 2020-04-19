using SampRcon.ViewModels.Base;
using Xamarin.Essentials;

namespace SampRcon.ViewModels.Credits
{
    public class CreditsViewModel : BaseViewModel
    {
        public string AppVersion
        {
            get => AppInfo.VersionString;
        }

        public string AppName
        {
            get => AppInfo.Name;
        }
    }
}