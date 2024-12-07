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
}

[System.Serializable]
public class TileData
{
    public Tile _tile;
    public bool _moveable = true;
}
