using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility.Serilize;

namespace Runtime.Map
{
    public abstract class TileBaseExporter : DataExporter<TileBase, Vector3Int>
    {
        public virtual TileBase Import(Tilemap tilemap, Dictionary<string, Tile> tileDic, TileBase tile, object param) { return null;  }
    }
}
