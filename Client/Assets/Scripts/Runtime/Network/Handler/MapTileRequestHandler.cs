public class MapTileRequestHandler : PacketHandler<DtoMessage>
{
    public MapTileRequestHandler(object data, EHandleType type) : base(data, type)
    {
    }

    protected override void OnFailed(DtoMessage data)
    {

    }

    protected override void OnSuccess(DtoMessage data)
    {
        
    }
}