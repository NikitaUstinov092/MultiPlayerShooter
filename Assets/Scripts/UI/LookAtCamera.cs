using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform _target;
    private Transform _transform;
    private void Start()
    {
        _transform = transform;
        _target = Camera.main.transform;
    }

    private void Update()
    {
        _transform.LookAt(_target);
    }
}
