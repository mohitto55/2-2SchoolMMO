using NUnit.Framework;
using Runtime.Map;
using UnityEngine;
using UnityEngine.Tilemaps;
using Runtime.Map;
using System.Collections.Generic;
public class MapGenerator : MonoBehaviour
{
    [SerializeField] Tilemap _tileMap;
    [SerializeField] string serilizeFileName;
    public void Awake()
    {
        List<TileBaseExporter> exporters = new List<TileBaseExporter>();
        exporters.Add(new TilePositionExporter());
        TileMapSerializer.TileMapSerailize(_tileMap, serilizeFileName, new TileIdExporter(), exporters);
    }
}
