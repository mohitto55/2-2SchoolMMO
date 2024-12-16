using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class ChunkUpdateResponseHandler : PacketHandler<DtoTileChunk>
{
    public ChunkUpdateResponseHandler(object data, EHandleType type) : base(data, type)
    {

    }

    protected override void OnFailed(DtoTileChunk data)
    {

    }

    protected override void OnSuccess(DtoTileChunk data)
    {
        Chunk<Vector2> tileMapChunk = ChunkGenerator.Instance.CreateChunk(EMapObjectType.Tile, data);
        MapGenerator.Instance.GenerateChunk(tileMapChunk, data);
    }
}