using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class MapTileResponseHandler : PacketHandler<DtoChunk>
{
    public MapTileResponseHandler(object data, EHandleType type) : base(data, type)
    {

    }

    protected override void OnFailed(DtoChunk data)
    {

    }

    protected override void OnSuccess(DtoChunk data)
    {
        Chunk<Vector2> tileMapChunk = ChunkGenerator.Instance.CreateChunk(typeof(TileMapChunk).Name, data);
        MapGenerator.Instance.GenerateChunk(tileMapChunk, data);
    }
}