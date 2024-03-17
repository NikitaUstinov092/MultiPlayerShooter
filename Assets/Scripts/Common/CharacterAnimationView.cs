using UnityEngine;

public class CharacterAnimationView : MonoBehaviour
{
   private static readonly int Grounded = Animator.StringToHash("Grounded");
   private static readonly int Speed = Animator.StringToHash("Speed");
   
   [SerializeField] private GroundChecker _groundChecker;
   [SerializeField] private Animator _animator;
   [SerializeField] private CharacterMove _character;
   private void Update()
   {
      var localVelocity = _character.transform.InverseTransformVector(_character.Velocity);
    
      var speed = localVelocity.magnitude / _character.Speed;
      var sign = Mathf.Sign(localVelocity.z);
      
      _animator.SetFloat(Speed, speed * sign);
      _animator.SetBool(Grounded, _groundChecker.IsFly == false);
   }
}
