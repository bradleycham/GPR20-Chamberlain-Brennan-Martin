using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clip : MonoBehaviour
{
    public string clipName;
    public int clipIndex;
    public float clipDuration; // can be calc. as a sum of frames or set at start
    public float durationInv;
    public int frameCount;
    public int firstIndex;//first and last frames
    public int lastIndex;
    public Keyframe[] framePool;

    public Clip()
    {

    }

    public Clip(string name, int first, int last)
    {
        clipName = name;
        firstIndex = first;
        lastIndex = last;

        clipDuration = 1.0f;
        durationInv = 1.0f / clipDuration;
        frameCount = 2;       
    }
    // distribute time
    //
    ~Clip()
    {

    }
}
