/*
File name: KeyframePool.cs
Purpose: Holds an unsorted and unordered collection of keyframes 
Contributors: Nick Brennan-Martin and Bradley Chamberlain
Collaborated on one PC
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyframePool : MonoBehaviour
{
    // Start is called before the first frame update
    int frameCount;
    public Keyframe[] keyframePool;

    public KeyframePool(int newFrameCount, Keyframe[] keyframes)
    {
        frameCount = newFrameCount;
        keyframePool = keyframes;
    }

    public KeyframePool()
    {
        frameCount = 256;
        keyframePool = new Keyframe[frameCount];
    }

    ~KeyframePool()
    {
        //keyframePool
    }
}
