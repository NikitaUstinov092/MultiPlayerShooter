using System;
using System.Collections.Generic;

public class EnemyStorage<T>
{
    private readonly Dictionary<string, T> _enemies = new Dictionary<string, T>();
    public void AddEnemy(string key, T enemy)
    {
        if (_enemies.ContainsKey(key))
        {
            throw new Exception("Такой враг уже есть");
        }
        _enemies.Add(key, enemy);
    }
    public void RemoveEnemy(string key)
    {
        if (_enemies.ContainsKey(key))
        {
            _enemies.Remove(key);
        }
    }
    public bool HasEnemy(string key, out T enemy)
    {
        if (_enemies.ContainsKey(key))
        {
            enemy = _enemies[key];
            return true;
        }

        enemy = default(T);
        return false;
    }

}