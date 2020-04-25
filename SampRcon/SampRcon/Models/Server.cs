using SampRcon.ViewModels.Base;
using SQLite;

namespace SampRcon.Models
{
    public class Server : BaseViewModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string ServerID { get; set; }
        public string IP { get; set; }
        public string Port { get; set; }
        public string Hostname { get; set; }
        public string Gamemode { get; set; }
        public string Language { get; set; }
        public string Map { get; set; }
        public string MaxPlayers { get; set; }
        public string Players { get; set; }
        public string Version { get; set; }
        public string Password { get; set; }
        public string RconPassword { get; set; }
        public string Time { get; set; }
        public string WebURL { get; set; }
        public string Rank { get; set; }
        public string AvgPlayers { get; set; }
        public string HostedTab { get; set; }
        public string LastUpdate { get; set; }
        public string TotalServers { get; set; }
    }
}