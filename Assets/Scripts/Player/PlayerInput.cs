using UnityEngine;

public class PlayerInput : MonoBehaviour
{ 
    [SerializeField] 
    private PlayerMove _playerMove;

    [SerializeField] 
    private PlayerShoot _playerShoot;

    [SerializeField] 
    private MouseLook _mouseLook;
    private void Update()
    {
        var hor = Input.GetAxisRaw("Horizontal");
        var vert = Input.GetAxisRaw("Vertical");

        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");
        
        _playerMove.SetDirection(hor, vert);
        _mouseLook.RotateX(-mouseY);
        _mouseLook.RotateY(mouseX);
        
        if(Input.GetMouseButtonDown(0))
            _playerShoot.Shoot();

        if (Input.GetKeyDown(KeyCode.Space))
            _playerMove.Jump();
    }
}
