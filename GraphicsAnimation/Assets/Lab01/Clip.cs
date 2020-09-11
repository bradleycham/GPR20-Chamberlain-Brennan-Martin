/*
File name: Clip.cs
Purpose: Data of a clip 
Contributors: Nick Brennan-Martin and Bradley Chamberlain
Collaborated on one PC
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clip : MonoBehaviour
{
    public string clipName; // identifies this clip
    public int clipIndex; // index of this clip in a pool of clips
    public float clipTime; // current time between 0 and clip duraton
    public float clipDuration; // can be calc. as a sum of frames or set at start
    public float durationInv; // 1/duration
    public int frameCount;
    public int firstIndex;//first and last frames
    public int lastIndex;

    public int[] frameSequence;
    public KeyframePool keyframePool;
    public Clip()
    {
        clipName = "null";
    }

    public Clip(string name, int first, int last, KeyframePool pool)
    {
        keyframePool = pool;

        clipName = name;
        firstIndex = first;
        lastIndex = last;

        clipDuration = 1.0f;
        durationInv = 1.0f / clipDuration;
        frameCount = lastIndex + 1;       
    }
    // distribute time
    //
    public void SetDuration(float newDuration)
    {
        clipDuration = newDuration;
    }
    ~Clip()
    {

    }
}
