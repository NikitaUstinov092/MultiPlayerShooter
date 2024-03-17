using UnityEngine;
using UnityEngine.Serialization;

public class HeroInput : MonoBehaviour
{ 
    [FormerlySerializedAs("_playerMove")] [SerializeField] 
    private HeroMove heroMove;

    [FormerlySerializedAs("_playerShoot")] [SerializeField] 
    private HeroShoot heroShoot;

    [SerializeField] 
    private MouseLook _mouseLook;
    private void Update()
    {
        var hor = Input.GetAxisRaw("Horizontal");
        var vert = Input.GetAxisRaw("Vertical");

        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");
        
        heroMove.SetDirection(hor, vert);
        _mouseLook.RotateX(-mouseY);
        _mouseLook.RotateY(mouseX);
        
        if(Input.GetMouseButtonDown(0))
            heroShoot.Shoot();

        if (Input.GetKeyDown(KeyCode.Space))
            heroMove.Jump();
    }
}
