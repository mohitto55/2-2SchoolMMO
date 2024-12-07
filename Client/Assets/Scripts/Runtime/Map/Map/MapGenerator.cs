using NUnit.Framework;
using Runtime.Map;
using UnityEngine;
using UnityEngine.Tilemaps;
using Runtime.Map;
using System.Collections.Generic;
public class MapGenerator : MonoBehaviour
{
    [SerializeField] Tilemap _tileMap;
    [SerializeField] SOTileData _soTileData;
    [SerializeField] string serilizeFileName;
    public void Awake()
    {
        Debug.Log(PathHelper.GetPojectParentFolder());
        List<TileBaseExporter> exporters = new List<TileBaseExporter>();
        exporters.Add(new TilePositionExporter());

        Dictionary<string, TileData> tileDataDic = new Dictionary<string, TileData>();

        foreach (TileData tileData in _soTileData.tiles)
        {
            if (tileData._tile != null)
                tileDataDic.Add(tileData._tile.name, tileData);
        }
        string folderPath = PathHelper.GetPojectParentFolder() + '/' + "Server/Data";
        TileMapSerializer.TileMapSerailize(_tileMap, tileDataDic, folderPath, serilizeFileName);
    }
}
