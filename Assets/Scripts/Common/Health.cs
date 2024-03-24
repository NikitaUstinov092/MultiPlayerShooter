using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class Health : MonoBehaviour
{
   public event Action<int> OnHealthSetup; 
   public event Action<int> OnHealthChanged;
   public event Action OnDeath;
   [field: SerializeField, ReadOnly] public int CurrentHealth {get; private set;}

   [SerializeField] private Config _startConfig;

   protected void Start()
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
      CurrentHealth = health;
      OnHealthSetup?.Invoke(CurrentHealth);
   }
   public void ReceiveHealth(float health)
   {
      CurrentHealth = (int)health;
      OnHealthChanged?.Invoke(CurrentHealth);
     
      if(CurrentHealth<=0)
         OnDeath?.Invoke();
   }
}
