using Arch.Core;
using Server.Debug;

/// <summary>
/// 일정 시간마다 위치 패킷을 뿌립니다.
/// </summary>
public class TransformPacketSenderSystem : ComponentSystem
{
    public override void Tick(World world, double dt)
    {
        var query = new QueryDescription().WithAll<PacketSendTimer, PositionComponent, VelocityComponent>();

        world.Query(in query,(Entity entity,
            ref PacketSendTimer sendTimer,
            ref PositionComponent position,
            ref VelocityComponent velocity) =>
        {
            sendTimer.elasedTime += (float)dt;
            if(sendTimer.sendTime <= sendTimer.elasedTime)
            {
                sendTimer.elasedTime = 0;

                ServerDebug.Log(LogType.Log, $"Transform Packet 전송 {position.x} , {position.y}");

                IOCPServer.SendAllClient(EHandleType.Transform, new DtoTransform()
                {
                    entityID = entity.Id,
                    dtoPosition = new DtoVector()
                    {
                        x = position.x, y = position.y
                    },
                    dtoVelocity = new DtoVector()
                    {
                        x = velocity.vx,
                        y = velocity.vy
                    }
                });
            }
        });
    }
}