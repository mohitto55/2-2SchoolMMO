using UnityEngine;

public class Action_Move : IAction<Vector2>
{
    public void Execute(Vector2 target)
    {
        //NetworkManager.Instance.SendPacket(EHandleType.Transform, new DtoVector() { });
    }
}