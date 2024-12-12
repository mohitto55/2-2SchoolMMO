using NUnit.Framework;
using Runtime.Map;
using UnityEngine;
using UnityEngine.Tilemaps;
using Runtime.Map;
using System.Collections.Generic;
using Runtime.BT.Singleton;
using UnityEngine.UIElements;
public class MapGenerator : MonoSingleton<MapGenerator>
{
    [SerializeField] Tilemap _tileMap;
    [SerializeField] SOTileData _soTileData;
    [SerializeField] string _mapName;

    Dictionary<string, TileData> _tileTable;
    private void Awake()
    {
        _tileTable = _soTileData.GetTileTable();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("요청");
            RequestMapData();
        }
    }
    public void RequestMapData()
    {
        DtoMessage message = new DtoMessage();
        message.message = _mapName;
        NetworkManager.Instance.SendPacket(EHandleType.MapTileRequest, message);
    }

    public void GenerateMap(List<DtoTileData> tiles)
    {
        Debug.LogWarning("리스트 카운트 : " + tiles.Count);

        foreach (var tile in tiles)
        {
            string id = tile.id;
            Debug.LogWarning(id);
            if (_tileTable.ContainsKey(id))
            {
                TileData tileData = _tileTable[id];
                Vector3Int position = new Vector3Int((int)tile.x, (int)tile.y, 0);
                Debug.LogWarning(id + " " + position);

                _tileMap.SetTile(position, tileData._tile);
            }
            else
            {
                Debug.LogWarning(id + "타일은 존재하지 않습니다.");
            }
        }
    }
}
