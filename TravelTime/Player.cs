using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TravelTime
{
    public class Player
    {
        public TcpClient TcpClient { get; private set; }
        public string Name { get; set; }
        private int _playerID { get; set; }
        private PColor _color {  get; set; }

        public Player(TcpClient tcpClient)
        {
            this.TcpClient = tcpClient;
        }

        private void GetData()
        {
            while (true)
            {
                var stream = TcpClient.GetStream();
                byte[] buffer = new byte[1024];
                stream.Read(buffer, 0, buffer.Length);

            }
        }


    }

    public enum PColor
    {
        Red,
        Green,
        Blue,
        Orange
    }
}
