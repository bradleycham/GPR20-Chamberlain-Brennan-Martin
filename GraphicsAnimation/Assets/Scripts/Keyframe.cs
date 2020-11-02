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
    public HierarchyState keyframeState;
    //public HierarchicalPose EndFrame;

    // Constructor
    public Keyframe()
    {
        index = 0;
        duration = 0.01f;
        durationInv = 1 / duration;
        //data = null;
    }
    // Constructor Overload 
    public Keyframe(int newIndex, float newDuration, HierarchyState poseStart)
    {
        index = newIndex;
        duration = newDuration;
        durationInv = 1 / duration;
        keyframeState = poseStart;
        //EndFrame = poseEnd;
    }

    // set index in pool
    public void SetIndex(int i)
    {
        index = i;
    }

    // distribute data
    public void SetData(HierarchyState poseStart)
    {
        keyframeState = poseStart;
        
    }
    public HierarchyState GetHierarchyState()
    {
        return keyframeState;
    }

    // set frame duration
    public void SetDuration(float newDuration)
    {
        duration = newDuration;
        durationInv = 1 / duration;
    }

    public float GetDuration()
    {
        return duration;
    }


}
