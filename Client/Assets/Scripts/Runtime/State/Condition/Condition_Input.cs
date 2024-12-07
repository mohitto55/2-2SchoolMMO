using UnityEngine;
using UnityEngine.InputSystem;

public class Condition_InputAxis : Condition_Base<Vector3, InputAction>
{
    public Condition_InputAxis(InputAction target) : base(target)
    {

    }

    public override Vector3 GetCondition()
    {
        return Vector3.zero;
    }
}