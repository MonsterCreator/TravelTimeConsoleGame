using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TravelTime
{
    public class ServerManager
    {
        private TcpListener _tcpListener;
        private readonly List<Player> _connectedClients = new List<Player>();

        public ServerManager(IPAddress ipAddress, int port)
        {
            _tcpListener = new TcpListener(ipAddress, port);
        }

        public async Task StartAsync()
        {
            _tcpListener.Start();
            Console.WriteLine("Сервер запущен успешно. Ожидаем подключений...");

            // Запускаем бесконечный цикл для обработки подключений
            new Thread(() => AcceptClientsAsync()).Start();
            Console.WriteLine("/start - запустить игру");
            Console.WriteLine("/close - закрыть лобби");
            // Запускаем бесконечный цикл для обработки команд
            CommandLoopAsync();
            //выходим из метода если в процессе сервер был закрыт
            return;
        }

        private async void AcceptClientsAsync()
        {
            while (true)
            {
                try
                {
                    TcpClient tcpClient = _tcpListener.AcceptTcpClient();
                    Player client = new Player(tcpClient);
                    var stream = client.TcpClient.GetStream();
                    byte[] buffer = new byte[512];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string username = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    client.Name = username;
                    _connectedClients.Add(client);
                    Console.WriteLine($"К серверу подключился пользователь с именем {client.Name}");

                    // Обработка подключения в отдельном потоке
                    _ = HandleClientAsync(client);
                }
                catch (Exception)
                {
                }
            }
        }

        private void CommandLoopAsync()
        {
            while (true)
            {
                string command = Console.ReadLine();
                switch (command)
                {
                    case "/start": StartGame(); break;
                    case "/close": CloseGame(); return;
                }
            }
        }

        private async Task HandleClientAsync(Player client)
        {
            try
            {
                // Ваш код обработки клиента
                // Например, чтение и запись данных с клиента
                // ...

                // При разрыве подключения удаляем клиента из списка
                client.TcpClient.GetStream().ReadTimeout = 5000; // Установите таймаут чтения
                await client.TcpClient.GetStream().ReadAsync(new byte[1], 0, 1); // Чтение для проверки подключения
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обработке клиента: {ex.Message}");
            }
            finally
            {
                _connectedClients.Remove(client);
                Console.WriteLine($"Пользователь {client.Name} отключился");
                client.TcpClient.Close();
            }
        }

        private void StartGame()
        {
            Console.WriteLine("Запуск игры");
            Game game = new Game();
            game.Start(_connectedClients, _tcpListener);
        }
        private void CloseGame()
        {
            foreach (var client in _connectedClients)
            {
                client.TcpClient.Close();
            }
            _tcpListener.Stop();
            _connectedClients.Clear();
            Console.WriteLine("Сервер закрыт");
        }
    }
}
