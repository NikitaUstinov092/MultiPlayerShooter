using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Common
{
    /// <summary>
    /// TO DO убрать монобех
    /// </summary>
    public class ServerDataSender: MonoBehaviour
    {
        [SerializeField] 
        private PlayerMove _playerMove;
        
        [SerializeField] 
        private PlayerShoot _shoot;

        private MultiplayerManager _multiplayerManager;
        
        private void Awake()
        {
            _multiplayerManager = MultiplayerManager.Instance;
            _playerMove.OnPlayerMoved += SendMoveData;
            _shoot.OnBulletReleased += SendShootData;
        }

        private void OnDestroy()
        {
            _playerMove.OnPlayerMoved -= SendMoveData;
            _shoot.OnBulletReleased -= SendShootData;
        }
        
        private void SendShootData(ShootInfo info)
        {
            info.Key = _multiplayerManager.GetClientKey();
            var json = JsonUtility.ToJson(info);
            _multiplayerManager.SendMessage("shoot", json);
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
            _multiplayerManager.SendMessage("move", data);
        }
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


