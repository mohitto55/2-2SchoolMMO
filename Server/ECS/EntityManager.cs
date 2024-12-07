using Arch.Core;
using System;


public class EntityManager
{
    World _world;

    public EntityManager()
    {
        _world = World.Create();
        var entity = _world.Create<PositionComponent, VelocityComponent>()
    }


}
