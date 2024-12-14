using Runtime.Map;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapSerializer : MonoBehaviour
{
    [SerializeField] Tilemap _tileMap;
    [SerializeField] SOTileData _soTileData;
    [SerializeField] string serilizeFileName;

    [Button]
    public void GenerateMapJsonData()
    {
        Debug.Log(PathHelper.GetPojectParentFolder());
        List<TileBaseExporter> exporters = new List<TileBaseExporter>();
        exporters.Add(new TilePositionExporter());

        Dictionary<string, TileData> tileDataDic = _soTileData.GetTileTable();
        string folderPath = PathHelper.GetPojectParentFolder() + '/' + "Server/Data";
        TileMapSerializer.TileMapSerailize(_tileMap, tileDataDic, folderPath, serilizeFileName);

        folderPath = PathHelper.GetFolderPath("Json");
        TileMapSerializer.TileMapSerailize(_tileMap, tileDataDic, folderPath, serilizeFileName);
    }
}
