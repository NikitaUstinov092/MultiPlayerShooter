using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  [SerializeField] 
  private Rigidbody _rigidbody;

  [SerializeField] 
  private float _delay = 3f;

  [Button]
  public void Release(Vector3 velocity)
  {
    _rigidbody.velocity = velocity;
    StartCoroutine(DelayDestroy());
  }

  private IEnumerator DelayDestroy()
  {
    yield return new WaitForSecondsRealtime(_delay);
    Destroy();
  }

  private void Destroy()
  {
    Destroy(gameObject);
  }

  private void OnCollisionEnter(Collision other)
  { 
    Destroy();
  }
}
