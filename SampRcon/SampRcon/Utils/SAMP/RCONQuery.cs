using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace SampRcon.Utils.SAMP
{
    public class RCONQuery
    {
        Socket qSocket;
        IPAddress address;
        int _port = 0;
        string _password = null;

        string[] results = new string[50];
        int _count = 0;

        public RCONQuery(string IP, int port, string password)
        {
            qSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            qSocket.SendTimeout = 5000;
            qSocket.ReceiveTimeout = 5000;

            try
            {
                address = Dns.GetHostAddresses(IP)[0];
            }

            catch
            {

            }

            _port = port;
            _password = password;
        }

        public bool Send(string command)
        {
            try
            {
                IPEndPoint endpoint = new IPEndPoint(address, _port);

                using (MemoryStream stream = new MemoryStream())
                {
                    using (BinaryWriter writer = new BinaryWriter(stream))
                    {
                        writer.Write("SAMP".ToCharArray());

                        string[] SplitIP = address.ToString().Split('.');

                        writer.Write(Convert.ToByte(Convert.ToInt32(SplitIP[0])));
                        writer.Write(Convert.ToByte(Convert.ToInt32(SplitIP[1])));
                        writer.Write(Convert.ToByte(Convert.ToInt32(SplitIP[2])));
                        writer.Write(Convert.ToByte(Convert.ToInt32(SplitIP[3])));

                        writer.Write((ushort)_port);

                        writer.Write('x');

                        writer.Write((ushort)_password.Length);
                        writer.Write(_password.ToCharArray());

                        writer.Write((ushort)command.Length);
                        writer.Write(command.ToCharArray());
                    }

                    if (qSocket.SendTo(stream.ToArray(), endpoint) > 0)
                        return true;
                }
            }

            catch
            {
                return false;
            }

            return false;
        }

        public int Receive()
        {
            try
            {
                for (int i = 0; i < results.GetLength(0); i++)
                    results.SetValue(null, i);

                _count = 0;

                EndPoint endpoint = new IPEndPoint(address, _port);

                byte[] rBuffer = new byte[500];

                int count = qSocket.ReceiveFrom(rBuffer, ref endpoint);

                using (MemoryStream stream = new MemoryStream(rBuffer))
                {
                    using (BinaryReader reader = new BinaryReader(stream))
                    {
                        if (stream.Length <= 11)
                            return _count;

                        reader.ReadBytes(11);
                        short len;

                        try
                        {
                            while ((len = reader.ReadInt16()) != 0)
                                results[_count++] = new string(reader.ReadChars(Convert.ToInt32(len)));
                        }

                        catch
                        {
                            return _count;
                        }
                    }
                }
            }
            catch
            {
                return _count;
            }
            return _count;
        }

        public string[] Store(int count)
        {
            string[] rString = new string[count];

            for (int i = 0; i < count && i < _count; i++)
                rString[i] = results[i];

            _count = 0;

            return rString;
        }
    }
}