using Arch.Core;
using Server.Debug;
using Windows.Services.Maps;

public static class ObjectManager
{
    static EntityManager _entityMgr;

    static World _world => _entityMgr.world;

    static Dictionary<int, GameObject> _gameObjects = new();


    public static void Init()
    {
        _entityMgr = new EntityManager();
        _entityMgr.Init();
    }

    public static bool Run()
    {
        return _entityMgr.Tick();
    }

    public static GameObject CreatePlayer()
    {
        var player = new Character(_world.Create(
            new PositionComponent(0,0),
            new VelocityComponent(0, 0),
            new PacketSendTimer(0.05, 0),
            new BoundColliderComponent(1,1, false, new List<Entity>())));
        _gameObjects.Add(player.entity.Id, player);
        return player;
    }
    public static void DestroyGameObject(int ID)
    {
        _world.Destroy(_gameObjects[ID].entity);
        _gameObjects.Remove(ID);
    }




}