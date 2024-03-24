using UnityEngine;

public class PositionReseter : MonoBehaviour
{
    [SerializeField] 
    private Health _hp;
    
    private Vector3 _startPos;
    private void Start()
    {
        _startPos = transform.position;
        _hp.OnDeath += ResetPosition;
    }
    
    private void OnDestroy()
    {
        _hp.OnDeath -= ResetPosition;
    }
    
    private void ResetPosition()
    {
        transform.position = _startPos;
    }
}

