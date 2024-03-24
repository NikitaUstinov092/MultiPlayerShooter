using UnityEngine;

public class HeroAdapter : MonoBehaviour
{
   [SerializeField]
   private Health _hp;
   
   [SerializeField]
   private DollSpawner _dollSpawner;
   
   private void Awake()
   {
      _hp.OnDeath += _dollSpawner.CreateDoll;
   }
   
   private void OnDisable()
   {
      _hp.OnDeath -= _dollSpawner.CreateDoll;
   }
}
