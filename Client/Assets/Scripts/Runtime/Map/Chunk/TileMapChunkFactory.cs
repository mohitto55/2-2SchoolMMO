using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

[System.Serializable]
public class TileMapChunkFactory : ChunkFactory<TileMapChunk>
{
    [SerializeField] private SOTileData _soTileData;
    [SerializeField] private Tilemap _tileMap;
    private Dictionary<string, TileData> _tileTable;

    public override Chunk<Vector2> Create(DtoChunk param)
    {
        if (_tileTable == null)
            _tileTable = _soTileData.GetTileTable();

        DtoTileChunk tileChunk = (DtoTileChunk)param;

        TileMapChunk tileMapChunk = new TileMapChunk();
        tileMapChunk.Map = _tileMap;
        tileMapChunk.Id = new UnityEngine.Vector2(param.chunkPosition.x, param.chunkPosition.y);

        for (int i = 0; i < tileChunk.tileCount; i++)
        {
            DtoTileData dtoTileData = tileChunk.dtoTiles[i];
            TileData tileData = GetTileData(dtoTileData.id);
            if (tileData == null)
                continue;
            tileMapChunk.Objects.Add(new ChunkTile()
            {
                position = new UnityEngine.Vector3Int((int)dtoTileData.x, (int)dtoTileData.y),
                tileBase = tileData._tile
            });
        }
        return tileMapChunk;
    }

    private TileData GetTileData(string id)
    {
        if (_tileTable.ContainsKey(id))
        {
            return _tileTable[id];
        }
        return null;
    }
}