using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  [SerializeField] 
  private Rigidbody _rigidbody;

  [SerializeField] 
  private float _delay = 3f;

  private int _damage;

  [Button]
  public void Release(Vector3 velocity, int damage = 0)
  {
    _rigidbody.velocity = velocity;
    _damage = damage;
  }
  
  private void OnCollisionEnter(Collision other)
  {
    if (other.gameObject.TryGetComponent(out IDamageable damageable))
    { 
      damageable.ReceiveDamage(_damage);
    }
    Destroy();
  }

  private void Destroy()
  {
    Destroy(gameObject);
  }
}
