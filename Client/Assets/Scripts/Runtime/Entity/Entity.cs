using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private string _objectId;
    public string ObjectId => _objectId;
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