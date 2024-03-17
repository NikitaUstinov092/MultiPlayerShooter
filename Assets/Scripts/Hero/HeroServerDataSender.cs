using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


    /// <summary>
    /// TO DO убрать монобех
    /// </summary>
    public class HeroServerDataSender: MonoBehaviour
    {
        [FormerlySerializedAs("heroMove")] [FormerlySerializedAs("_playerMove")] [SerializeField] 
        private HeroMove _move;
        
        [SerializeField] 
        private HeroShoot _shoot;

        private MultiplayerManager _multiplayerManager;
        
        private void Awake()
        {
            _multiplayerManager = MultiplayerManager.Instance;
            _move.OnPlayerMoved += SendMoveData;
            _shoot.OnBulletReleased += SendShootData;
        }

        private void OnDestroy()
        {
            _move.OnPlayerMoved -= SendMoveData;
            _shoot.OnBulletReleased -= SendShootData;
        }
        
        private void SendShootData(ShootInfo info)
        {
            info.Key = _multiplayerManager.GetClientKey();
            var json = JsonUtility.ToJson(info);
            _multiplayerManager.SendMessage("shooting", json);
        }
        
        private void SendMoveData(Vector3 position, Vector3 velocity, float rotateX, float rotateY)
        {
            var data = new Dictionary<string, object>()
            {
                { "pX", position.x },
                { "pY", position.y },
                { "pZ", position.z },
                { "vX", velocity.x },
                { "vY", velocity.y },
                { "vZ", velocity.z },
                { "rX", rotateX },
                { "rY", rotateY },
            };
            _multiplayerManager.SendMessage("moving", data);
        }
    }


[Serializable]
public struct ShootInfo
{
    public string Key;
    public float PosX;
    public float PosY;
    public float PosZ;
    public float DirX;
    public float DirY;
    public float DirZ;
}


