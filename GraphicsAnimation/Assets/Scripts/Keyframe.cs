/*
File name: KeyframePool.cs
Purpose: Data of a keyframe 
Contributors: Nick Brennan-Martin and Bradley Chamberlain
Collaborated on one PC
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyframe : MonoBehaviour
{
    public int index;
    public float duration;
    public float durationInv;
    public HierarchicalPose FrameStart;
    public HierarchicalPose EndFrame;

    // Constructor
    public Keyframe()
    {
        index = 0;
        duration = 0.01f;
        durationInv = 1 / duration;
        //data = null;
    }
    // Constructor Overload 
    public Keyframe(int newIndex, float newDuration, HierarchicalPose poseStart, HierarchicalPose poseEnd)
    {
        index = newIndex;
        duration = newDuration;
        durationInv = 1 / duration;
        FrameStart = poseStart;
        EndFrame = poseEnd;
    }

    // set index in pool
    public void SetIndex(int i)
    {
        index = i;
    }

    // distribute data
    public void SetData(HierarchicalPose poseStart, HierarchicalPose poseEnd)
    {
        FrameStart = poseStart;
        EndFrame = poseEnd;
    }

    // set frame duration
    public void SetDuration(float newDuration)
    {
        duration = newDuration;
        durationInv = 1 / duration;
    }


}
