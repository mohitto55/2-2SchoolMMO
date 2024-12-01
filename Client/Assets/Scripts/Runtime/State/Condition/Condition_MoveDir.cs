using UnityEngine;

public class Condition_MoveRigidDir : Condition_Base<Vector3, Rigidbody>
{
    public Condition_MoveRigidDir(Rigidbody target) : base(target)
    {
    }

    public override Vector3 GetCondition()
    {
        return _target.linearVelocity.normalized;
    }
}
