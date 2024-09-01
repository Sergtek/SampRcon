namespace SampRcon.Models.Mappers
{
    internal static class ServerMapper
    {
        public static Server MapToModel(this ServerResponse server)
        {
            return new Server
            {
                IP = server.Ip,
                Port = server.Port,
                Hostname = server.Hn,
                Gamemode = server.Gm,
                Language = server.La,
                Version = server.Vn
            };
        }
    }
}