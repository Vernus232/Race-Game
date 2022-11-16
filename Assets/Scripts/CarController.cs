using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    public int carIndex;

    [Header("Nitro")]
    public float nitroMaxValue;
    [HideInInspector] public float nitroValue;
    public float nitroPower;
    private float nitroAddup;
    public float nitroDecrease;
    [Range(1,10)]
    public int incToDec;
    [Header ("Kostili")]
    public int rotationStrength;
    public int breakStrengthBoost;
    public int stabilization;
    public int boost;
    public bool is_2wd_front = false;
    public bool is_2wd_back = true;
    public bool is_4wd = false;

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBreaking;
    private Vector3 m_EulerAngleVelocity;
    private bool nitroCD = false;

    
    [Header ("Options")]
    public float motorForce;
    public float breakForce;
    public float maxSteerAngle;

    [Header ("Links")]
    [SerializeField] private Rigidbody carRb;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;
    [SerializeField] private ParticleSystem[] nitroParticleSystems;
    private void Start()
    {
        nitroAddup = (nitroDecrease / incToDec);
        nitroValue = nitroMaxValue;
        
        if (GlobalCarData.isFirstLoad)
        {   
            SaveCarSettings();
        }
        else 
        {   
            LoadCarSettings();
        }
    }

    // physics update (50fps)
    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        TurnRotation();
        Nitro();
    }

    // Z coordinate rotation
    private void TurnRotation()
    {
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
        if (Input.GetKey(KeyCode.A))
        {
            m_EulerAngleVelocity = new Vector3(0, 0, rotationStrength);
            carRb.MoveRotation(carRb.rotation * deltaRotation);
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_EulerAngleVelocity = new Vector3(0, 0, -rotationStrength);
            carRb.MoveRotation(carRb.rotation * deltaRotation);
        }
    }

    // Input
    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    // Motor
    private void HandleMotor()
    {
        if (is_2wd_back){
            rearLeftWheelCollider.motorTorque = verticalInput * motorForce;
            rearRightWheelCollider.motorTorque = verticalInput * motorForce;
        }
        else if (is_2wd_front){
            frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
            frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        }
        else if (is_4wd){
            frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
            frontRightWheelCollider.motorTorque = verticalInput * motorForce;
            rearLeftWheelCollider.motorTorque = verticalInput * motorForce;
            rearRightWheelCollider.motorTorque = verticalInput * motorForce;
        }

        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    // Break
    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    // Nitro
    public void Nitro()
    {
        if (nitroCD == false)
        {
            if (Input.GetKey(KeyCode.LeftShift) & nitroValue > 0)
            {
                carRb.AddForce(transform.forward * nitroPower);
                nitroValue -= nitroDecrease;
                foreach (ParticleSystem nitroParticleSystem in nitroParticleSystems)
                {
                    nitroParticleSystem.Play();
                }
            }
        }
        if (nitroValue < nitroMaxValue)
        {
            nitroValue += nitroAddup;
        }

        // Nitro cooldown
        if (nitroValue <= 0)
        {
            nitroCD = true;
        }
        if (nitroValue > nitroMaxValue / 10)
        {
            nitroCD = false;
        }
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    public void SaveCarSettings()
    {
        GlobalCarData.motorForce = motorForce;
        GlobalCarData.breakForce = breakForce;
        GlobalCarData.maxSteerAngle = maxSteerAngle;
        GlobalCarData.nitroMaxValue = nitroMaxValue;
        GlobalCarData.nitroPower = nitroPower;
        GlobalCarData.nitroDecrease = nitroDecrease;
    }
    public void LoadCarSettings()
    {
        motorForce = GlobalCarData.motorForce;
        breakForce = GlobalCarData.breakForce;
        maxSteerAngle = GlobalCarData.maxSteerAngle;
        nitroMaxValue = GlobalCarData.nitroMaxValue;
        nitroPower = GlobalCarData.nitroPower;
        nitroDecrease = GlobalCarData.nitroDecrease;
    }
 
}