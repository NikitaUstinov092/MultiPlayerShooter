using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HpView : MonoBehaviour
{
    [SerializeField]
    private Slider healthSlider; 
    
    [SerializeField]
    private Health hp;

    private const float StartValue = 1;
 
    private int _previousHealth;
    private void Awake()
    {
        hp.OnHealthSetup += Init;
        hp.OnHealthChanged += UpdateHealth;
    }

    private void OnDestroy()
    {
        hp.OnHealthSetup -= Init;
        hp.OnHealthChanged -= UpdateHealth;
    }

    private void Init(int health)
    {
        _previousHealth = health;
        healthSlider.value = StartValue; // Устанавливаем Slider.value в 1
        healthSlider.maxValue = StartValue;
        healthSlider.minValue = 0f;
        hp.OnHealthSetup -= Init; // Отписываемся от события, оно выполнило свою функцию
    }
    
    private void UpdateHealth(int newHealth)
    {
        var healthDifference = newHealth - _previousHealth; // Вычисляем разницу между предыдущим и новым здоровьем
        var healthPercent = Mathf.Abs((float)healthDifference / _previousHealth); // Вычисляем процент изменения здоровья (с учетом абсолютного значения разницы)
        var result = StartValue - healthPercent;
        healthSlider.value = result; 
    }

}

