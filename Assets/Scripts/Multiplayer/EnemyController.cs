using System.Collections.Generic;
using Colyseus.Schema;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public void OnChange(List<DataChange> changes)
    {
        Vector3 position = transform.position;
        
        foreach (var dataChanged in changes)
        {
            switch (dataChanged.Field)
            {
                case "x":
                    position.x = (float)dataChanged.Value;
                    break;
                case "y":
                    position.z = (float)dataChanged.Value;
                    break;
                default:
                    Debug.LogWarning("Не обрабатываются поля " + dataChanged.Field);
                    break;
            }
        }

        transform.position = position;
    }
}
