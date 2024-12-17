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
                ServerDebug.Log(LogType.Log, "����Ʈ " + boundA.contactsEntity[i].Id.ToString());
        });
    }
    // �浹 ���� �޼���
    public static bool IsCollision(float aMinX, float aMinY, float aMaxX, float aMaxY,
        float bMinX, float bMinY, float bMaxX, float bMaxY)
    {
        // �� �ڽ��� ��ġ���� Ȯ��
        return aMinX < bMaxX &&
               aMaxX > bMinX &&
               aMinY < bMaxY &&
               aMaxY > bMinY;
    }

}