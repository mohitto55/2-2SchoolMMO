using UnityEngine;

public class TransformHandler : PacketHandler<DtoTransform>
{
    public TransformHandler(object data, EHandleType type) : base(data, type)
    {
    }

    protected override void OnFailed(DtoTransform data)
    {
    }

    protected override void OnSuccess(DtoTransform data)
    {
        ObjectManager.Instance.SetTransformEntityData(data.entityID, data);
    }
}
