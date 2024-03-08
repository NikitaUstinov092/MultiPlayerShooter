using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInput : MonoBehaviour
{
    [FormerlySerializedAs("unitPlayer")] [FormerlySerializedAs("_player")] [SerializeField] 
    private UnitPlayer _unitPlayer;
    private void Update()
    {
        var hor = Input.GetAxisRaw("Horizontal");
        var vert = Input.GetAxisRaw("Vertical");
        _unitPlayer.SetDirection(hor, vert);
        SendMove();
    }

    private void SendMove()
    {
        _unitPlayer.GetMoveInfo(out Vector3 position, out Vector3 velocity);
        Dictionary<string, object> data = new Dictionary<string, object>()
        {
            { "pX", position.x },
            { "pY", position.y },
            { "pZ", position.z },
            { "vX", velocity.x },
            { "vY", velocity.y },
            { "vZ", velocity.z },
            
        };
        MultiplayerManager.Instance.SendMessage("move", data);
    }
}
