using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInput : MonoBehaviour
{
    [FormerlySerializedAs("playerMove")] [SerializeField] 
    private PlayerMove _playerMove;

    [SerializeField] 
    private MouseLook _mouseLook;
    private void Update()
    {
        var hor = Input.GetAxisRaw("Horizontal");
        var vert = Input.GetAxisRaw("Vertical");

        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");

        if (Input.GetKeyDown(KeyCode.Space))
            _playerMove.Jump();
        
        _playerMove.SetDirection(hor, vert);
        _mouseLook.RotateX(-mouseY);
        _mouseLook.RotateY(mouseX);
        
        SendMoveServer(); //TO DO Убрать в другой класс
    }

    private void SendMoveServer()
    {
        _playerMove.GetMoveInfo(out Vector3 position, out Vector3 velocity, out float rotateX, out float rotateY);
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
        MultiplayerManager.Instance.SendMessage("move", data);
    }
}
