using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2;

    [SerializeField] 
    private Rigidbody _rigidbody;

    [SerializeField] 
    private GroundChecker _groundChecker;

    [SerializeField] 
    private float _jumpForce = 50f;
   
    [SerializeField] 
    private float _jumpDelay = 0.2f;
    
    private float _jumpTime;
    private float _hor, _vert;
   
    private void FixedUpdate()
    {
        var playerTransform = transform;
        var velocity = (playerTransform.forward * _vert + playerTransform.right * _hor).normalized * _speed;
        velocity.y = _rigidbody.velocity.y;
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

    public void Jump()
    {
        if(_groundChecker.IsFly)
            return;
        
        if(Time.deltaTime - _jumpTime < _jumpDelay)
            return;
        
        _jumpTime = Time.time;
        
        _rigidbody.AddForce(0,_jumpForce,0, ForceMode.VelocityChange);
    }

   
}
