using System;
using UnityEngine;

public class HeroMove : CharacterMove
{
    public event Action<Vector3, Vector3, float, float> OnPlayerMoved;
    
    [SerializeField] 
    private Rigidbody _rigidbody;

    [SerializeField] 
    private GroundChecker _groundChecker;

    [SerializeField] 
    private Transform _head;

    [SerializeField] 
    private float _jumpForce = 50f;
   
    [SerializeField] 
    private float _jumpDelay = 0.2f;
    
    private float _jumpTime;
    private float _hor, _vert;

    public void SetDirection(float hor, float vert)
    {
        _hor = hor;
        _vert = vert;
    }
    
    public void Jump()
    {
        if(_groundChecker.IsFly)
            return;
        
        if(Time.time - _jumpTime < _jumpDelay)
            return;
        
        _jumpTime = Time.time;
        
        _rigidbody.AddForce(0,_jumpForce,0, ForceMode.VelocityChange);
    }
    
    private void FixedUpdate()
    {
        var playerTransform = transform;
        var velocity = (playerTransform.forward * _vert + playerTransform.right * _hor).normalized * Speed;
        velocity.y = _rigidbody.velocity.y;
        Velocity = velocity;
        
        _rigidbody.velocity = Velocity;

        OnMove();
    }

    private void OnMove()
    {
        var transform1 = transform;
        var pos = transform1.position;
        var velocity = _rigidbody.velocity;
        var rotateX = _head.localEulerAngles.x; 
        var rotateY = transform1.eulerAngles.y;

        OnPlayerMoved?.Invoke(pos, velocity, rotateX, rotateY);
    }
}
