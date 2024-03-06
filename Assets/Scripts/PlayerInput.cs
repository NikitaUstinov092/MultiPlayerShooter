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
        _unitPlayer.GetMoveInfo(out Vector3 position);
        Dictionary<string, object> data = new Dictionary<string, object>()
        {
            { "x", position.x },
            { "y", position.z }
        };
        MultiplayerManager.Instance.SendMessage("move", data);
    }
}
