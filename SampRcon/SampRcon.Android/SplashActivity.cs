using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace SampRcon.Droid
{
    [Activity(Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme.Splash", MainLauncher = true, NoHistory = true,
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashActivity : global::AndroidX.AppCompat.App.AppCompatActivity
    {
        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(new Intent(ApplicationContext, typeof(MainActivity)));
        }
    }
}