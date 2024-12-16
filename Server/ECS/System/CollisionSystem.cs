using Arch.Core;

public class CollisionSystem : ComponentSystem
{
    public override void Tick(World world, double dt)
    {
        var contacts = new List<Entity>();
        var query = new QueryDescription().WithAll<BoundColliderComponent, PositionComponent>();


        world.Query(in query, (Entity entityA,
         ref BoundColliderComponent boundA,
         ref PositionComponent positionA) =>
        {
            
            var aMinX = positionA.x - (boundA.w / 2.0f);
            var aMaxX = positionA.x + (boundA.w / 2.0f);
            var aMinY = positionA.y - (boundA.h / 2.0f);
            var aMaxY = positionA.y + (boundA.h / 2.0f);

            world.Query(in query, (Entity entityB,
            ref BoundColliderComponent boundB,
            ref PositionComponent positionB) =>
            {
                var bMinX = positionB.x - (boundB.w / 2.0f);
                var bMaxX = positionB.x + (boundB.w / 2.0f);
                var bMinY = positionB.y - (boundB.h / 2.0f);
                var bMaxY = positionB.y + (boundB.h / 2.0f);

                if(IsCollision(aMinX, aMinY, aMaxX, aMaxY, bMinX, bMinY, bMaxX, bMaxY))
                {
                    if (entityA != entityB)
                        contacts.Add(entityB);
                }

            });
            boundA.contactsEntity = contacts;
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