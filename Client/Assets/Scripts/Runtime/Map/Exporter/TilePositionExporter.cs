using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Runtime.Map
{
    public class TilePositionExporter : TileBaseExporter
    {
        public override string DataType => "Position";

        public override string Export(TileBase data, Vector3Int param)
        {
            return $"{param.x}|{param.y}";
        }
    }
}