using NUnit.Framework.Interfaces;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility.Data;

[CreateAssetMenu(fileName = "SOTileData", menuName = "Tile/TileData")]

public class SOTileData : ScriptableObject
{
    public List<TileData> tiles;

    public Dictionary<string, TileData> GetTileTable()
    {
        Dictionary<string, TileData> tileTable = new Dictionary<string, TileData>();

        foreach (TileData tileData in tiles)
        {
            if (tileData._tile != null)
                tileTable.Add(tileData._tile.name, tileData);
        }
        return tileTable;
    }
}

[System.Serializable]
public class TileData
{
    public Tile _tile;
    public bool _moveable = true;
}
