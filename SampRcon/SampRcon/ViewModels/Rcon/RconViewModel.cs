using SampRcon.Resources;
using SampRcon.Utils.SAMP;
using SampRcon.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace SampRcon.ViewModels.Rcon
{
    [QueryProperty("CurrentRconPassword", "currentRconPassword")]
    public class RconViewModel : ServerBaseViewModel
    {
        private ObservableCollection<string> _logValue;
        private string _commandValue;

        public string CurrentRconPassword
        {
            set => RconPassword = Uri.UnescapeDataString(value);
        }

        public ObservableCollection<string> LogValue
        {
            get => _logValue;
            set
            {
                _logValue = value;
                RaiseOnPropertyChanged();
            }
        }

        public string CommandValue
        {
            get => _commandValue;
            set
            {
                _commandValue = value;
                RaiseOnPropertyChanged();
                ((Command)SendRCONCommand).ChangeCanExecute();
            }
        }

        private ICommand _sendRCONCommand;
        public ICommand SendRCONCommand
        {
            get
            {
                if (_sendRCONCommand == null)
                {
                    _sendRCONCommand = new Command(
                        execute: () => SendRcon(),
                        canExecute: () => !string.IsNullOrWhiteSpace(CommandValue));
                }
                return _sendRCONCommand;
            }
        }

        private void SendRcon()
        {
            var portInteger = 0;
            if (Int32.TryParse(Server.Port, out portInteger))
            {
                var sQuery = new RCONQuery(Server.IP, portInteger, RconPassword);
                sQuery.Send(CommandValue);
                var count = sQuery.Receive();
                string[] info = sQuery.Store(count);
                if (info.Any())
                {
                    foreach (var result in info)
                    {
                        LogValue.Add($"{result}");
                    }
                }
                else
                {
                    LogValue.Add($"{CommandValue}");
                }
                CommandValue = string.Empty;
            }
        }

        public RconViewModel()
        {
            LogValue = new ObservableCollection<string> { AppResources.ResourceManager.GetString("RconSuccessfulConnectionMessage") };
        }
    }
}