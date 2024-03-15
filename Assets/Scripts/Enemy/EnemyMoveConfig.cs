using System.Collections.Generic;
using Colyseus.Schema;
using DefaultNamespace;
using UnityEngine;

public class EnemyMoveConfig : MonoBehaviour
{
    [SerializeField] 
    private EnemyMove enemyMove;
    
    private AverageIntervalCalculator _averageIntervalCalculator = new AverageIntervalCalculator();
    
    private Player _player;
    public void Init(Player player)
    {
        _player = player;
        enemyMove.SetSpeed(player.speed);
        _player.OnChange += OnChange;
    }
    

    private void OnChange(List<DataChange> changes)
    {
        _averageIntervalCalculator.SaveReceivedTime();
        var position = transform.position;
       // var position = enemyMove.TargetPosition;
        var velocity = enemyMove.Velocity;
        
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
                case "rX":
                    enemyMove.SetRotateX((float)dataChanged.Value);
                    break;
                case "rY":
                    enemyMove.SetRotateY((float)dataChanged.Value);
                    break;
                
                default:
                    Debug.LogWarning("Не обрабатываются поля " + dataChanged.Field);
                    break;
            }
        }

        enemyMove.SetPosition(position, velocity, _averageIntervalCalculator.GetAverageInterval());
    }

    public void Destroy()
    {
        _player.OnChange -= OnChange;
        Destroy(gameObject);
    }
}
