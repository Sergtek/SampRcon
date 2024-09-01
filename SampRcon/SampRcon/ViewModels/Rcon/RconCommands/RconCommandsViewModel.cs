using SampRcon.Resources;
using SampRcon.Utils.SAMP;
using SampRcon.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;


namespace SampRcon.ViewModels.Rcon.RconCommands
{
    public class RconCommandsViewModel : ServerBaseViewModel
    {
        private ObservableCollection<string> _logValue;
        private ObservableCollection<string> _cmdlist = new ObservableCollection<string>
        {
            "echo",
            "exec",
            "cmdlist",
            "varlist",
            "exit",
            "kick",
            "ban",
            "gmx",
            "changemode",
            "say",
            "reloadbans",
            "reloadlog",
            "players",
            "banip",
            "unbanip",
            "gravity",
            "weather",
            "loadfs",
            "unloadfs",
            "reloadfs"
        };
        private string _commandValue;

        public ObservableCollection<string> LogValue
        {
            get => _logValue;
            set
            {
                _logValue = value;
                RaiseOnPropertyChanged();
            }
        }

        public ObservableCollection<string> Cmdlist
        {
            get => _cmdlist;
            set
            {
                _cmdlist = value;
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

                    switch (CommandValue)
                    {
                        case "cmdlist":
                            foreach (var cmd in Cmdlist)
                            {
                                LogValue.Add(cmd);
                            }
                            break;
                    }
                }
                else
                {
                    LogValue.Add($"{CommandValue}");
                }
                CommandValue = string.Empty;
            }
        }

        public RconCommandsViewModel()
        {
            LogValue = new ObservableCollection<string> { AppResources.ResourceManager.GetString("RconSuccessfulConnectionMessage") };
        }
    }
}