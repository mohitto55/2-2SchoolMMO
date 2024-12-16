using Runtime.BT.Singleton;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class ChunkGenerator : SerializedMonoSingleton<ChunkGenerator>
{
    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout), SerializeField]
    private Dictionary<EMapObjectType, ChunkFactory> _factoryTable = new Dictionary<EMapObjectType, ChunkFactory>();


    public Chunk<Vector2> CreateChunk(EMapObjectType id, DtoChunk chunk)
    {
        if (!_factoryTable.ContainsKey(id))
        {
            return null;
        }
        return _factoryTable[id].Create(chunk);
    }
}
