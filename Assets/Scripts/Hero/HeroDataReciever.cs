using System.Collections.Generic;
using Colyseus.Schema;
using UnityEngine;

public class HeroDataReciever : MonoBehaviour
{
    [SerializeField] 
    private HeroHealth _heroHealth;
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
                case "hp":
                    _heroHealth.SetUpHealth((float) dataChanged.Value);
                    break;
            }
        }

    }
}
