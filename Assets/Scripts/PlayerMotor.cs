using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Vector3 velocity;
    private Vector3 rotation;
    private Vector3 cameraRotation;
    private Vector3 thrusterForce;

    private Rigidbody rb;

    public void SetVelocity(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void SetRotation(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void SetCameraRotation(Vector3 _cameraRotation)
    {
        cameraRotation = _cameraRotation;
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
        print(cam.transform.eulerAngles[0]);
        if (cameraRotation[0] < 0)
        {
            if (cam.transform.eulerAngles[0] <= 50f || cam.transform.eulerAngles[0] >= 100f)
                cam.transform.Rotate(-cameraRotation);
        }
        else if (cameraRotation[0] > 0)
        {
            if (cam.transform.eulerAngles[0] <= 200f || cam.transform.eulerAngles[0] >= 330f)
                cam.transform.Rotate(-cameraRotation);
        }
    }
}