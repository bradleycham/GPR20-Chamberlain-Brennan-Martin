using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyframePool : MonoBehaviour
{
    // Start is called before the first frame update
    int frameCount;
    public Keyframe[] keyframePool;

    public KeyframePool(int newFrameCount)
    {
        frameCount = newFrameCount;
        keyframePool = new Keyframe[frameCount];
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
