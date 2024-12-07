//public class World
//{
//    public World()
//    {

//    }


//    Entity[] AllEntities => Entities.All;


//    List<IComponentSystem> componentSystems = new List<IComponentSystem>();

//    public PositionComponent[] positionComponents = new PositionComponent[Entities.MAX_ENTITIES_COUNT];
//    public VelocityComponent[] velocityComponents = new VelocityComponent[Entities.MAX_ENTITIES_COUNT];

//    public void Tick()
//    {
//        foreach (var system in componentSystems)
//        {
//            system.Tick(this);
//        }

//    }

//}