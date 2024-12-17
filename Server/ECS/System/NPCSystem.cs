using Arch.Core;
using Arch.Core.Extensions;
using Server.Debug;

public class NPCSystem : ComponentSystem
{
    public override void Tick(World world, double dt)
    {
        var contacts = new List<Entity>();
        var query = new QueryDescription().WithAll<BoundColliderComponent, PositionComponent, NPCComponent, EntityTypeComponent>();

        world.Query(in query, (Entity entityA,
         ref BoundColliderComponent boundA,
         ref PositionComponent positionA) =>
        {
            if (boundA.contactsEntity.Count <= 0)
                return;

            for (int i = 0; i < boundA.contactsEntity.Count; i++)
                ServerDebug.Log(LogType.Log, "콘택트 " + boundA.contactsEntity[i].Id.ToString());
        });
    }
    // 충돌 감지 메서드
    public static bool IsCollision(float aMinX, float aMinY, float aMaxX, float aMaxY,
        float bMinX, float bMinY, float bMaxX, float bMaxY)
    {
        // 두 박스가 겹치는지 확인
        return aMinX < bMaxX &&
               aMaxX > bMinX &&
               aMinY < bMaxY &&
               aMaxY > bMinY;
    }

}