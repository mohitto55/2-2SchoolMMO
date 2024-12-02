using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Player _target;

    Vector2 _axis;

    private void Update()
    {
        _axis.x = Input.GetAxis("Horizontal");
        _axis.y = Input.GetAxis("Vertical");
    }

}