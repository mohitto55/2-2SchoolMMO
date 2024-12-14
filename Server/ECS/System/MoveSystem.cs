using Arch.Core;

public class MoveSystem : ComponentSystem
{
    public override void Tick(World world, double dt)
    {
        var query = new QueryDescription().WithAll<VelocityComponent, PositionComponent>();

        world.Query(in query, (Entity entity,
            ref VelocityComponent velocity,
            ref PositionComponent position) =>
        {
            position.x += velocity.vx * (float)dt;
            position.y += velocity.vy * (float)dt;
        });
    }
}