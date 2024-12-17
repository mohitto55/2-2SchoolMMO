using Runtime.BT.Singleton;
using System;
using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController>
{
    [SerializeField]
    Character _target;

    [SerializeField]
    Vector2 _fixedAxis;
    [SerializeField]
    Vector2 _axis;

    public Action<Character> OnSetTarget;
    public void SetTarget(Character target)
    {
        _target = target;
        OnSetTarget(target);
    }

    private void Update()
    {
        if (_target != null)
        {
            var vec = _target.transform.position;
            vec.z = -10;
            Camera.main.transform.position = vec;
        }
        _axis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * 5;



    }
    private void FixedUpdate()
    {
        if (_target == null) return;

        if (_axis != _fixedAxis)
        {
            _fixedAxis = _axis ;

            NetworkManager.Instance.SendPacket(EHandleType.Transform, new DtoTransform()
            {
                dtoVelocity = new DtoVector() { x = _fixedAxis.x, y = _fixedAxis.y }
            });

            _target.directVelocity = _fixedAxis.normalized;
        }
    }

}