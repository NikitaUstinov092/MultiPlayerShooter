using UnityEngine;

    public class GroundChecker: MonoBehaviour
    {
        public bool IsFly = true;

        private void OnCollisionStay(Collision other)
        {
            var contactPoints = other.contacts;
            for (var i = 0; i < contactPoints.Length; i++)
            {
                if (contactPoints[i].normal.y > .45f)
                {
                    IsFly = false;
                }
            }
        }

        private void OnCollisionExit(Collision other)
        {
            IsFly = true;
        }
    }
