/*
File name: InverseKinematics.cs
Purpose:  This is for the use of inverse kinematics
Contributors: Nick Brennan-Martin and Bradley Chamberlain
Collaborated on one PC
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseKinematics : MonoBehaviour
{

    Matrix4x4 rotationMatrix;
    public GameObject startJoint;
    public GameObject endJoint;
    public GameObject middleJoint;
    public Vector3 constantEffector;
    public Vector3 target;
    float x;
    float y;
    float z;
    float tan;

    public float error;
    public float length0;
    public float length1;
    public Transform[] joints;

    public Transform neckJoint;

    // Start is called before the first frame update
    void Start()
    {

        joints[0] = startJoint.transform;
        joints[1] = middleJoint.transform;
        joints[2] = endJoint.transform;
    }

    // Update is called once per frame
    void Update()
    {

        //middleJoint.transform.position = TrianglesIK();
        //Invserse2();
    }

    public void FormMatrix()
    {

        rotationMatrix[0] = Mathf.Cos(y) * Mathf.Cos(z);
        rotationMatrix[1] = -1 * Mathf.Cos(y) * Mathf.Sin(z);
        rotationMatrix[2] = Mathf.Sin(y);

        rotationMatrix[4] = Mathf.Sin(x) * Mathf.Sin(y) * Mathf.Cos(z) + Mathf.Cos(x) * Mathf.Sin(z);
        rotationMatrix[5] = -1 * Mathf.Sin(x) * Mathf.Sin(y) * Mathf.Sin(z) + Mathf.Cos(x) * Mathf.Cos(z);
        rotationMatrix[6] = -1 * Mathf.Sin(x) * Mathf.Cos(y);

        rotationMatrix[8] = -1 * Mathf.Cos(x) * Mathf.Sin(y) * Mathf.Cos(z) + Mathf.Sin(x) * Mathf.Sin(z);
        rotationMatrix[9] = Mathf.Cos(x) * Mathf.Sin(x) * Mathf.Cos(z) + Mathf.Sin(x) * Mathf.Cos(z);
        rotationMatrix[10] = Mathf.Cos(x) * Mathf.Sin(x) * Mathf.Cos(z) + Mathf.Sin(x) * Mathf.Cos(z);

        tan = Mathf.Atan2(-1 * rotationMatrix[6], rotationMatrix[10]);
    }

    public Vector3 TrianglesIK()
    {

        Vector3 c = -startJoint.transform.position + constantEffector;
        Vector3 d = -startJoint.transform.position + endJoint.transform.position;
        Vector3 n = Vector3.Cross(c, d);
        Vector3 nNormalize = Vector3.Normalize(n);
        Vector3 dNormalize = Vector3.Normalize(d);
        Vector3 h = Vector3.Cross(nNormalize, dNormalize);

        float s = .5f * (d.magnitude + length0 + length1);
        float v = s - d.magnitude;
        float bigA = Mathf.Sqrt(s * (v) *(s - length0) *(s - length1));
        float bigH = 2 * bigA / d.magnitude;
        float bigD = Mathf.Sqrt(length0 * length0 - bigH * bigH);
        Vector3 p = startJoint.transform.position + bigD * d + bigH * h;

        //float Distance = Mathf.Sqrt(c.x * c.x + h.y * h.y);
        //Vector3 p = startJoint.transform.position + Distance * d + h.y * h;
        //Debug.Log(p);
        return p;
    }

    public void Invserse2()
    {

        Vector3 v = target - neckJoint.position;
        Vector3 z = v.normalized;

    }
}
