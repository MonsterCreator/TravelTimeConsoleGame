using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TravelTime
{
    public static class ClientManager
    {
        private static string userName = ConfigurationManager.AppSettings["Username"];
        private static TcpClient tcpClient = new TcpClient();
        public static void ConnectToServer(string IP, int port)
        {
            try
            {
                tcpClient.Client.Connect(IPAddress.Parse(IP), port);
                Console.WriteLine("Подключение установлено! Ожидаем начала игры...");
                SendData(Encoding.UTF8.GetBytes(userName));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не удалось осуществить подключение. Ошибка: {ex.Message}");
            }
        }

        public static void SendData(byte[] data)
        {
            if (tcpClient.Connected)
            {
                var stream = tcpClient.GetStream();
                stream.Write(data, 0, data.Length);
                stream.Close();
            }

        }
    }
}
