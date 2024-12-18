using Arch.Core.Extensions;
using Server.Debug;

public class PlayerInteractionHandler : PacketHandler<DtoMessage>
{
    public PlayerInteractionHandler(object data, EHandleType type) : base(data, type)
    {
    }

    protected override void OnFailed(DtoMessage data)
    {
    }

    protected override void OnSuccess(DtoMessage data)
    {
        var client = IOCPServer.GetClient(m_id);
        var entity = client.characterObject.entity;
        ref InteractionComponent comps = ref entity.Get<InteractionComponent>();
        comps.actionID = m_id;
        comps.canAction = true;
        comps.lastDisableTimer = comps.disableTimer;
    }
}