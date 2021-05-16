using System.Collections.Generic;

namespace Askalhorn.Common.GameWorld
{
    // class LocalWorld: IWorld
    // {
    //     private TiledMap map;
    //     private List<GameObject> objects = new List<GameObject>();
    //     private Player player;
    //     private BitMatrix freeCellMatrix;
    //
    //     public LocalWorld(TiledMap tiledMap)
    //     {
    //         this.map = tiledMap;
    //         player = new Player(2, 2, new PlayerInfo()
    //         {
    //             Texture = InfoManager.Content.Load<Texture2D>("images/player")
    //         });
    //         objects.Add(player);
    //         InitializeFreeCellMatrix();
    //     }
    //
    //     public IEnumerable<GameObject> GetGameObjects()
    //     {
    //         return objects;
    //     }
    //
    //     public Player GetPlayer()
    //     {
    //         return player;
    //     }
    //
    //     public TiledMap GetTiledMap()
    //     {
    //         return map;
    //     }
    //
    //     public bool IsFreeCell(int x, int y)
    //     {
    //         return freeCellMatrix[x, y];
    //     }
    //
    //     GameObject IWorld.GetGameObjectOnCell(int x, int y)
    //     {
    //         throw new System.NotImplementedException();
    //     }
    //
    //     private void InitializeFreeCellMatrix()
    //     {
    //         freeCellMatrix = new BitMatrix(map.Width, map.Height);
    //         var floors = map.GetLayer<TiledMapTileLayer>("Floor");
    //         var walls = map.GetLayer<TiledMapTileLayer>("WallLayer");
    //
    //         // непроходимая рамка по краю карты
    //         for (int i = 0; i < map.Width; i++)
    //         {
    //             freeCellMatrix[i, 0] = false;
    //             freeCellMatrix[i, map.Height - 1] = false;
    //         }
    //
    //         for (int i = 0; i < map.Height; i++)
    //         {
    //             freeCellMatrix[0, i] = false;
    //             freeCellMatrix[map.Width - 1, i] = false;
    //         }
    //
    //         //for (ushort x = 1; x < map.Width - 1; x++)
    //         //    for (ushort y = 1; y < map.Height - 1; y++)
    //         //        freeCellMatrix[x, y] =
    //         //            !floors.GetTile(x, y).IsBlank &&
    //         //            walls.GetTile((ushort)(x - 1), (ushort)(y - 1)).IsBlank;
    //     }
    // }
}
