using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class Health : MonoBehaviour
{
   public event Action<int> OnHealthChanged;
   public event Action OnDeath;
   public int GetMaxHP => _maxHealth;

   [SerializeField]
   protected int _maxHealth ;
   
   protected int _currentHealth;
   
   public void SetUpHealth(int health)
   {
      if (health > 0 && health <= _maxHealth)
      {
         _currentHealth = health;
      }
      else if(health > _maxHealth)
      {
         _currentHealth = _maxHealth;
      }
   }
   protected void OnDamage()
   {
      if (_currentHealth <= 0)
      {
         OnDeath?.Invoke();
         return;
      }
      OnHealthChanged?.Invoke(_currentHealth);
   }
}


public interface IDamageable
{
   void ReceiveDamage(int damage);
}
