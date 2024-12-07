using UnityEngine;
using UnityEngine.Tilemaps;

namespace Runtime.Map
{
    public class TileIdExporter : TileBaseExporter
    {
        public override string DataType => "ID";

        public override string Export(TileBase data, Vector3Int param)
        {
            if (data != null)
            return data.name;
            return "";
        }

        public override void Import(Tilemap tilemap, TileBase tile, object param)
        {
            string name = (string)param;
            Debug.LogError(name);
        }
    }
}