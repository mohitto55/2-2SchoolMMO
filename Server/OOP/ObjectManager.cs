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
            new BoundColliderComponent(1,1, false, new List<Entity>()),
            new InteractionComponent(EEntityType.Player, 0.ToString(), false, true, 0.1f)));
        _gameObjects.Add(player.entity.Id, player);
        return player;
    }

    public static GameObject CreateNPC(DtoVector position, int npcUid)
    {
        var npc = new NPC(_world.Create(
            new PositionComponent(position.x, position.y),
            new VelocityComponent(0, 0),
            new PacketSendTimer(0.05, 0),
            new BoundColliderComponent(1, 1, true, new List<Entity>()),
            new InteractionComponent(EEntityType.Npc, npcUid.ToString())));
        _gameObjects.Add(npc.entity.Id, npc);
        return npc;
    }
    public static void DestroyGameObject(int ID)
    {
        _world.Destroy(_gameObjects[ID].entity);
        _gameObjects.Remove(ID);
    }

    public static GameObject GetGameObject(int ID)
    {
        if (_gameObjects.ContainsKey(ID))
            return _gameObjects[ID];
        return null;
    }



}