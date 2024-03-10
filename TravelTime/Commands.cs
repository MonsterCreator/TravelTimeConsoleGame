using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TravelTime
{
    public static class Commands
    {
        

        public static void GetCommand()
        {
            while (true)
            {
                string command = Console.ReadLine();

                
                switch (command)
                {
                    case "-help": PrintCommandList(); break;
                    case "-create": CreateRoom(); break;
                    case "-connect": ConnectRoom(); break;
                    default: Console.WriteLine("Введена несуществующая команда"); break;
                        
                }
            }
        }

        public static void PrintCommandList()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Список комманд:");
            Console.WriteLine("-help -> Вывести список доступных команд");
            Console.WriteLine("-create -> Создать сервер для игры. Ваш компьютер выступает в качестве сервера.");
            Console.WriteLine("-connect -> Подключиться к удалённому серверу. Требуется IP адрес сервера и порт.");
            Console.WriteLine("-about -> Получить информацию о данной программе (игре).");
            Console.WriteLine("-settings -> некоторые параметры которые вы можете настроить. Например ваш ник");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void CreateRoom()
        {
            Console.WriteLine("Создаём комнату. Пожалуйста введите порт на котором хотите создать сервер.");
            int port;
            while (true)
            {
                try
                {
                    port = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Введены некорректные данные попробуйте ещё раз");
                }
            }
            string externalIp = new WebClient().DownloadString("http://icanhazip.com").Replace("\n", ""); ;
            
            Console.WriteLine($"Создаём сервер. IP:{IPAddress.Any.Address} Порт:{port}");
            Console.WriteLine("Сообщите IP и порт своим друзьям, чтобы они смогли подключиться к серверу");
            ServerManager server = new ServerManager(IPAddress.Any, port);
            server.StartAsync();
        }

        public static void ConnectRoom()
        {
            Console.WriteLine("Подключаемся к комнате. Введите порт.");
            int port;
            while (true)
            {
                try
                {
                    port = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Введены некорректные данные попробуйте ещё раз");
                }
            }
            Console.WriteLine("Введите IP сервера");
            string externalIp;
            while (true)
            {
                try
                {
                    externalIp = Console.ReadLine();
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Введены некорректные данные попробуйте ещё раз");
                }
            }

            Console.WriteLine($"Пробуем подключиться к серверу... IP:{externalIp} Порт:{port}");
            ClientManager.ConnectToServer(externalIp, port);
        }


    }
}
