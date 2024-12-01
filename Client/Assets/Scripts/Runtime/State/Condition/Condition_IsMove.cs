using UnityEngine;

public class Condition_IsRigidMove : Condition_Base<bool, Rigidbody>
{
    public Condition_IsRigidMove(Rigidbody target) : base(target)
    {
    }

    public override bool GetCondition()
    {
        return _target.linearVelocity.magnitude > 0;
    }
}
