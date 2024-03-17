using UnityEngine;


    public class EnemyShoot: MonoBehaviour
    {
        [SerializeField] private Bullet _bullet;
        public void Shoot(ShootInfo shootInfo)
        {
            var velocity = new Vector3(shootInfo.DirX, shootInfo.DirY,  shootInfo.DirZ);
            var position = new Vector3(shootInfo.PosX, shootInfo.PosY,  shootInfo.PosZ);
            Instantiate(_bullet, position, Quaternion.identity).Release(velocity);
        }
    }
