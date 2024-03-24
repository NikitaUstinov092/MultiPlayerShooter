using System.Collections.Generic;
using Colyseus.Schema;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyDataReciever : MonoBehaviour
{
    [SerializeField] 
    private EnemyMove _enemyMove;
    
    [FormerlySerializedAs("_enemyHp")] [SerializeField] 
    private Health hp;
    
    private AverageIntervalCalculator _averageIntervalCalculator = new AverageIntervalCalculator();
    
    private Player _player;
    public void Init(Player player)
    {
        _player = player;
        _enemyMove.SetSpeed(player.speed);
        _player.OnChange += OnChange;
    }

    private void OnChange(List<DataChange> changes)
    {
        _averageIntervalCalculator.SaveReceivedTime();
        var position = transform.position;
        var velocity = _enemyMove.Velocity;
        
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
                    _enemyMove.SetRotateX((float)dataChanged.Value);
                    break;
                case "rY":
                    _enemyMove.SetRotateY((float)dataChanged.Value);
                    break;
                
                case "hP":
                    hp.ReceiveHealth((float)dataChanged.Value);
                    break;

                default:
                    Debug.LogWarning("Не обрабатываются поля " + dataChanged.Field);
                    break;
            }
        }

        _enemyMove.SetPosition(position, velocity, _averageIntervalCalculator.GetAverageInterval());
    }

    public void Destroy()
    {
        _player.OnChange -= OnChange;
        Destroy(gameObject);
    }
}
