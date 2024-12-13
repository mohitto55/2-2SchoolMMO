using System.Linq;

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
    }
}