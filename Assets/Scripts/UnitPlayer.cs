using Sirenix.OdinInspector;
using UnityEngine;

public class UnitPlayer : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2;

    [ShowInInspector, ReadOnly]
    private Vector3 _direction;
    private void Update()
    {
        transform.position += _direction * (Time.deltaTime * _speed);
    }

    public void SetDirection(float hor, float vert)
    {
        _direction = new Vector3(hor,0, vert).normalized;
    }

    public void GetMoveInfo(out Vector3 pos)
    {
        pos = transform.position;
    }
}
