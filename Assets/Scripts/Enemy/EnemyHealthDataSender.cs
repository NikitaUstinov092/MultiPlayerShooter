using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthDataSender : MonoBehaviour
{
    [SerializeField]
    private EnemyHealth _enemyHealth;
    private MultiplayerManager _multiplayerManager;
    private string _sessionId;

    /*private void Awake()
    {
        _multiplayerManager = MultiplayerManager.Instance;
        _sessionId = _multiplayerManager.GetClientKey();
        _enemyHealth.OnHealthChanged += SenHealthData;
    }
    
    private void OnDestroy()
    {
        _enemyHealth.OnHealthChanged -= SenHealthData;
    }*/
   
    private void SenHealthData(int health)
    {
        Dictionary<string, object> data = new Dictionary<string, object>()
        {
            {"id", _sessionId},
            {"value", health}
        };
        
        _multiplayerManager.SendMessage("health", data);
        Debug.Log("Отправлено на сервер");
    }
}
