using System;
using UnityEngine;

public class PlayerAnimationView : MonoBehaviour
{
   private static readonly int Grounded = Animator.StringToHash("Grounded");
   private static readonly int Speed = Animator.StringToHash("Speed");
   
   [SerializeField] private GroundChecker _groundChecker;
   [SerializeField] private Animator _animator;
   [SerializeField] private Rigidbody _rigidbody;
   [SerializeField] private float _maxSpeed;
   private void Update()
   {
      var localVelocity = _rigidbody.transform.InverseTransformVector(_rigidbody.velocity);
    
      var speed = localVelocity.magnitude / _maxSpeed;
      var sign = Mathf.Sign(localVelocity.z);
      
      _animator.SetFloat(Speed, speed * sign);
      _animator.SetBool(Grounded, _groundChecker.IsFly == false);
   }
}
