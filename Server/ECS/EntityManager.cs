using Arch.Core;
using Arch.Core.Extensions;
using Server.Debug;
using System.Diagnostics;
using System.Reflection;


public class EntityManager
{
    World _world;
    public World world { get => _world; }

    ComponentSystem?[] componentSystem;

    Stopwatch _dtWatch;
    double _dt;
    DateTime time1 = DateTime.Now;
    DateTime time2 = DateTime.Now;


    public void Init()
    {
        _world = World.Create();

        componentSystem = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsSubclassOf(typeof(ComponentSystem)) && !t.IsAbstract)
            .Select(t => Activator.CreateInstance(t) as ComponentSystem).ToArray();
    }

    public bool Tick()
    {
        time2 = DateTime.Now;
        _dt = (time2.Ticks - time1.Ticks) / 10000000f;
        time1 = time2;

        foreach (var item in componentSystem)
        {
            item?.Tick(_world, _dt);
        }

        return true;
    }


}
