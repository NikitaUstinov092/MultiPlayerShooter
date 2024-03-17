using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class HeroShoot : MonoBehaviour
{
   public event Action<ShootInfo> OnBulletReleased;
   
   [SerializeField] 
   private Bullet _bullet;

   [SerializeField] 
   private Transform _spawnPoint;

   [SerializeField] 
   private int _damage = 2;
   
   [SerializeField] 
   private float _speed = 10f;
   
   [SerializeField] 
   private float _coolDown = 0.2f;
   
   private float _lastShootTime;

   private Vector3 _velocity;

   [Button]
   public void Shoot()
   {
      if (!CanShoot())
         return;
      
      _velocity = _spawnPoint.forward * _speed;
      Instantiate(_bullet, _spawnPoint.position, _spawnPoint.rotation).Release(_velocity, _damage);
      OnShoot();
   }

   private void OnShoot()
   {
     var info = new ShootInfo();
     var position = _spawnPoint.position;

     info.PosX = position.x;
     info.PosY = position.y;
     info.PosZ = position.z;
     
     info.DirX = _velocity.x;
     info.DirY = _velocity.y;
     info.DirZ = _velocity.z;
     
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
