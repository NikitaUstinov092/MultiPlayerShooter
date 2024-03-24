using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDestroyer : MonoBehaviour
{
    [SerializeField] 
    private float _delay = 3f;
    private void Start()
    {
        StartCoroutine(DelayDestroy());
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSecondsRealtime(_delay);
        Destroy(gameObject);
    }
}
