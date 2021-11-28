using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public bool steer;
    public bool invertSteer;
    public bool power;
    
   public float steerAngle { get; set; }
    public float Torque { get; set; }

    private WheelCollider wheelCollider;
    private Transform wheelTransform;
   
    void Start()
    {
        wheelCollider = GetComponentInChildren<WheelCollider>();
        wheelTransform = GetComponentInChildren<MeshRenderer>().GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.deltaTime != 0)
        {
            wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rot);
            wheelTransform.position = pos;
            wheelTransform.rotation = rot;
        }
    }

    private void FixedUpdate()
    {
        if (steer)
        {
            wheelCollider.steerAngle = steerAngle * (invertSteer ? -1 : 1);
        }

        if (power)
        {
            wheelCollider.motorTorque = Torque;
        }
    }
}
