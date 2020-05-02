using SampRcon.Models;
using SampRcon.Resources;
using SampRcon.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SampRcon.ViewModels.Rcon.RconHome
{
    public class RconHomeViewModel : ServerBaseViewModel
    {
        private ObservableCollection<RconOption> _rconOptions;

        public ObservableCollection<RconOption> RconOptions
        {
            get => _rconOptions;
            set
            {
                _rconOptions = value;
                RaiseOnPropertyChanged();
            }
        }

        public ICommand NavigateToCommand => new Command<string>(async (id) => await NavigateTo(id));

        public RconHomeViewModel()
        {
            InitializeRconOptions();
        }

        private async Task NavigateTo(string optionId)
        {
            var jsonServer = SerializeServer(Server);

            if (optionId == "0")
            {
                await Shell.Current.GoToAsync($"rconCommands?currentServer={jsonServer}&currentRconPassword={RconPassword}");
            }

            if (optionId == "1")
            {
                await Shell.Current.GoToAsync($"rconPlayers?currentServer={jsonServer}&currentRconPassword={RconPassword}");
            }
        }

        private void InitializeRconOptions()
        {
            RconOptions = new ObservableCollection<RconOption>
            {
                new RconOption
                {
                    Id = 0,
                    Title = AppResources.ResourceManager.GetString("RconHomeCommandTitle"),
                    Icon = "cmd"
                },
                new RconOption
                {
                    Id = 1,
                    Title = AppResources.ResourceManager.GetString("PlayerLabel"),
                    Icon = "user"
                }
            };
        }
    }
}