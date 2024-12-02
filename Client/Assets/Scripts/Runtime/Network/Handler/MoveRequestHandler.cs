using UnityEngine;

public class MoveRequestHandler : PacketHandler<DtoVector>
{
    public MoveRequestHandler(object data, EHandleType type) : base(data, type)
    {
    }

    protected override void OnFailed(DtoVector data)
    {
    }

    protected override void OnSuccess(DtoVector data)
    {
    }
}
