using UnityEngine;

public class Condition_LookDir : Condition_Base<Vector3, Transform>
{
    public Condition_LookDir(Transform target) : base(target)
    {
    }

    public override Vector3 GetCondition()
    {
        return _target.forward;
    }

    
}