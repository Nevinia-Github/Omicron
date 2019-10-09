using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Vector3 velocity;
    private Vector3 rotation;
    private float cameraRotationX = 0f;
    private float currentCameraRotX = 0f;
    private Vector3 thrusterForce;

    [SerializeField]
    private float cameraRotLimit = 85f;

    private Rigidbody rb;

    public void SetVelocity(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void SetRotation(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void SetCameraRotationX(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    public void SetThrusterForce(Vector3 _thrusterForce)
    {
        thrusterForce = _thrusterForce;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
        if(thrusterForce != Vector3.zero)
        {
            rb.AddForce(thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    private void Rotate()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        currentCameraRotX += cameraRotationX;
        currentCameraRotX = Mathf.Clamp(currentCameraRotX, -cameraRotLimit, cameraRotLimit);

        cam.transform.localEulerAngles = new Vector3(-currentCameraRotX, 0f, 0f);

    }
}