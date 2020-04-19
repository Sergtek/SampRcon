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
    [QueryProperty("IpServer", "ipServer")]
    [QueryProperty("PortServer", "portServer")]
    [QueryProperty("RconPassword", "rconPassword")]
    public class RconViewModel : BaseViewModel
    {
        private string _currentIpServer;
        private string _currentPortServer;
        private string _currentRconPassword;
        private ObservableCollection<string> _logValue;
        private string _commandValue;

        public string IpServer
        {
            set => CurrentIpServer = Uri.UnescapeDataString(value);
        }

        public string PortServer
        {
            set => CurrentPortServer = Uri.UnescapeDataString(value);
        }

        public string RconPassword
        {
            set => CurrentRconPassword = Uri.UnescapeDataString(value);
        }

        public string CurrentIpServer
        {
            get => _currentIpServer;
            set
            {
                _currentIpServer = value;
                RaiseOnPropertyChanged();
            }
        }

        public string CurrentPortServer
        {
            get => _currentPortServer;
            set
            {
                _currentPortServer = value;
                RaiseOnPropertyChanged();
            }
        }

        public string CurrentRconPassword
        {
            get => _currentRconPassword;
            set
            {
                _currentRconPassword = value;
                RaiseOnPropertyChanged();
            }
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
            if (Int32.TryParse(CurrentPortServer, out portInteger))
            {
                var sQuery = new RCONQuery(CurrentIpServer, portInteger, CurrentRconPassword);
                sQuery.Send(CommandValue);
                int count = sQuery.Receive();
                string[] info = sQuery.Store(count);
                if (info.Any())
                {
                    LogValue.Add($"{info.First()}");
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