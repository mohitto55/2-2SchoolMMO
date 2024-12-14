using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using Runtime.BT.Singleton;
using System;

public class MapGenerator : MonoSingleton<MapGenerator>
{
    [SerializeField] Character _character;
    [SerializeField] Tilemap _tileMap;
    [SerializeField] SOTileData _soTileData;
    [SerializeField] string _mapName;

    [SerializeField] int chunkSurroundDst = 10;
    private int _chunkSize = 4;

    Dictionary<string, TileData> _tileTable;
    Dictionary<Vector2, DtoChunk> mapChunk = new Dictionary<Vector2, DtoChunk>();

    [SerializeField] Transform testPosition;
    private void Awake()
    {
        _tileTable = _soTileData.GetTileTable();
    }

    float duration = 1;

    public void Update()
    {

        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            RequestMapChunk();
            duration = 1;
        }
        ChunkUpdate();
    }
    public void RequestMapChunk()
    {
        DtoChunkRequest chunkRequest = new DtoChunkRequest();
        chunkRequest.mapName = _mapName;
        chunkRequest.position = new DtoVector() { x = testPosition.position.x, y = testPosition.position.y };
        chunkRequest.surroundDst = chunkSurroundDst;

        NetworkManager.Instance.SendPacket(EHandleType.MapTileRequest, chunkRequest);
    }

    void ChunkUpdate()
    {
        List<Vector2> removeChunk = new List<Vector2>();
        foreach (var kv in mapChunk)
        {
            DtoVector centerVector = new DtoVector() { x = testPosition.position.x, y = testPosition.position.y };
            DtoChunk chunk = kv.Value;
            Debug.DrawLine(testPosition.position, new Vector3(chunk.chunkID.x, chunk.chunkID.y));
            if(!MapUtility.IsChunkInLoadChunk(centerVector, chunk, chunkSurroundDst))
            {
                Debug.Log(chunkSurroundDst);
                for (int i = 0; i < chunk.tileCount; i++)
                {
                    DtoTileData tileData = chunk.dtoTiles[i];
                    Vector3Int tilePos = new Vector3Int((int)tileData.x, (int)tileData.y);
                    _tileMap.SetTile(tilePos, null);
                }
                removeChunk.Add(kv.Key);
            }
        }
        foreach (var vec in removeChunk)
        {
            if(mapChunk.ContainsKey(vec))
            mapChunk.Remove(vec);
        }
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
