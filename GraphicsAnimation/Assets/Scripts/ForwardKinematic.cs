/*
File name: FowardKinematics.cs
Purpose:  This program will calculate the the kinematic position of each pose (no longer needed)
Contributors: Nick Brennan-Martin and Bradley Chamberlain
Collaborated on one PC
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardKinematic : MonoBehaviour
{
    // ignore this was for ideas
    public HierarchicalPose currentPose;
    public HierarchicalPose nextPose;
    public HierarchicalPose samplePose;

    public HierarchicalPose lhs;
    public HierarchicalPose rhs;

    public HierarchicalPose previousPose;
    public HierarchicalPose nextNextPose;
    //----------------------------------------------

    public GameObject player;
    public bool thisWASD;
    public bool thisIKJL;
    public float offsetF;
    public float angularOffsetF;
    public float velocityF;
    public float accelerationF;
    public float angularVelocityF;
    public float angularAccelerationF;
    [Range(0, 1)]
    public float u;
    public string mode;

    public enum integration { direct, velocity, acceleration, fVelocity, fAcceleration, bonus};
    public integration choice;


    //----------------------------------------------
    //smoothstep interpolation  blend oepration
    public HierarchicalPose Smoothstep(HierarchicalPose hp, HierarchicalPose hp2, float t)
    {

        HierarchicalPose temp = hp;

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {

            float alpha = t * t * (3 - 2 * t);

            temp.currentPose[i].translation = hp.currentPose[i].translation * alpha + (hp2.currentPose[i].translation * (1 - alpha));
            temp.currentPose[i].orientation = hp.currentPose[i].orientation * alpha + (hp2.currentPose[i].orientation * (1 - alpha));
            temp.currentPose[i].scale = hp.currentPose[i].scale * alpha + (hp2.currentPose[i].scale * (1 - alpha));
        }

        return temp;
    }

    //descale blend oepration
    public HierarchicalPose Descale(HierarchicalPose samplePose, HierarchicalPose nextPose, float t)
    {

        HierarchicalPose temp = samplePose;
        HierarchicalPose invert = InvertPose(nextPose);

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {

            temp.currentPose[i].scale = samplePose.currentPose[i].scale * (1 - t) + invert.currentPose[i].scale * t;
        }

        return temp;
    }

    //convert blend oepration
    public HierarchicalPose Convert(HierarchicalPose samplePose)
    {

        HierarchicalPose temp = samplePose;

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {

            temp.currentPose[i].worldPose = Matrix4x4.TRS(
                samplePose.currentPose[i].translation,
                Quaternion.Euler(samplePose.currentPose[i].orientation.x, samplePose.currentPose[i].orientation.y, samplePose.currentPose[i].orientation.z),
                samplePose.currentPose[i].scale);
        }

        return temp;
    }

    //revert blend oepration
    public HierarchicalPose Revert(HierarchicalPose samplePose)
    {

        HierarchicalPose temp = samplePose;

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {

            temp.currentPose[i].translation = new Vector3(samplePose.currentPose[i].worldPose.m00, samplePose.currentPose[i].worldPose.m01, samplePose.currentPose[i].worldPose.m02);
            temp.currentPose[i].orientation = new Vector3(samplePose.currentPose[i].worldPose.m10, samplePose.currentPose[i].worldPose.m11, samplePose.currentPose[i].worldPose.m12);
            temp.currentPose[i].scale = new Vector3(samplePose.currentPose[i].worldPose.m20, samplePose.currentPose[i].worldPose.m21, samplePose.currentPose[i].worldPose.m22);
        }

        return temp;
    }

    //forwardkinematic blend oepration
    public HierarchicalPose ForwaredKinematic(Hierarchy hier, Matrix4x4[] localTransform, Matrix4x4[] objectTransform, HierarchicalPose samplePose)
    {

        HierarchicalPose temp = samplePose;

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {
            if (hier.treeDepth[i].parentIndex == -1)
            {

                objectTransform[i] = localTransform[i];
            }
            else // forward kinematics
            {

                objectTransform[i] = objectTransform[hier.treeDepth[i].parentIndex] * localTransform[i].transpose;
            }
        }

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {
            samplePose.currentPose[i].transform.position = samplePose.currentPose[i].translation + samplePose.currentPose[hier.treeDepth[i].parentIndex].translation;
        }

        return temp;
    }

    //inverse kinematic blend oepration
    public HierarchicalPose InverseKinematic(Hierarchy hier, Matrix4x4[] localTransform, Matrix4x4[] objectTransform, HierarchicalPose samplePose)
    {

        HierarchicalPose temp = samplePose;

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {
            if (hier.treeDepth[i].parentIndex == -1)
            {

                localTransform[i] = objectTransform[i];
            }
            else // forward kinematics
            {

                localTransform[hier.treeDepth[i].parentIndex] = localTransform[i] * objectTransform[i].transpose;
            }
        }

        for (int i = samplePose.currentPose.Length; i > 0; i--)
        {

            temp.currentPose[hier.treeDepth[i].parentIndex].transform.position = samplePose.currentPose[i].translation + samplePose.currentPose[hier.treeDepth[i].parentIndex].translation;
        }

        return temp;
    }

    //cubichermite interpolation blend oepration
    public HierarchicalPose CubicHermite(HierarchicalPose p0, HierarchicalPose p1, float t)
    {

        HierarchicalPose temp = p0;

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {

            float h1 = 2 * t * t * t - 3 * t * t + 1;
            float h2 = -2 * t * t * t + 3 * t * t;
            float h3 = t * t * t - 2 * t * t + t;
            float h4 = t * t * t - t * t;

            HierarchicalPose t1 = Cubic(previousPose, currentPose, nextPose, nextNextPose, t);
            HierarchicalPose t2 = Cubic(currentPose, nextPose, nextNextPose, previousPose, t);

            temp.currentPose[i].translation = h1 * p0.currentPose[i].translation + h2 * p1.currentPose[i].translation + h3 * t1.currentPose[i].translation + h4 * t2.currentPose[i].translation;
        }

        return temp;
    }


    //----------------------------------------------------------------------
    //concat blend
    public HierarchicalPose Concat(HierarchicalPose lhs, HierarchicalPose rhs)
    {

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {
            samplePose.currentPose[i].translation = lhs.currentPose[i].translation + rhs.currentPose[i].translation;
            samplePose.currentPose[i].orientation = lhs.currentPose[i].orientation + rhs.currentPose[i].orientation;
            samplePose.currentPose[i].scale = new Vector3
                (lhs.currentPose[i].scale.x * rhs.currentPose[i].scale.x,
                 lhs.currentPose[i].scale.y * rhs.currentPose[i].scale.y,
                 lhs.currentPose[i].scale.z * rhs.currentPose[i].scale.z);
        }

        return samplePose;
    }

    //near blend
    public HierarchicalPose Near(HierarchicalPose samplePose, HierarchicalPose currentPose, float t)
    {

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {

            if (t < .5f)
            {

                samplePose.currentPose[i].translation = currentPose.currentPose[i].translation;
                samplePose.currentPose[i].orientation = currentPose.currentPose[i].orientation;
                samplePose.currentPose[i].scale = currentPose.currentPose[i].scale;
            }

            else
            {

                samplePose.currentPose[i].translation = nextPose.currentPose[i].translation;
                samplePose.currentPose[i].orientation = nextPose.currentPose[i].orientation;
                samplePose.currentPose[i].scale = nextPose.currentPose[i].scale;
            }
        }

        return samplePose;
    }


    //binearest blend
    public HierarchicalPose BiNearest(HierarchicalPose hI0, HierarchicalPose hT0, HierarchicalPose hI1, HierarchicalPose hT1, float t, float t2)
    {
        HierarchicalPose hPose = new HierarchicalPose(hT0.currentPose.Length);
        for (int i = 0; i < hI1.currentPose.Length; i++)
        {
            SpatialPose pose0 = new SpatialPose();
            if (t < .5f)
            {
                pose0.orientation = hI0.currentPose[i].orientation;
                pose0.translation = hI0.currentPose[i].translation;
                pose0.scale = hI0.currentPose[i].scale;

            }

            else
            {

                pose0.orientation = hT0.currentPose[i].orientation;
                pose0.translation = hT0.currentPose[i].translation;
                pose0.scale = hT0.currentPose[i].scale;
            }


            SpatialPose pose1 = new SpatialPose();

            if (t < .5f)
            {

                pose1.orientation = hI1.currentPose[i].orientation;
                pose1.translation = hI1.currentPose[i].translation;
                pose1.scale = hI1.currentPose[i].scale;
            }
            else
            {

                pose1.orientation = hT1.currentPose[i].orientation;
                pose1.translation = hT1.currentPose[i].translation;
                pose1.scale = hT1.currentPose[i].scale;
            }

            if (t2 < 0.5f)
            {
                hPose.currentPose[i] = pose0;

            }
            else
            {
                hPose.currentPose[i] = pose1;
            }
        }

        return hPose;
    }

    // Composite Linear Interpolation
    public HierarchicalPose Linear(HierarchicalPose pose0, HierarchicalPose pose1, float u)
    {
        HierarchicalPose newPose = new HierarchicalPose(pose1.currentPose.Length);
        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {
            newPose.currentPose[i].translation = (1 - u) * pose0.currentPose[i].translation + pose1.currentPose[i].translation * u;
            newPose.currentPose[i].orientation = (1 - u) * pose0.currentPose[i].orientation + pose1.currentPose[i].orientation * u;
            float x = (1 - u) * pose0.currentPose[i].scale.x * pose1.currentPose[i].scale.x * u;
            float y = (1 - u) * pose0.currentPose[i].scale.y * pose1.currentPose[i].scale.y * u;
            float z = (1 - u) * pose0.currentPose[i].scale.z * pose1.currentPose[i].scale.z * u;
            newPose.currentPose[i].scale = new Vector3(x, y, z);
        }

        return newPose;
    }
    // Traditional Linear Interpolation
    public HierarchicalPose Lerp(HierarchicalPose hp, HierarchicalPose hp2, float u)
    {
        HierarchicalPose newPose = new HierarchicalPose(hp.currentPose.Length);

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {

            newPose.currentPose[i].translation = hp.currentPose[i].translation + (hp2.currentPose[i].translation - hp.currentPose[i].translation) * u;
            newPose.currentPose[i].orientation = hp.currentPose[i].orientation + (hp2.currentPose[i].orientation - hp.currentPose[i].orientation) * u;
            float x = hp.currentPose[i].scale.x * (hp2.currentPose[i].scale.x - hp.currentPose[i].scale.x) * u;
            float y = hp.currentPose[i].scale.y * (hp2.currentPose[i].scale.y - hp.currentPose[i].scale.y) * u;
            float z = hp.currentPose[i].scale.z * (hp2.currentPose[i].scale.z - hp.currentPose[i].scale.z) * u;
            newPose.currentPose[i].scale = new Vector3(x, y, z);
        }
        return newPose;
    }

    //bilinear blend
    public HierarchicalPose BiLinear(HierarchicalPose pose0, HierarchicalPose pose1, HierarchicalPose pose2, HierarchicalPose pose3, float u, float u2)
    {
        return Lerp(Lerp(pose0, pose1, u), Lerp(pose2, pose3, u), u2);
    }

    //split blend
    public HierarchicalPose Split(HierarchicalPose samplePose, HierarchicalPose lhs, HierarchicalPose rhs)
    {

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {
            samplePose.currentPose[i].translation = lhs.currentPose[i].translation - rhs.currentPose[i].translation;
            samplePose.currentPose[i].orientation = lhs.currentPose[i].orientation - rhs.currentPose[i].orientation;
            samplePose.currentPose[i].scale = new Vector3
                (lhs.currentPose[i].scale.x / rhs.currentPose[i].scale.x,
                 lhs.currentPose[i].scale.y / rhs.currentPose[i].scale.y,
                 lhs.currentPose[i].scale.z / rhs.currentPose[i].scale.z);
        }

        return samplePose;
    }

    //scale blend
    public HierarchicalPose Scale(HierarchicalPose samplePose, HierarchicalPose nextPose, float t)
    {

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {

            samplePose.currentPose[i].scale = currentPose.currentPose[i].scale * (1 - t) + nextPose.currentPose[i].scale * t;
        }

        return samplePose;
    }

    //triangular blend
    public HierarchicalPose Trianglular(HierarchicalPose samplePose, HierarchicalPose currentPose, HierarchicalPose nextPose, HierarchicalPose nextNextPose, float t1, float t2)
    {

        float t0 = 1 - t1 - t2;

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {

            samplePose.currentPose[i].translation = t0 * currentPose.currentPose[i].translation + nextPose.currentPose[i].translation * t1 + nextNextPose.currentPose[i].translation * t2;
            samplePose.currentPose[i].orientation = t0 * currentPose.currentPose[i].orientation + nextPose.currentPose[i].orientation * t1 + nextNextPose.currentPose[i].orientation * t2;
            samplePose.currentPose[i].scale = t0 * currentPose.currentPose[i].scale + nextPose.currentPose[i].scale * t1 + nextNextPose.currentPose[i].scale * t2;
        }

        return samplePose;
    }

    //cubic blend
    public HierarchicalPose Cubic(HierarchicalPose preInit, HierarchicalPose init, HierarchicalPose final, HierarchicalPose postFinal, float u)
    {
        // ((-1/2)preInit + (3/2)init - (3/2)final + (1/2)postFinal)x^3 
        //  + (preInit - (5/2)init + 2final - 1/2postFinal)x^2
        //  + (-1/2 preInit + 1/2 final)x + init
        HierarchicalPose hPose = new HierarchicalPose(init.currentPose.Length);

        for (int i = 0; i < init.currentPose.Length; i++)
        {
            Vector3 translation = (1 / 2 * preInit.currentPose[i].translation + 3 / 2 * init.currentPose[i].translation + 1 / 2 * postFinal.currentPose[i].translation) * Mathf.Pow(u, 3)
                                + (preInit.currentPose[i].translation - 2 * final.currentPose[i].translation - 1 / 2 * final.currentPose[i].translation) * Mathf.Pow(u, 2)
                                + (-1 / 2 * preInit.currentPose[i].translation + 1 / 2 * final.currentPose[i].translation) * u + init.currentPose[i].translation;

            Vector3 orientation = (1 / 2 * preInit.currentPose[i].orientation + 3 / 2 * init.currentPose[i].orientation + 1 / 2 * postFinal.currentPose[i].orientation) * Mathf.Pow(u, 3)
                                + (preInit.currentPose[i].orientation - 2 * final.currentPose[i].orientation - 1 / 2 * final.currentPose[i].orientation) * Mathf.Pow(u, 2)
                                + (-1 / 2 * preInit.currentPose[i].orientation + 1 / 2 * final.currentPose[i].orientation) * u + init.currentPose[i].orientation;

            float scaleX = (1 / 2 * preInit.currentPose[i].scale.x * 3 / 2 * init.currentPose[i].scale.x * 1 / 2 * postFinal.currentPose[i].scale.x) * Mathf.Pow(u, 3)
                                * (preInit.currentPose[i].scale.x - 2 * final.currentPose[i].scale.x / 1 / 2 * final.currentPose[i].scale.x) * Mathf.Pow(u, 2)
                                * (-1 / 2 * preInit.currentPose[i].scale.x * 1 / 2 * final.currentPose[i].scale.x) * u + init.currentPose[i].scale.x;

            float scaleY = (1 / 2 * preInit.currentPose[i].scale.y * 3 / 2 * init.currentPose[i].scale.y * 1 / 2 * postFinal.currentPose[i].scale.y) * Mathf.Pow(u, 3)
                                * (preInit.currentPose[i].scale.y - 2 * final.currentPose[i].scale.y / 1 / 2 * final.currentPose[i].scale.y) * Mathf.Pow(u, 2)
                                * (-1 / 2 * preInit.currentPose[i].scale.y * 1 / 2 * final.currentPose[i].scale.y) * u + init.currentPose[i].scale.y;

            float scaleZ = (1 / 2 * preInit.currentPose[i].scale.z * 3 / 2 * init.currentPose[i].scale.z * 1 / 2 * postFinal.currentPose[i].scale.z) * Mathf.Pow(u, 3)
                                * (preInit.currentPose[i].scale.z - 2 * final.currentPose[i].scale.z / 1 / 2 * final.currentPose[i].scale.z) * Mathf.Pow(u, 2)
                                * (-1 / 2 * preInit.currentPose[i].scale.z * 1 / 2 * final.currentPose[i].scale.z) * u + init.currentPose[i].scale.z;

            Vector3 scale = new Vector3(scaleX, scaleY, scaleZ);
            SpatialPose newPose = new SpatialPose();

            newPose.translation = translation;
            newPose.orientation = orientation;
            newPose.scale = scale;
            hPose.currentPose[i] = newPose;
        }
        Debug.Log("cubic");
        return hPose;
    }

    //bicubic blend
    public HierarchicalPose BiCubic(HierarchicalPose preInit, HierarchicalPose init, HierarchicalPose final, HierarchicalPose postFinal, float u)
    {
        // ((-1/2)preInit + (3/2)init - (3/2)final + (1/2)postFinal)x^3 
        //  + (preInit - (5/2)init + 2final - 1/2postFinal)x^2
        //  + (-1/2 preInit + 1/2 final)x + init
        HierarchicalPose hPose = new HierarchicalPose(init.currentPose.Length);

        for (int i = 0; i < init.currentPose.Length; i++)
        {
            Vector3 translation = (1 / 2 * preInit.currentPose[i].translation + 3 / 2 * init.currentPose[i].translation + 1 / 2 * postFinal.currentPose[i].translation) * Mathf.Pow(u, 3)
                                + (preInit.currentPose[i].translation - 2 * final.currentPose[i].translation - 1 / 2 * final.currentPose[i].translation) * Mathf.Pow(u, 2)
                                + (-1 / 2 * preInit.currentPose[i].translation + 1 / 2 * final.currentPose[i].translation) * u + init.currentPose[i].translation;
            Vector3 orientation = (1 / 2 * preInit.currentPose[i].orientation + 3 / 2 * init.currentPose[i].orientation + 1 / 2 * postFinal.currentPose[i].orientation) * Mathf.Pow(u, 3)
                                + (preInit.currentPose[i].orientation - 2 * final.currentPose[i].orientation - 1 / 2 * final.currentPose[i].orientation) * Mathf.Pow(u, 2)
                                + (-1 / 2 * preInit.currentPose[i].orientation + 1 / 2 * final.currentPose[i].orientation) * u + init.currentPose[i].orientation;
            float scaleX = (1 / 2 * preInit.currentPose[i].scale.x * 3 / 2 * init.currentPose[i].scale.x * 1 / 2 * postFinal.currentPose[i].scale.x) * Mathf.Pow(u, 3)
                                * (preInit.currentPose[i].scale.x - 2 * final.currentPose[i].scale.x / 1 / 2 * final.currentPose[i].scale.x) * Mathf.Pow(u, 2)
                                * (-1 / 2 * preInit.currentPose[i].scale.x * 1 / 2 * final.currentPose[i].scale.x) * u + init.currentPose[i].scale.x;
            float scaleY = (1 / 2 * preInit.currentPose[i].scale.y * 3 / 2 * init.currentPose[i].scale.y * 1 / 2 * postFinal.currentPose[i].scale.y) * Mathf.Pow(u, 3)
                                * (preInit.currentPose[i].scale.y - 2 * final.currentPose[i].scale.y / 1 / 2 * final.currentPose[i].scale.y) * Mathf.Pow(u, 2)
                                * (-1 / 2 * preInit.currentPose[i].scale.y * 1 / 2 * final.currentPose[i].scale.y) * u + init.currentPose[i].scale.y;
            float scaleZ = (1 / 2 * preInit.currentPose[i].scale.z * 3 / 2 * init.currentPose[i].scale.z * 1 / 2 * postFinal.currentPose[i].scale.z) * Mathf.Pow(u, 3)
                                * (preInit.currentPose[i].scale.z - 2 * final.currentPose[i].scale.z / 1 / 2 * final.currentPose[i].scale.z) * Mathf.Pow(u, 2)
                                * (-1 / 2 * preInit.currentPose[i].scale.z * 1 / 2 * final.currentPose[i].scale.z) * u + init.currentPose[i].scale.z;
            Vector3 scale = new Vector3(scaleX, scaleY, scaleZ);
            SpatialPose newPose = new SpatialPose();
            newPose.translation = translation;
            newPose.orientation = orientation;
            newPose.scale = scale;
            hPose.currentPose[i] = newPose;
        }
        return hPose;
    }

    //identity blend
    public HierarchicalPose Identity()
    {

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {
            SpatialPose temp = new SpatialPose(Matrix4x4.identity, Vector3.one, Vector3.one, Vector3.zero);
            samplePose.currentPose[i] = temp;
        }

        return samplePose;
    }

    //construct blend
    public HierarchicalPose Construct(HierarchicalPose controlPose, HierarchicalPose plusPose)
    {

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {
            samplePose.currentPose[i].translation = controlPose.currentPose[i].translation + plusPose.currentPose[i].translation;
            samplePose.currentPose[i].orientation = controlPose.currentPose[i].orientation + plusPose.currentPose[i].orientation;
            samplePose.currentPose[i].scale = controlPose.currentPose[i].scale + plusPose.currentPose[i].scale;
        }

        //controlPose.worldPose += plusPose.worldPose;
        return samplePose;
    }

    //copy blend
    public HierarchicalPose Copy(HierarchicalPose copy)
    {

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {
            samplePose.currentPose[i] = copy.currentPose[i];
        }

        return samplePose;
    }

    //invert blend
    public HierarchicalPose InvertPose(HierarchicalPose inPose)
    {

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {
            samplePose.currentPose[i].translation = inPose.currentPose[i].translation * -1;
            samplePose.currentPose[i].orientation = inPose.currentPose[i].orientation * -1;
            samplePose.currentPose[i].scale = inPose.currentPose[i].scale * -1;
        }

        return samplePose;
    }
    /*
    public HierarchicalPose Cubic(float t)
    {

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {


            Vector3 a = 2 * currentPose.currentPose[i].translation;
            Vector3 b = nextPose.currentPose[i].translation - currentPose.currentPose[i].translation;
            Vector3 c = 2 * previousPose.currentPose[i].translation - 5 * currentPose.currentPose[i].translation + 4 * nextPose.currentPose[i].translation - nextNextPose.currentPose[i].translation;
            Vector3 d = -1 * previousPose.currentPose[i].translation + 3 * currentPose.currentPose[i].translation - 3 * nextPose.currentPose[i].translation + nextNextPose.currentPose[i].translation;

            Vector3 final = .5f * (a + (b * t) + (c * t * t) + (d * t * t * t));

            samplePose.currentPose[i].translation = final;
        }

        return samplePose;
    }
    */

    //Euler Intergration
    public Vector3 EulerIntergration(Vector3 currentPos, Vector3 vectorChange)
    {

        Vector3 temp = currentPos;
        temp = currentPos + Time.deltaTime * vectorChange;
        return temp;
    }

    public void EulerIntergration2(Vector3 vectorChange, bool thisWASD, bool thisIKJL)
    {

        if (thisWASD)
        {

            player.transform.position = EulerIntergration(player.transform.position, vectorChange);
        }

        if (thisIKJL)
        {

            player.transform.rotation = Quaternion.Euler(EulerIntergration(player.transform.rotation.eulerAngles, vectorChange));
        }

    }

    //Kinematic Intergration
    public Vector3 KinematicIntergration(Vector3 currentPos, Vector3 vectorChange, Vector3 accelerationChange)
    {

        Vector3 temp = currentPos;
        temp = currentPos + Time.deltaTime * vectorChange;
        temp = temp + accelerationChange * Time.deltaTime * Time.deltaTime * .5f;
        return temp;
    }

    public void KinematicIntergration2(Vector3 vectorChange, Vector3 accelerationChange, bool thisWASD, bool thisIKJL)
    {

        if (thisWASD)
        {

            player.transform.position = KinematicIntergration(player.transform.position, vectorChange, accelerationChange);
        }

        if (thisIKJL)
        {

            player.transform.rotation = Quaternion.Euler(KinematicIntergration(player.transform.rotation.eulerAngles, vectorChange, accelerationChange));
        }
    }

    //Interpolation Intergration
    public Vector3 InterpolationIntergration(Vector3 currentPos, Vector3 defualtPos, float t)
    {

        Vector3 temp = currentPos;
        Vector3 offest = currentPos + defualtPos;
        temp = currentPos + (offest - currentPos) * t;
        return temp;
    }

    public void InterpolationIntergration2(Vector3 defualtPos, float t, bool thisWASD, bool thisIKJL)
    {

        if (thisWASD)
        {

            player.transform.position = InterpolationIntergration(player.transform.position, defualtPos, t);
        }

        if (thisIKJL)
        {

            player.transform.rotation = Quaternion.Euler(InterpolationIntergration(player.transform.rotation.eulerAngles, defualtPos, t));
        }
    }

    //Quadratic Bezier Intergration
    public Vector3 SecondInterpolationIntergration(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {

        Vector3 temp = ((1 - t) * (1 - t)) * p0 + 2 * (1 - t) * t * p1 + t * t * p2;
        Vector3 tempD = 2 * (1 - t) * (p1 - p0) + 2 * t * (p2 - p1);
        Vector3 tempD2 = 2 * (p2 - 2 * p1 + p0);

        return temp;
    }

    public void SecondInterpolationIntergration2(Vector3 p1, Vector3 p2, float t)
    {

        player.transform.position = SecondInterpolationIntergration(player.transform.position, p1, p2, t);
    }

    //Direct Control
    public void Direct(bool thisWASD, bool thisIKJL)
    {

        if (thisWASD)
        {

            if (Input.GetKey(KeyCode.W))
            {

                player.transform.position += new Vector3(1,0,0);
            }

            if (Input.GetKey(KeyCode.S))
            {

                player.transform.position += new Vector3(-1, 0, 0);
            }

            if (Input.GetKey(KeyCode.A))
            {

                player.transform.position += new Vector3(0, 0, -1);
            }

            if (Input.GetKey(KeyCode.D))
            {

                player.transform.position += new Vector3(0, 0, 1);
            }
        }

        if (thisIKJL)
        {

            if (Input.GetKey(KeyCode.J))
            {

                player.transform.rotation *= Quaternion.Euler(new Vector3(0, -1, 0));
            }

            if (Input.GetKey(KeyCode.L))
            {

                player.transform.rotation *= Quaternion.Euler(new Vector3(0, 1, 0));
            }
        }
    }

    //Control Velocity
    public void ControlVelocity(bool thisWASD, bool thisIKJL)
    {

        if (thisWASD)
        {

            if (Input.GetKey(KeyCode.W))
            {

                EulerIntergration2( new Vector3(1, 0, 0), thisWASD, false);
            }

            if (Input.GetKey(KeyCode.S))
            {

                EulerIntergration2( new Vector3(-1, 0, 0), thisWASD, false);
            }

            if (Input.GetKey(KeyCode.A))
            {

                EulerIntergration2( new Vector3(0, 0, -1), thisWASD, false);
            }

            if (Input.GetKey(KeyCode.D))
            {

                EulerIntergration2( new Vector3(0, 0, 1), thisWASD, false);
            }
        }

        if (thisIKJL)
        {

            if (Input.GetKey(KeyCode.J))
            {

                EulerIntergration2( new Vector3(0, -1, 0), false, thisIKJL);
            }

            if (Input.GetKey(KeyCode.L))
            {

                EulerIntergration2( new Vector3(0, 1, 0), false, thisIKJL);
            }
        }
    }

    //Control Accerleration
    public void ControlAcceleration(bool thisWASD, bool thisIKJL)
    {

        if (thisWASD)
        {

            if (Input.GetKey(KeyCode.W))
            {

                KinematicIntergration2( new Vector3(velocityF,0,0), new Vector3(accelerationF, 0, 0), thisWASD, false);
                velocityF = EulerIntergration(new Vector3(velocityF, 0, 0), new Vector3(accelerationF, 0, 0)).x;
            }

            if (Input.GetKey(KeyCode.S))
            {

                KinematicIntergration2(new Vector3(-1 * velocityF, 0, 0), new Vector3(-1 * accelerationF, 0, 0), thisWASD, false);
                velocityF = EulerIntergration(new Vector3(-1 * velocityF, 0, 0), new Vector3(-1 * accelerationF, 0, 0)).x;
            }

            if (Input.GetKey(KeyCode.A))
            {

                KinematicIntergration2(new Vector3(0, 0, -1 * velocityF), new Vector3(0, 0, -1 * accelerationF), thisWASD, false);
                velocityF = EulerIntergration(-1 * new Vector3(0, 0, -1 * velocityF), new Vector3(0, 0, -1 * accelerationF)).z;
            }

            if (Input.GetKey(KeyCode.D))
            {

                KinematicIntergration2(new Vector3(0, 0, velocityF), new Vector3(0, 0, accelerationF), thisWASD, false);
                velocityF = EulerIntergration(new Vector3(0, 0, velocityF), new Vector3(0, 0, accelerationF)).z;
            }
        }

        if (thisIKJL)
        {

            if (Input.GetKey(KeyCode.J))
            {

                KinematicIntergration2(new Vector3(0, angularVelocityF, 0), new Vector3(0, angularAccelerationF, 0), false, thisIKJL);
                angularVelocityF = EulerIntergration(new Vector3(0, angularVelocityF, 0), new Vector3(0, angularAccelerationF, 0)).y;
            }

            if (Input.GetKey(KeyCode.L))
            {

                KinematicIntergration2(new Vector3(0, -1 * angularVelocityF, 0), new Vector3(0, -1 * angularAccelerationF, 0), false, thisIKJL);
                angularVelocityF = EulerIntergration(new Vector3(0, -1 * angularVelocityF, 0), new Vector3(0, -1 * angularAccelerationF, 0)).y;
            }
        }
    }

    //Fake velocity
    public void FakeVelocity(bool thisWASD, bool thisIKJL,float t)
    {

        if (thisWASD)
        {

            if (Input.GetKey(KeyCode.W))
            {

                InterpolationIntergration2( new Vector3(offsetF,0,0), t, thisWASD, false);
            }

            if (Input.GetKey(KeyCode.S))
            {

                InterpolationIntergration2(new Vector3(-1 * offsetF, 0, 0), t, thisWASD, false);
            }

            if (Input.GetKey(KeyCode.A))
            {

                InterpolationIntergration2(new Vector3(0, 0, -1 * offsetF), t, thisWASD, false);
            }

            if (Input.GetKey(KeyCode.D))
            {

                InterpolationIntergration2(new Vector3(0, 0, offsetF), t, thisWASD, false);
            }
        }

        if (thisIKJL)
        {

            if (Input.GetKey(KeyCode.J))
            {

                InterpolationIntergration2(new Vector3(0, -1 * angularOffsetF, 0), t, false, thisIKJL);
            }

            if (Input.GetKey(KeyCode.L))
            {

                InterpolationIntergration2(new Vector3(0, angularOffsetF, 0), t, false, thisIKJL);
            }
        }
    }

    //fake acceleration
    public void FakeAcceleration(bool thisWASD, bool thisIKJL, float t)
    {

        if (thisWASD)
        {

            if (Input.GetKey(KeyCode.W))
            {

                EulerIntergration2(new Vector3(velocityF, 0, 0), thisWASD, false);
                velocityF = InterpolationIntergration(new Vector3(velocityF, 0, 0), new Vector3(offsetF, 0, 0), t).x;
            }

            if (Input.GetKey(KeyCode.S))
            {

                EulerIntergration2(new Vector3(-1 * velocityF, 0, 0), thisWASD, false);
                velocityF = InterpolationIntergration(new Vector3(-1 * velocityF, 0, 0), new Vector3(-1 * offsetF, 0, 0), t).x;
            }

            if (Input.GetKey(KeyCode.A))
            {

                EulerIntergration2(new Vector3(0, 0, -1 * velocityF), thisWASD, false);
                velocityF = InterpolationIntergration(new Vector3(0, 0, -1 * velocityF), new Vector3(0, 0, -1 * offsetF), t).z;
            }

            if (Input.GetKey(KeyCode.D))
            {

                EulerIntergration2(new Vector3(0, 0, velocityF), thisWASD, false);
                velocityF = InterpolationIntergration(new Vector3(0, 0, velocityF), new Vector3(0, 0, offsetF), t).z;
            }
        }

        if (thisIKJL)
        {

            if (Input.GetKey(KeyCode.J))
            {

                EulerIntergration2(new Vector3(0, angularVelocityF, 0), false, thisIKJL);
                angularVelocityF = InterpolationIntergration(new Vector3(0, angularVelocityF, 0), new Vector3(0, angularOffsetF, 0), t).y;
            }

            if (Input.GetKey(KeyCode.L))
            {

                EulerIntergration2(new Vector3(0, -1 * angularVelocityF, 0), false, thisIKJL);
                angularVelocityF = InterpolationIntergration(new Vector3(0, -1 * angularVelocityF, 0), new Vector3(0, -1 * angularOffsetF, 0), t).y;
            }
        }
    }

    void Update()
    {
        
        if(choice == integration.direct)
        {

            Direct(true, true);
            mode = "Direct";
        }

        if (choice == integration.velocity)
        {

            ControlVelocity(true, true);
            mode = "velocity";
        }

        if (choice == integration.acceleration)
        {

            ControlAcceleration(true, true);
            mode = "acceleration";
        }

        if (choice == integration.fVelocity)
        {

            FakeVelocity(true, true, u);
            mode = "fake velocity";
        }

        if (choice == integration.fAcceleration)
        {

            FakeAcceleration(true, true, u);
            mode = "fake accleration";
        }

        if (choice == integration.bonus)
        {

            SecondInterpolationIntergration2(new Vector3(velocityF, 0, 0), new Vector3(0, 0, accelerationF), u);
            velocityF = EulerIntergration(new Vector3(velocityF, 0, 0), new Vector3(accelerationF, 0, 0)).x;
            mode = "bonus demo";
        }

    }
}