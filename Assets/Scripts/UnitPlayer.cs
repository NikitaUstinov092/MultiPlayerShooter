using Sirenix.OdinInspector;
using UnityEngine;

public class UnitPlayer : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2;

    [SerializeField] 
    private Rigidbody _rigidbody;
    private float _hor, _vert;
    private void FixedUpdate()
    {
        var playerTransform = transform;
        var velocity = (playerTransform.forward * _vert + playerTransform.right * _hor).normalized * _speed;
        _rigidbody.velocity = velocity;
    }

   
    public void SetDirection(float hor, float vert)
    {
        _hor = hor;
        _vert = vert;
    }

    public void GetMoveInfo(out Vector3 pos, out Vector3 velocity)
    {
        pos = transform.position;
        velocity = _rigidbody.velocity;
    }
}
