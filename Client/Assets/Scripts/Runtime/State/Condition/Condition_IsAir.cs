using UnityEngine;

public class Condition_IsAir : Condition_Base<bool, Transform>
{
    public Condition_IsAir(Transform target) : base(target)
    {
    }

    public override bool GetCondition()
    {
        return true;
    }
}
