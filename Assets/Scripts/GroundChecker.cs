using UnityEngine;

    public class GroundChecker: MonoBehaviour
    {
        public bool IsFly = true;
        
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _coycoteTime = .15f;

        private float _flyTimer;
        private void Update()
        {
           if(Physics.CheckSphere(transform.position, _radius, _layerMask))
           {
               IsFly = false;
               _flyTimer = 0;
           }
           else
           {
               _flyTimer += Time.deltaTime;
               if(_flyTimer> _coycoteTime)
                   IsFly = true;
           }
        }
        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
        #endif
    }
