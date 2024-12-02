using System;
using System.Collections;


public static class Entities
{
    public const int MAX_ENTITIES_COUNT = 10000;
    static Entities()
    {
        for (int i = 0; i < MAX_ENTITIES_COUNT; i++)
        {
            _idPool.Enqueue(i);
        }
    }

    static Queue<int> _idPool = new Queue<int>();

    static int _lastIdx = -1;
    public static int LastIdx { get => _lastIdx; }

    public static Entity[] All { get => _entites; }
    static Entity[] _entites = new Entity[MAX_ENTITIES_COUNT];

    public static void DestroyEntity(Entity entity) => DestroyEntity(entity.Index);

    public static void DestroyEntity(int idx)
    {

        _entites[idx] = null;
        // 바꾸려는 인덱스가 마지막 인덱스와 같다면
        _entites[idx] = _entites[_lastIdx];
        // 객체를 하나 줄게한다.
        _lastIdx--;
    }
    
    public static Entity CreateEntity()
    {
        _lastIdx++;

        // 엔티티 생성시 마지막 번호에 인큐
        var entity = new Entity(_idPool.Dequeue());
        _entites[_lastIdx] = entity;

        return entity;
    }
}
public class Entity
{
    int _index;
    public int Index { get => _index;  }

    public Entity(int index)
    {
        _index = index;
    }


    public void AddComponent<T>() where T : IComponent
    {

    }


}