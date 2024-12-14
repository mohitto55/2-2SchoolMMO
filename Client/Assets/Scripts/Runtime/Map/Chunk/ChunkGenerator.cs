using Runtime.BT.Singleton;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChunkGenerator : MonoSingleton<ChunkGenerator>
{
    [SerializeField] List<Chunk<Vector2>> chunks = new List<Chunk<Vector2>>();
    private Dictionary<string, ChunkFactory> _factoryTable;

    [SerializeField] TileMapChunkFactory _tileMapChunkFactory;
    protected void Awake()
    {
        _factoryTable = new Dictionary<string, ChunkFactory>();
        AddChunkFactory(_tileMapChunkFactory);
    }
    public Chunk<Vector2> CreateChunk(string id, DtoChunk chunk)
    {
        if (!_factoryTable.ContainsKey(id))
        {
            return null;
        }
        return _factoryTable[id].Create(chunk);
    }

    protected void AddChunkFactory(ChunkFactory factory)
    {
        string factoryId = factory.Id;
        if (!_factoryTable.ContainsKey(factoryId))
        {
            _factoryTable.Add(factoryId, factory);
        }
    }
}
