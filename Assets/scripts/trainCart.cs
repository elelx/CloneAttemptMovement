using UnityEngine;

public class trainCart : MonoBehaviour
{
    public Rigidbody rb;

    public WheelCollider wheel1, wheel2, wheel3, wheel4; //aatached to car wheels
    public float drivespeed, steerspeed;
    float horizontalInput, verticalInput;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

    }

    private void FixedUpdate()
    {
        float motor = Input.GetAxis("Vertical") * drivespeed;
        wheel1.motorTorque = motor;
        wheel2.motorTorque = motor;
        wheel3.motorTorque = motor;
        wheel4.motorTorque = motor;

        wheel1.steerAngle = steerspeed * horizontalInput;
        wheel2.steerAngle = steerspeed * horizontalInput;
    }
}