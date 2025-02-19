using UnityEngine;
using UnityEngine.VFX;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Rigidbody _rig;
    [SerializeField] VisualEffect _effect;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        _effect.SetVector3("PlayerPos", transform.position);
        transform.eulerAngles = new(0, Input.GetAxisRaw("Mouse X") + transform.eulerAngles.y, 0);
    }

    private void FixedUpdate()
    {
        var moveDirection = transform.TransformDirection(new(Input.GetAxisRaw("Horizontal"),_rig.linearVelocity.y,Input.GetAxisRaw("Vertical"))) * 10;
        _rig.linearVelocity = new(moveDirection.x, _rig.linearVelocity.y, moveDirection.z);
    }


}
