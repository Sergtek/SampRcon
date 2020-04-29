using SampRcon.Models;
using SampRcon.Utils.SAMP;
using SampRcon.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace SampRcon.ViewModels.Servers
{
    public class ServersInfoViewModel : ServerBaseViewModel
    {
        private ObservableCollection<Player> _playersList = new ObservableCollection<Player>();
        public ObservableCollection<Player> PlayersList
        {
            get => _playersList;
            set
            {
                _playersList = value;
                RaiseOnPropertyChanged();
            }
        }

        public ICommand RefreshCommand => new Command(ExecuteRefreshCommand);

        private void ExecuteRefreshCommand()
        {
            IsRefreshing = true;
            var portInteger = 0;
            if (Int32.TryParse(Server.Port, out portInteger))
            {
                var sQuery = new Query(Server.IP, portInteger);
                sQuery.Send('d');
                var count = sQuery.Receive();
                var info = sQuery.Store(count).ToList();
                var players = info.Select((e, i) => new { Item = e, Grouping = (i / 4) }).GroupBy(e => e.Grouping).ToList();

                PlayersList.Clear();
                for (var i = 0; i < players.Count(); i++)
                {
                    PlayersList.Add(new Player
                    {
                        Id = players[i].ElementAtOrDefault(0).Item,
                        Name = players[i].ElementAtOrDefault(1).Item,
                        Score = players[i].ElementAtOrDefault(2).Item,
                        Ping = players[i].ElementAtOrDefault(3).Item
                    });
                }
            }
            IsRefreshing = false;
        }
    }
}