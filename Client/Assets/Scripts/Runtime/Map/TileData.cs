using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility.Data;

[System.Serializable]
public class TileSerializeData : StaticData
{
    public string id;
    public Vector3Int position;
    public bool moveable;

    public TileSerializeData() { }
    public TileSerializeData(TileData tileData)
    {
        if (tileData == null)
            return;

        id = tileData?._tile.name;
        moveable = tileData._moveable;
    }

    public void SetTileData(TileData tileData)
    {
        id = tileData?._tile.name;
        moveable = tileData._moveable;
    }
}

[System.Serializable]
public class TileDataList
{
    public List<string> tiles;

    public TileDataList(List<string> tiles)
    {
        this.tiles = tiles;
    }
}