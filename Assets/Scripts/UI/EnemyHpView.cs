using UnityEngine;
using UnityEngine.UI;

public class EnemyHpView : MonoBehaviour
{
    [SerializeField]
    private Slider healthSlider; 
    
    [SerializeField]
    private EnemyHealth _enemyHp;
 
    private int _previousHealth;
    private void Awake()
    {
        _enemyHp.OnHealthSetup += Init;
        _enemyHp.OnHealthChanged += UpdateHealth;
    }

    private void OnDestroy()
    {
        _enemyHp.OnHealthSetup -= Init;
        _enemyHp.OnHealthChanged -= UpdateHealth;
    }

    private void Init(int health)
    {
        _previousHealth = health;
        healthSlider.value = 1f; // Устанавливаем Slider.value в 1
        healthSlider.maxValue = 1f;
        healthSlider.minValue = 0f;
        _enemyHp.OnHealthSetup -= Init; // Отписываемся от события, оно выполнило свою функцию
    }
    
    private void UpdateHealth(int newHealth)
    {
        var healthDifference = newHealth - _previousHealth; // Вычисляем разницу между предыдущим и новым здоровьем
        var healthPercent = Mathf.Abs((float)healthDifference / _previousHealth); // Вычисляем процент изменения здоровья (с учетом абсолютного значения разницы)
        var result = 1 - healthPercent;
        healthSlider.value = result; 
    }

}

