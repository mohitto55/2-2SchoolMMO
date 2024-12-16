using Arch.Core;
using Server.Debug;

public class MoveSystem : ComponentSystem
{
    public override void Tick(World world, double dt)
    {
        var query = new QueryDescription().WithAll<VelocityComponent, PositionComponent, BoundColliderComponent>();

        world.Query(in query, (Entity entity,
            ref VelocityComponent velocity,
            ref PositionComponent position,
            ref BoundColliderComponent bounds) =>
        {

            var aMinX = position.x + velocity.vx * (float)dt - (bounds.w / 2.0f);
            var aMaxX = position.x + velocity.vx * (float)dt + (bounds.w / 2.0f);
            var aMinY = position.y - velocity.vy * (float)dt - (bounds.h / 2.0f);
            var aMaxY = position.y + velocity.vy * (float)dt + (bounds.h / 2.0f);

            bool trigger = false;

            world.Query(in query, (Entity entityB,
            ref BoundColliderComponent boundB,
            ref PositionComponent positionB) =>
            {
                if (entity != entityB)
                {
                    var bMinX = positionB.x - (boundB.w / 2.0f);
                    var bMaxX = positionB.x + (boundB.w / 2.0f);
                    var bMinY = positionB.y - (boundB.h / 2.0f);
                    var bMaxY = positionB.y + (boundB.h / 2.0f);

                    if (CollisionSystem.IsCollision(aMinX, aMinY, aMaxX, aMaxY, bMinX, bMinY, bMaxX, bMaxY))
                    {
                        if (!boundB.isTrigger)
                        {
                            ServerDebug.Log(LogType.Log, $"객체 충돌 {entity.Id} , {entityB.Id}");
                            trigger = true;
                            return;
                        }
                    }
                }
            });

            if(!trigger)
            {
                position.x += velocity.vx * (float)dt;
                position.y += velocity.vy * (float)dt;
            }
        });
    }
}