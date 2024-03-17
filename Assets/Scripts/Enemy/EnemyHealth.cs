using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health, IDamageable
{
    void IDamageable.ReceiveDamage(int damage)
    {
        _currentHealth -= damage;
        Debug.Log($"Нанёсён Урон {_currentHealth}" );
        OnDamage();
    }
}
