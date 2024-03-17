using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "NewConfig", menuName = "Config/CreateNewConfig")]
public class Config : SerializedScriptableObject
{
    public readonly Dictionary<string, int> Data = new Dictionary<string, int>();
    public Dictionary<string, object> GetConfig()
    {
        return Data.ToDictionary<KeyValuePair<string, int>, string, object>(kvp => kvp.Key, kvp => kvp.Value);
    }

}