using UnityEngine;

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
        DtoMessage message = new DtoMessage();
        NetworkManager.Instance.SendPacket(EHandleType.PlayerInteraction, message);
    }
}
