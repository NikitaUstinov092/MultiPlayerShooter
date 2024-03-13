using Sirenix.OdinInspector;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  [SerializeField] private Rigidbody _rigidbody;

  [Button]
  public void Release(Vector3 direction, float speed)
  {
    _rigidbody.velocity = direction * speed;
  }
}
