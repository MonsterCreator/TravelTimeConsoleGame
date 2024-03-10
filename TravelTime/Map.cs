using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelTime
{
    public class Map
    {
        public MapTile[] Tiles { get; private set; }
        public Map(int mapLenght)
        {
            Tiles = GenerateTilesArray(mapLenght);
        }

        private static MapTile[] GenerateTilesArray(int lenght)
        {
            MapTile[] mapTiles = new MapTile[lenght];
            for (int pos = 0; pos < mapTiles.Length; pos++)
            {
                mapTiles[pos] = GenerateTile(pos);
            }

            return mapTiles;
        }

        // Genetate one random tile
        private static MapTile GenerateTile(int pos)
        {
            int rndValue = new Random().Next(0, 10);

            MapTile mapTile;

            if(rndValue == 1) mapTile = new MapTileMove();
            else if(rndValue == 2) mapTile = new MapTileDouble();
            else if(rndValue == 3) mapTile = new MapTileBack();
            else  mapTile = new MapTile();

            mapTile.Position = pos;

            return mapTile;
        }
    }

    public class MapTile
    {
        public int Position { get; set; }
        public List<Player> PlayersOnTile { get; set; }
        public Player PlayerObj { get; set; }
    }

    public class MapTileMove : MapTile
    {
        public MapTileMove()
        {
            bool isPositive = Convert.ToBoolean(new Random().Next(0, 1));
            if(isPositive) TeleportTileValue = new Random().Next(1, 5);
            else TeleportTileValue = new Random().Next(1, 5) * -1;
        }
        public bool isPositive { get; set; }
        public int TeleportTileValue { get; set; }
    }

    public class MapTileDouble : MapTile
    {
        
    }

    public class MapTileBack : MapTile
    {

    }



}
