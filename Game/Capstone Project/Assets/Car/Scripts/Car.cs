using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    /*public WheelCollider WheelColliderFrontLeft;
    public WheelCollider WheelColliderFrontRight;
    public WheelCollider WheelColliderBackLeft;
    public WheelCollider WheelColliderBackRight;

    public Transform WheelFrontLeft;
    public Transform WheelFrontRight;
    public Transform WheelBackLeft;
    public Transform WheelBackRight;
*/
    public Transform centerOfMass;

    public float Steer {get; set;}
    public float Throttle {get; set;}
    
    public float motorTorque = 1500f;
    public float maxSteer = 20f;

    private Rigidbody rb;
    private Wheel[] wheels;

    void Start()
    {
        wheels = GetComponentsInChildren<Wheel>();
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;

    }


    void Update()
    {
        Steer = GameManager.Instance.InputController.SteerInput;
        Throttle = GameManager.Instance.InputController.ThrottleInput;
        
        foreach(var wheel in wheels)
        {
            wheel.steerAngle = Steer * maxSteer;
            wheel.Torque = Throttle * motorTorque;
        }
    }

}
