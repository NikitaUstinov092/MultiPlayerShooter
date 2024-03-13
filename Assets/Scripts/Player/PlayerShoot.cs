using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
   public event Action<ShootInfo> OnBulletReleased;
   
   [SerializeField] 
   private Bullet _bullet;
   
   [SerializeField] 
   private Transform _spawnPoint;
   
   [SerializeField] 
   private float _speed = 10f;
   
   [SerializeField] 
   private float _coolDown = 0.2f;
   
   private float _lastShootTime;
   
   [Button]
   public void Shoot()
   {
      if (!CanShoot())
         return;
      
      Instantiate(_bullet, _spawnPoint.position, _spawnPoint.rotation).Release(_spawnPoint.forward, _speed);
      OnShoot();
   }

   private void OnShoot()
   {
     var info = new ShootInfo();
     var position = _spawnPoint.position;
     var direction = _spawnPoint.forward;
     
     info.PosX = position.x;
     info.PosY = position.y;
     info.PosZ = position.z;
     
     info.DirX = direction.x;
     info.DirY = direction.y;
     info.DirZ = direction.z;
     
     OnBulletReleased?.Invoke(info);
   }

   private bool CanShoot()
   {
      if (Time.time - _lastShootTime < _coolDown)
         return false;
      
      _lastShootTime = Time.time;
      return true;
   }
}
