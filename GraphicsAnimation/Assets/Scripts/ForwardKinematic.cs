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

    public HierarchicalPose samplePose;

    public void Nearest(HierarchicalPose hp, HierarchicalPose hp2, float t)
    {

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {

            if (t < .5f)
            {

                samplePose.currentPose[i].translation = hp.currentPose[i].translation;
                samplePose.currentPose[i].orientation = hp.currentPose[i].orientation;
                samplePose.currentPose[i].scale = hp.currentPose[i].scale;
            }

            else
            {

                samplePose.currentPose[i].translation = hp2.currentPose[i].translation;
                samplePose.currentPose[i].orientation = hp2.currentPose[i].orientation;
                samplePose.currentPose[i].scale = hp2.currentPose[i].scale;
            }
        }
    }

    public void Linear(HierarchicalPose hp, HierarchicalPose hp2, float t)
    {

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {

                samplePose.currentPose[i].translation = (1 - t) * hp.currentPose[i].translation + hp2.currentPose[i].translation * t;
                samplePose.currentPose[i].orientation = (1 - t) * hp.currentPose[i].orientation + hp2.currentPose[i].orientation * t;
                samplePose.currentPose[i].scale = (1 - t) * hp.currentPose[i].scale + hp2.currentPose[i].scale * t;
        }
    }

    public void Step(HierarchicalPose hp, HierarchicalPose hp2, float t)
    {

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {

            samplePose.currentPose[i].translation = hp.currentPose[i].translation;
            samplePose.currentPose[i].orientation = hp.currentPose[i].orientation;
            samplePose.currentPose[i].scale = hp.currentPose[i].scale;

        }
    }

    public void Smoothstep(HierarchicalPose hp, HierarchicalPose hp2, float t)
    {

        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {

            float alpha = t * t * (3 - 2 * t);

            samplePose.currentPose[i].translation = hp.currentPose[i].translation * alpha + (hp2.currentPose[i].translation * (1 - alpha));
            samplePose.currentPose[i].orientation = hp.currentPose[i].orientation * alpha + (hp2.currentPose[i].orientation * (1 - alpha));
            samplePose.currentPose[i].scale = hp.currentPose[i].scale * alpha + (hp2.currentPose[i].scale * (1 - alpha));
        }
    }
}
