using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{
    public JointMotor2D motor;

    private HingeJoint2D joint;

    private void Start()
    {
        joint = gameObject.GetComponent<HingeJoint2D>();

        motor = joint.motor;
        joint.useMotor = false;
        motor.motorSpeed = 900f;
        joint.useMotor = true;



    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(joint.limitState);
        if (joint.limitState == JointLimitState2D.LowerLimit)
        {
            Debug.Log(motor.motorSpeed);
            motor.motorSpeed = 30f;
            Debug.Log(motor.motorSpeed);
        }
        else if(joint.limitState == JointLimitState2D.UpperLimit)
        {
            Debug.Log(motor.motorSpeed);
            motor.motorSpeed = -30f;
            Debug.Log(motor.motorSpeed);
        }
    }
}