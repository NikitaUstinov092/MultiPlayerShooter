using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
   public event Action<int> OnHealthSetup; 
   public event Action<int> OnHealthChanged; 
   [field: SerializeField, ReadOnly] public int Health {get; private set;}

   [SerializeField] private Config _startConfig;

   private void Start()
   {
      var config = _startConfig.GetConfig();
      if (!config.TryGetValue("hp", out var hp))
      {
         throw new Exception("Стартовый hp не найден");
      }
      SetUpHealth((int) hp);
   }
   private void SetUpHealth(int health)
   {
      Debug.Log(Health + "ПЕРВОНАЧАЛЬНОЕ ЗДОРОВЬЕ");
      Health = health;
      OnHealthSetup?.Invoke(Health);
   }
   public void ReceiveHealth(float health)
   {
      Debug.Log("Здоровье изменено");
      Health = (int)health;
      OnHealthChanged?.Invoke(Health);
   }
}
