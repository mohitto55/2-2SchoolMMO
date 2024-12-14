using System.Linq;

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
    }
}