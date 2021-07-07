using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    public float motorTorque = 250f;
    public float brakeTorque = 22000;
    public float maxSteerAngle = 30f;

    Rigidbody rb;
    public Transform CenterOFGravity;

    public GameObject[] wheelMeshes;
    public WheelCollider[] wheelColliders;

    public float antiRoll = 5000f;

    




    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = CenterOFGravity.transform.localPosition;

        
    }

    void LateUpdate()
    {
        AnimateWheel();
    }


    void FixedUpdate()
    {
        for (int i = 0; i < wheelColliders.Length - 1; i++)
        {
            GroundWheels(wheelColliders[i], wheelColliders[i + 1]);
        }

    }


    public void Drive(float steer, float move, float brake)
    {
        for (int i = 0; i < wheelColliders.Length; i++)
        {

            if (i < 2)
            {
                wheelColliders[i].steerAngle = steer * maxSteerAngle;
            }

            wheelColliders[i].motorTorque = move * motorTorque;
            wheelColliders[i].brakeTorque = brake * brakeTorque;
        }
    }


    void AnimateWheel()
    {
        Vector3 pos;
        Quaternion rot;

        for (int i = 0; i < wheelColliders.Length; i++)
        {
            wheelColliders[i].GetWorldPose(out pos, out rot);
            wheelMeshes[i].transform.position = pos;
            wheelMeshes[i].transform.rotation = rot;
        }
    }


    void GroundWheels(WheelCollider WL, WheelCollider WR)
    {
        WheelHit hit;
        float travelL = 1.0f;
        float travelR = 1.0f;

        bool groundedL = WL.GetGroundHit(out hit);
        if (groundedL)
        {
            travelL = (-WL.transform.InverseTransformPoint(hit.point).y - WL.radius) / WL.suspensionDistance;
        }

        bool groundedR = WR.GetGroundHit(out hit);
        if (groundedR)
        {
            travelR = (-WR.transform.InverseTransformPoint(hit.point).y - WR.radius) / WR.suspensionDistance;
        }

        float antiRollForce = (travelL - travelR) * antiRoll;

        if (groundedL)
        {
            rb.AddForceAtPosition(WL.transform.up * -antiRollForce, WL.transform.position);
        }

        if (groundedR)
        {
            rb.AddForceAtPosition(WR.transform.up * antiRollForce, WR.transform.position);
        }

    }

}
