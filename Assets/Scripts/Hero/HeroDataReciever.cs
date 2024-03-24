using System.Collections.Generic;
using Colyseus.Schema;
using UnityEngine;

public class HeroDataReciever : MonoBehaviour
{
    [SerializeField] 
    private Health _heroHealth;
    public void Init(Player player)
    {
        player.OnChange += SetHealth;
    }

    private void SetHealth(List<DataChange> changes)
    {
        foreach (var dataChanged in changes)
        {
            switch (dataChanged.Field)
            {
                case "hP":
                    _heroHealth.ReceiveHealth((float) dataChanged.Value);
                    break;
            }
        }

    }
}
