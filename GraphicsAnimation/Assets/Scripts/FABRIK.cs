using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FABRIK : MonoBehaviour
{

    public Transform[] jointPosition;
    public Transform target;
    public float[] jointDistance;
    public float totalJointDistance = 0;
    public float tolerance = .05f;
    public Transform endJoint;
    public Quaternion[] startRotation;

    // Start is called before the first frame update
    void Start()
    {
        
        for(int i = 0; i <= jointDistance.Length - 1; i++)
        {

            totalJointDistance += jointDistance[i];
        }

        startRotation[0] = jointPosition[0].rotation;
        startRotation[1] = jointPosition[1].rotation;
        startRotation[2] = jointPosition[2].rotation;
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance( jointPosition[0].position, target.position);

        //target unreachable
        if(distance > totalJointDistance)
        {

            for(int i = 0; i < jointPosition.Length - 1; i++)
            {

                float length = Vector3.Distance(jointPosition[i].position, target.position);
                float lambda = jointDistance[i] / length;
                jointPosition[i + 1].position = (1 - lambda) * jointPosition[i].position + lambda * target.position;
            }
        }

        //target is reachable
        else
        {

            Vector3 b = jointPosition[0].position;
            float difA = Vector3.Distance(jointPosition[jointPosition.Length - 1].position, target.position);

            while(difA > tolerance)
            {

                //forward reaching
                jointPosition[jointPosition.Length - 1].position = target.position;

                for(int i = jointPosition.Length - 1; i >= 0; i--)
                {

                    float length = Vector3.Distance(jointPosition[i + 1 ].position, jointPosition[i].position);
                    float lambda = jointDistance[i] / length;
                    jointPosition[i].position = (1 - lambda) * jointPosition[i].position + lambda * jointPosition[i-1].position;
                }

                //backward reaching
                jointPosition[0].position = b;

                for(int i = 0; i < jointPosition.Length - 1; i++)
                {

                    float length = Vector3.Distance(jointPosition[i + 1].position, jointPosition[i].position);
                    float lambda = jointDistance[i] / length;
                    jointPosition[i + 1].position = (1 - lambda) * jointPosition[i].position + lambda * jointPosition[i+1].position;
                }

                jointPosition[jointPosition.Length - 1] = endJoint;

                difA = Vector3.Distance(jointPosition[jointPosition.Length - 1].position, target.position);
            }

            InverseRotation();
        }
    }

    void InverseRotation()
    {
        

    }
}
