using Arch.Core;

/// <summary>
/// 일정 시간마다 위치 패킷을 뿌립니다.
/// </summary>
public class PositionPacketSenderSystem : ComponentSystem
{
    public void Tick(World world, float dt)
    {
        var query = new QueryDescription().WithAll<PacketSendTimer, PositionComponent>();

        world.Query(in query,(ref PacketSendTimer sendTimer, ref PositionComponent timer) =>
        {
            




        });
    }
}