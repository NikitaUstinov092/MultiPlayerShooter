using System;
using System.Collections.Generic;
using Colyseus.Schema;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyMoveConfig : MonoBehaviour
{
    [FormerlySerializedAs("_enemy")] [SerializeField] 
    private EnemyMove enemyMove;
    
    private AverageIntervalCalculator _averageIntervalCalculator = new();
    private Player _player;
    public void Init(Player player)
    {
        _player = player;
        enemyMove.SetSpeed(player.speed);
        _player.OnChange += OnChange;
    }
    public void OnChange(List<DataChange> changes)
    {
        _averageIntervalCalculator.SaveReceivedTime();
        var position = enemyMove.TargetPosition;
        var velocity = Vector3.zero;
        
        foreach (var dataChanged in changes)
        {
            switch (dataChanged.Field)
            {
                case "pX":
                    position.x = (float)dataChanged.Value;
                    break;
                case "pY":
                    position.y = (float)dataChanged.Value;
                    break;
                case "pZ":
                    position.z = (float)dataChanged.Value;
                    break;
                case "vX":
                    velocity.x = (float)dataChanged.Value;
                    break;
                case "vY":
                    velocity.y = (float)dataChanged.Value;
                    break;
                case "vZ":
                    velocity.z = (float)dataChanged.Value;
                    break;
                default:
                    Debug.LogWarning("Не обрабатываются поля " + dataChanged.Field);
                    break;
            }
        }

        enemyMove.SetPosition(position, velocity, _averageIntervalCalculator.GetAverageInterval());
    }

    private void OnDestroy()
    {
        _player.OnChange -= OnChange;
        Destroy(gameObject);
    }
}
