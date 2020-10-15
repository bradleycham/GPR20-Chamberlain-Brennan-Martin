﻿/*
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

    public HierarchicalPose Lerp(float t)
    {

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {

            samplePose.currentPose[i].translation = (1 - t) * currentPose.currentPose[i].translation + nextPose.currentPose[i].translation * t;
            samplePose.currentPose[i].orientation = (1 - t) * currentPose.currentPose[i].orientation + nextPose.currentPose[i].orientation * t;
            samplePose.currentPose[i].scale = (1 - t) * currentPose.currentPose[i].scale + nextPose.currentPose[i].scale * t;
        }

        return samplePose;
    }

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
}
