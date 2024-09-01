using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin.Forms;
using Plugin.CurrentActivity;

namespace SampRcon.Droid
{
    [Activity(
        Label = "SA-MP RCON",
        Icon = "@mipmap/ic_launcher",
        RoundIcon = "@mipmap/ic_round_launcher",
        Theme = "@style/MainTheme",
        MainLauncher = false,
        ResizeableActivity = false,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static MainActivity instance;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Forms.SetFlags("SwipeView_Experimental");
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            instance = this;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}