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

    //public void Nearest(HierarchicalPose hp, HierarchicalPose hp2, float t)
    //{

    //    for (int i = 0; i < samplePose.currentPose.Length; i++)
    //    {

    //        if (t < .5f)
    //        {

    //            samplePose.currentPose[i].translation = hp.currentPose[i].translation;
    //            samplePose.currentPose[i].orientation = hp.currentPose[i].orientation;
    //            samplePose.currentPose[i].scale = hp.currentPose[i].scale;
    //        }

    //        else
    //        {

    //            samplePose.currentPose[i].translation = hp2.currentPose[i].translation;
    //            samplePose.currentPose[i].orientation = hp2.currentPose[i].orientation;
    //            samplePose.currentPose[i].scale = hp2.currentPose[i].scale;
    //        }
    //    }
    //}

    ////lerp
    //public void Linear(HierarchicalPose hp, HierarchicalPose hp2, float t)
    //{

    //    for (int i = 0; i < samplePose.currentPose.Length; i++)
    //    {

    //            samplePose.currentPose[i].translation = (1 - t) * hp.currentPose[i].translation + hp2.currentPose[i].translation * t;
    //            samplePose.currentPose[i].orientation = (1 - t) * hp.currentPose[i].orientation + hp2.currentPose[i].orientation * t;
    //            samplePose.currentPose[i].scale = (1 - t) * hp.currentPose[i].scale + hp2.currentPose[i].scale * t;
    //    }
    //}

    //public void Step(HierarchicalPose hp, HierarchicalPose hp2, float t)
    //{

    //    for (int i = 0; i < samplePose.currentPose.Length; i++)
    //    {

    //        samplePose.currentPose[i].translation = hp.currentPose[i].translation;
    //        samplePose.currentPose[i].orientation = hp.currentPose[i].orientation;
    //        samplePose.currentPose[i].scale = hp.currentPose[i].scale;

    //    }
    //}

    //public void Smoothstep(HierarchicalPose hp, HierarchicalPose hp2, float t)
    //{

    //    for (int i = 0; i < samplePose.currentPose.Length; i++)
    //    {

    //        float alpha = t * t * (3 - 2 * t);

    //        samplePose.currentPose[i].translation = hp.currentPose[i].translation * alpha + (hp2.currentPose[i].translation * (1 - alpha));
    //        samplePose.currentPose[i].orientation = hp.currentPose[i].orientation * alpha + (hp2.currentPose[i].orientation * (1 - alpha));
    //        samplePose.currentPose[i].scale = hp.currentPose[i].scale * alpha + (hp2.currentPose[i].scale * (1 - alpha));
    //    }
    //}

    public HierarchicalPose Concat()
    {

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {
            samplePose.currentPose[i].translation = lhs.currentPose[i].translation + rhs.currentPose[i].translation;
            samplePose.currentPose[i].orientation = lhs.currentPose[i].orientation + rhs.currentPose[i].orientation;
            samplePose.currentPose[i].scale =  new Vector3
                (lhs.currentPose[i].scale.x * rhs.currentPose[i].scale.x,
                 lhs.currentPose[i].scale.y * rhs.currentPose[i].scale.y,
                 lhs.currentPose[i].scale.z * rhs.currentPose[i].scale.z);
        }

        return samplePose;
    }

    public HierarchicalPose Near(float t)
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

            if(t2 < 0.5f)
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
    public HierarchicalPose BiLinear(HierarchicalPose pose0, HierarchicalPose pose1, HierarchicalPose pose2, HierarchicalPose pose3, float u, float u2)
    {
        return Lerp(Lerp(pose0, pose1, u), Lerp(pose2, pose3, u), u2);
    }

    public HierarchicalPose Split()
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

    public HierarchicalPose Scale(float t)
    {

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {

            samplePose.currentPose[i].scale = currentPose.currentPose[i].scale * (1 - t) + nextPose.currentPose[i].scale * t;
        }

        return samplePose;
    }

    public HierarchicalPose Trianglular(float t1, float t2)
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


    public HierarchicalPose Cubic(HierarchicalPose preInit, HierarchicalPose init, HierarchicalPose final, HierarchicalPose postFinal, float u)
    {
        // ((-1/2)preInit + (3/2)init - (3/2)final + (1/2)postFinal)x^3 
        //  + (preInit - (5/2)init + 2final - 1/2postFinal)x^2
        //  + (-1/2 preInit + 1/2 final)x + init
        HierarchicalPose hPose = new HierarchicalPose(init.currentPose.Length);
        
        for (int i =0; i< init.currentPose.Length; i++)
        {
            Vector3 translation = (1 / 2 * preInit.currentPose[i].translation + 3 / 2 * init.currentPose[i].translation + 1 / 2 * postFinal.currentPose[i].translation) * Mathf.Pow(u, 3)
                                + (preInit.currentPose[i].translation - 2 * final.currentPose[i].translation - 1 / 2 * final.currentPose[i].translation) * Mathf.Pow(u, 2)
                                + (-1 / 2 * preInit.currentPose[i].translation + 1 / 2 * final.currentPose[i].translation) * u + init.currentPose[i].translation;
            
            Vector3 orientation = (1 / 2 * preInit.currentPose[i].orientation + 3 / 2 * init.currentPose[i].orientation + 1 / 2 * postFinal.currentPose[i].orientation) * Mathf.Pow(u, 3)
                                + (preInit.currentPose[i].orientation - 2 * final.currentPose[i].orientation - 1 / 2 * final.currentPose[i].orientation) * Mathf.Pow(u, 2)
                                + (-1 / 2 * preInit.currentPose[i].orientation + 1 / 2 * final.currentPose[i].orientation) * u + init.currentPose[i].orientation;
            
            float scaleX =      (1 / 2 * preInit.currentPose[i].scale.x * 3 / 2 * init.currentPose[i].scale.x * 1 / 2 * postFinal.currentPose[i].scale.x) * Mathf.Pow(u, 3)
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
}
