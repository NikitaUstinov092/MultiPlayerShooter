using UnityEngine;

    public class EnemyMove: CharacterMove
    {
        public Vector3 TargetPosition { get; private set; } = Vector3.zero;
        private float _velocityMagnitude;
        [SerializeField] private Transform _head;
      
        
        public void SetPosition(in Vector3 position, in Vector3 velocity, in float averageInterval)
        {
            TargetPosition = position + Velocity * averageInterval;
            _velocityMagnitude = velocity.magnitude;
            
            Velocity = velocity;
        }

        public void SetSpeed(float value)
        {
            Speed = value;
        }
        
        public void SetRotateX(float value)
        {
            _head.localEulerAngles = new Vector3(value, 0, 0);
        }
        public void SetRotateY(float value)
        {
            transform.localEulerAngles = new Vector3(0, value, 0); 
        }
        

        private void Start()
        {
            TargetPosition = transform.position;
        }

        private void Update()
        {
            if (_velocityMagnitude > .1f)
            {
                var maxDistance = _velocityMagnitude * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, TargetPosition, maxDistance);
            }
            else
            {
                transform.position = TargetPosition;
            }
        }
    }
