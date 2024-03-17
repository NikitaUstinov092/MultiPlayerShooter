using System;
using UnityEngine;

    public class HeroHealth: MonoBehaviour
    {
        public event Action OnDeath;
        public int GetMaxHP => _maxHealth;
        [SerializeField] private int _maxHealth;
        
        private int _currentHealth;

        public void SetUpHealth(float health)
        {
            Debug.Log($"Здоровье {health} получено с сервера, старое значение {_currentHealth}");
            if (health > 0 && health <= _maxHealth)
            {
                _currentHealth = (int)health;
            }
            else if (health >= _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
        }
        
        private void CheckDeath()
        {
            if (_currentHealth > 0)
                return;
            OnDeath?.Invoke();
            Debug.Log("Враг умер");
        }
    }
