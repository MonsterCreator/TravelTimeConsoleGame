using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TravelTime
{
    public class Game
    {
        private TcpListener _server { get; set; }
        private List<Player> _playerList {  get; set; }
        private Map map { get; set; }
        int playerStep = 0;
        public void Start(List<Player> clients, TcpListener server)
        {
            _playerList = clients;
            _server = server;
            map = new Map(60);
            SetPlayersOnStart();
            NextMove();
        }

        private void SetPlayersOnStart() 
        {
            foreach (var player in _playerList)
            {
                map.Tiles[0].PlayersOnTile.Add(player);
            }
        }

        private void NextMove()
        {
            AwaitPlayerStep(_playerList[playerStep]);
            playerStep++;
            if (playerStep >= _playerList.Count) playerStep = 0;
        }

        private async void AwaitPlayerStep(Player player)
        {
            await SendStep(player);
            var stream = player.TcpClient.GetStream();
            byte[] buffer = new byte[1024];
            stream.Read(buffer,0,buffer.Length);
            string command = Encoding.UTF8.GetString(buffer, 0, buffer.Length);

        }

        private Task SendStep(Player player)
        {
            var stream = player.TcpClient.GetStream();
            byte[] buffer = new byte[1024];
            stream.Read(buffer, 0, buffer.Length);
            return Task.CompletedTask;
        }


        
    }

  
}
