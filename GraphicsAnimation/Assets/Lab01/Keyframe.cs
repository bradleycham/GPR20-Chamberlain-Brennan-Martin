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
    public int data;

    // Constructor
    public Keyframe()
    {
        index = 0;
        duration = 0.01f;
        durationInv = 1 / duration;
        data = 1;
    }
    // Constructor Overload 
    public Keyframe(int newIndex, float newDuration, int newData)
    {
        index = newIndex;
        duration = newDuration;
        durationInv = 1 / duration;
        data = newData;
    }

    // set index in pool
    public void SetIndex(int i)
    {
        index = i;
    }

    // distribute data
    public void SetData(int i)
    {
        data = i;
    }

    // set frame duration
    public void SetDuration(float newDuration)
    {
        duration = newDuration;
        durationInv = 1 / duration;
    }


}
