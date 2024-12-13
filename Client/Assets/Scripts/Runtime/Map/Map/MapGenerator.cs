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
    [SerializeField] Character _character;
    [SerializeField] Tilemap _tileMap;
    [SerializeField] SOTileData _soTileData;
    [SerializeField] string _mapName;

    [SerializeField] int chunkSurroundDst = 1;

    Dictionary<string, TileData> _tileTable;
    Dictionary<Vector2, DtoChunk> mapChunk = new Dictionary<Vector2, DtoChunk>();
    private void Awake()
    {
        _tileTable = _soTileData.GetTileTable();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("요청");
            RequestMapChunk();
        }
    }
    public void RequestMapChunk()
    {
        DtoChunkRequest chunkRequest = new DtoChunkRequest();
        chunkRequest.mapName = _mapName;
        chunkRequest.position = new DtoVector() { x = _character.transform.position.x, y = _character.transform.position.y };
        chunkRequest.surroundDst = chunkSurroundDst;

        NetworkManager.Instance.SendPacket(EHandleType.MapTileRequest, chunkRequest);
    }

    void ChunkUpdate()
    {

    }
    public void GenerateMap(DtoChunk chunk)
    {
        if (chunk == null)
        {
            Debug.LogWarning("NULL");

            return;
        }
        Vector2 position = new Vector2(chunk.chunkID.x, chunk.chunkID.y);
        if (!mapChunk.ContainsKey(position))
        {
            mapChunk.Add(position, chunk);
        }

        for (int i = 0; i < chunk.tileCount; i++)
        {
            DtoTileData tileData = chunk.dtoTiles[i];
            if (!_tileTable.ContainsKey(tileData.id))
            {
                Debug.LogWarning(tileData.id + "타일은 존재하지 않습니다." + tileData.x + " " + tileData.y);
                continue;
            }

            Vector3Int tilePos = new Vector3Int((int)tileData.x, (int)tileData.y);
            TileData tileBase = _tileTable[tileData.id];

            _tileMap.SetTile(tilePos, tileBase._tile);
        }
    }
}
