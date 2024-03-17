using System;
using UnityEngine;

public class EnemyDamageObserver : MonoBehaviour, IDamageable
{
    public event Action<int> OnDamageReceived;
    
    void IDamageable.ReceiveDamage(int damage)
    {
        OnDamageReceived?.Invoke(damage);
    }
}


public interface IDamageable
{
    void ReceiveDamage(int damage);
}