using Arch.Core.Extensions;

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
        var client = IOCPServer.GetClient(m_id);
        var entity = client.characterObject.entity;
        ref VelocityComponent comps = ref entity.Get<VelocityComponent>();
        ref PositionComponent posComps = ref entity.Get<PositionComponent>();

        float scala = (data.dtoVelocity.x * data.dtoVelocity.x) + (data.dtoVelocity.y * data.dtoVelocity.y);

        if(scala == 0)
        {
            comps.vx = 0;
            comps.vy = 0;
            data.dtoVelocity.x = 0;
            data.dtoVelocity.y = 0;

            data.dtoPosition.x = posComps.x;
            data.dtoPosition.y = posComps.y;

            client.SendPacket(EHandleType.Transform, data);
        }
        else
        {
            comps.vx = data.dtoVelocity.x / (float)Math.Sqrt((double)scala) * 5;
            comps.vy = data.dtoVelocity.y / (float)Math.Sqrt((double)scala) * 5;

        }

    }
}