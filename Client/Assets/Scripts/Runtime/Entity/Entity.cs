using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private int _objectId;
    [SerializeField] private EEntityType _entityType;
    public int ObjectId => _objectId;
    public EEntityType EntityType => _entityType;

    private int index;
    
    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {

    }
    
}