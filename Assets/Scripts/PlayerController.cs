using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float lookSpeed = 3f;

    private PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        Vector3 _xMov = transform.right * Input.GetAxisRaw("Horizontal");
        Vector3 _zMov = transform.forward * Input.GetAxisRaw("Vertical");
        Vector3 _yMov = transform.up * Input.GetAxisRaw("Jump");
        Vector3 _velocity = (_xMov + _zMov + _yMov).normalized * speed;

        motor.SetVelocity(_velocity);

        Vector3 _rotation = new Vector3(0, Input.GetAxisRaw("Mouse X"), 0) * lookSpeed;

        motor.SetRotation(_rotation);

        Vector3 _cameraRotation = new Vector3(Input.GetAxisRaw("Mouse Y"), 0, 0) * lookSpeed;

        motor.SetCameraRotation(_cameraRotation);
    }
}
