public class World
{
    public World()
    {

    }


    Entity[] AllEntities => Entities.All;


    List<IComponentSystem> componentSystems = new List<IComponentSystem>();


    public void Tick()
    {
        foreach (var system in componentSystems)
        {
            system.Tick(this);
        }

    }

}