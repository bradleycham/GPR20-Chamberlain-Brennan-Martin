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
    //float clipTime; // current time between 0 and clip duraton
    public float clipDuration; // can be calc. as a sum of frames or set at start
    float durationInv; // 1/duration
    
    public int frameCount;

    int firstIndex;//first and last frames
    int lastIndex;

    public Vector2[] startEndTimes;

    public ClipTransition EndTransition;
    public ClipTransition ReverseTransition;

    public int[] frameSequence;
    public KeyframePool keyframePool;

    public float GetClipDuration()
    {
        return clipDuration;
    }
    public int GetClipIndex()
    {
        return clipIndex;
    }
    void Start()
    {
        CalculateDuration();
    }
    // default constructor
    public Clip()
    {
        clipName = "null";
    }

    //constructor overload
    public Clip(string name, int first, int last, KeyframePool pool)
    {
        keyframePool = pool;

        clipName = name;
        firstIndex = first;
        lastIndex = last;

        clipDuration = 1.0f;
        durationInv = 1.0f / clipDuration;
              
    }

    // set the duration of this clip
    public void SetDuration(float newDuration)
    {
        clipDuration = newDuration;
    }

    // calculate the duration of the clip via the frame lengths
    public void CalculateDuration()
    {
        startEndTimes = new Vector2[frameCount];
        float cumulativeTime = 0.0f;
        for (int i = 0; i < frameCount; i++)
        {
            startEndTimes[i].x = cumulativeTime;
            cumulativeTime += keyframePool.framePool[frameSequence[i]].duration;
            startEndTimes[i].y = cumulativeTime;
        }
        clipDuration = cumulativeTime;
    }
}
