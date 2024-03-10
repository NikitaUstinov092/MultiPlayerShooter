using UnityEngine;

    public class EnemyMove: CharacterMove
    {
        public Vector3 TargetPosition { get; private set; } = Vector3.zero;
        private float _velocityMagnitude;
      
        
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
