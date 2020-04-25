using SampRcon.Models;

namespace SampRcon.Mappers
{
    internal static class ServerMapper
    {
        public static Server MapToModel(this Server server)
        {
            return new Server
            {
                ServerID = server.ServerID,
                IP = server.IP,
                Port = server.Port,
                Hostname = server.Hostname,
                Gamemode = server.Gamemode,
                Language = server.Language,
                Map = server.Map,
                MaxPlayers = server.MaxPlayers,
                Players = server.Players,
                Version = server.Version,
                Password = server.Password,
                RconPassword = string.Empty,
                Time = server.Time,
                WebURL = server.WebURL,
                Rank = server.Rank,
                AvgPlayers = server.AvgPlayers,
                HostedTab = server.HostedTab,
                LastUpdate = server.LastUpdate,
                TotalServers = server.TotalServers
            };
        }
    }
}