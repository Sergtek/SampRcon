using SampRcon.Utils.SQL;
using Xamarin.Forms;

namespace SampRcon
{
    public partial class App : Application
    {
        static ItemDatabase database;

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        public static ItemDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ItemDatabase();
                }
                return database;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}