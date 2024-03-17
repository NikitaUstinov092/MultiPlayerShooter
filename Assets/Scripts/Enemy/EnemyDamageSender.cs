using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyDamageSender : MonoBehaviour
{
    [FormerlySerializedAs("damageObserver")] [FormerlySerializedAs("damageReciever")] [SerializeField]
    private EnemyDamageObserver enemyDamageObserver;
    
    private MultiplayerManager _multiplayerManager;
    private string _playerID;

    private void Awake()
    {
        _multiplayerManager = MultiplayerManager.Instance;
        enemyDamageObserver.OnDamageReceived += SendHealthData;
    }
    
    private void OnDestroy()
    {
        enemyDamageObserver.OnDamageReceived -= SendHealthData;
    }
   
    private void SendHealthData(int damage)
    {
        var data = new Dictionary<string, object>()
        {
            {"id", _playerID},
            {"value", damage}
        };
        
        _multiplayerManager.SendMessage("damage", data);
        Debug.Log($"Урон {damage} отправлен на сервер id: {_playerID}");
    }

    public void SetPlayerId(string id)
    {
        _playerID = id;
    }
}
