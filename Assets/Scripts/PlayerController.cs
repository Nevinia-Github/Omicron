using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(ConfigurableJoint))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSpeed = 3f;
    [SerializeField]
    private float thrusterForce = 1000f;
    [SerializeField]
    private float maxThrusterTime = 100f;

    [Header("Joint")]
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 3.402823e+38f;

    private PlayerMotor motor;
    private ConfigurableJoint joint;
    private float thrusterTime = 100f;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();
        SetJointSettings(jointSpring);
    }

    private void Update()
    {
        Vector3 _xMov = transform.right * Input.GetAxisRaw("Horizontal");
        Vector3 _zMov = transform.forward * Input.GetAxisRaw("Vertical");
        Vector3 _velocity = (_xMov + _zMov).normalized * speed;

        motor.SetVelocity(_velocity);

        Vector3 _rotation = new Vector3(0, Input.GetAxisRaw("Mouse X"), 0) * lookSpeed;

        motor.SetRotation(_rotation);

        float _cameraRotationX = Input.GetAxisRaw("Mouse Y") * lookSpeed;

        motor.SetCameraRotationX(_cameraRotationX);

        Vector3 _thrusterForce = Vector3.zero;
        if (Input.GetButton("Jump") && thrusterTime > 0)
        {
            thrusterTime -= 2;
            _thrusterForce = Vector3.up * thrusterForce;
            SetJointSettings(0f);
        }
        else
            SetJointSettings(jointSpring);

        if (thrusterTime < maxThrusterTime)
            thrusterTime++;

        print(thrusterTime);

        motor.SetThrusterForce(_thrusterForce);
    }

    private void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive
        {
            maximumForce = jointMaxForce,
            positionSpring = _jointSpring
        };

    }
}
