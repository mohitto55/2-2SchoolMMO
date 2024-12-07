using UnityEngine;
using UnityEngine.Tilemaps;
using Utility.Serilize;

namespace Runtime.Map
{
    public abstract class TileBaseExporter : DataExporter<TileBase, Vector3Int>
    {
        public virtual void Import(Tilemap tilemap, TileBase tile, object param) { }
    }
}
