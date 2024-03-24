using UnityEngine;

public class DollSpawner : MonoBehaviour
{
   [SerializeField] private GameObject _prefab;

   public void CreateDoll()
   {
      var transform1 = transform;
      Instantiate(_prefab, transform1.position, transform1.rotation);
   }
}
