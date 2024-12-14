using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

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
    }
}