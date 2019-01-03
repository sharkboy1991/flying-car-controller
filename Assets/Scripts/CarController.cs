using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    Rigidbody _rig;

    public Telemetry _tel;

    public AudioSource engineSound;

    public float power = 500;

    public float xRotSpeed = 50;
    public float yRotSpeed = 50;

    //private variables
    float enginePitch = 1;

    float hAxis = 0;
    float vAxis = 0;

    float gasThrottle = 0;

    float kph = 0;
    float mph = 0;

    void Start()
    {
        _rig = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CalculateSpeed();
        EngineSound();
        GetAxis();
        GasPedal();
        BrakePedal();
        CarMovement();
    }

    void CalculateSpeed()
    {
        kph = _rig.velocity.magnitude * 3.6f;
        mph = _rig.velocity.magnitude * 2.8f;

        _tel.CarSpeed(kph, mph);//send speed data to telemetry
    }

    void EngineSound()
    {
        enginePitch = Mathf.Lerp(1.3f, .6f, (kph - 120) / (0 - 120));

        engineSound.pitch = enginePitch;
    }

    void GetAxis()
    {

        hAxis = Input.GetAxis("Horizontal"); //get horizontal axis input
        vAxis = Input.GetAxis("Vertical");//get vertical axis input

        _tel.Axis(vAxis, hAxis);//send axis data to telemetry

        /*
        //clamp x rotation to -85/85 degrees
        if (transform.rotation.x > -85 && transform.rotation.x < 85)
            vAxis = Input.GetAxis("Vertical");
        else
            vAxis = 0;
            */
    }

    void GasPedal()
    {
        //get gas throttle input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gasThrottle = 1;

            _tel.GasPedal(true);//toggle on gas pedal to telemetry
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            gasThrottle = 0;

            _tel.GasPedal(false);//toggle off gas pedal to telemetry
        }
    }

    void BrakePedal()
    {
        //get brakes input
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _rig.drag = 3f;

            _tel.BrakePedal(true);//toggle on brake pedal to telemetry
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _rig.drag = 0.5f;

            _tel.BrakePedal(false);//toggle off brake pedal to telemetry
        }
    }

    void CarMovement()
    {
        transform.Rotate(Vector3.up * Time.fixedDeltaTime * xRotSpeed * hAxis);//left right
        transform.Rotate(Vector3.right * Time.fixedDeltaTime * yRotSpeed * vAxis);//up down

        Quaternion tempRot = transform.rotation;

        float zRot = 0;

        if (hAxis > 0.1)
            zRot = -25f;
        else if (hAxis < -0.1)
            zRot = 25f;
        else
            zRot = 0f;

        tempRot.eulerAngles = new Vector3(tempRot.eulerAngles.x, tempRot.eulerAngles.y, zRot);
        transform.rotation = Quaternion.Lerp(transform.rotation, tempRot, Time.fixedDeltaTime * 4f);
    }

    private void FixedUpdate()
    {
        _rig.AddForce(transform.forward * Time.fixedDeltaTime * power * gasThrottle);
    }
}
