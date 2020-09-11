/*
File name: ClipController.cs
Purpose:  This data structure is the interface between the user
and the clip that they are controlling. It allows the user
to change attributes of the clip such as playback direction, 
speed, and frame info.
Contributors: Nick Brennan-Martin and Bradley Chamberlain
Collaborated on one PC
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipController : MonoBehaviour
{
    public ClipPool pool;
    public Clip clip;

    public string controllerName;

    public int clipIndex;
    public float clipDuration;
    public float clipTime; // time that has passed since start
    float clipParameter;
    
    public int frameIndex; //changes throughout the update
    public float frameTime;
    float frameParameter;

    public int playDirection;
    float frameOvershoot;

    public float timeScalar = 1.0f;

    // Default Contstructor
    public ClipController()
    {
        controllerName = "INACTIVE";
        pool = null;
        clipIndex = 0;
        clipTime = 0.0f;
        clipParameter = 0.0f;
        clip = null;

        frameIndex = 0;
        frameTime = 0.0f;
        frameParameter = 0.0f;

        playDirection = 1;
    }

    // Constructor overload
    public ClipController(int newClipIndex, int startFrame, int playState, ClipPool newPool)
    {
        pool = newPool;
        clipIndex = newClipIndex;
        //frameIndex = startFrame;
        playDirection = playState;
    }

    void Update()
    {
        // apply time step
        UpdateTime();
        ResolveTime();
        
        // post
        clipParameter = clipTime / clip.clipDuration;
        float v = frameTime / clip.keyframePool.framePool[clip.frameSequence[frameIndex]].duration;
        frameParameter = v;
        //DebugList();

        // create looping feature
    }

    // determine the current frame and time within
    public void ResolveTime()
    {
        if (frameParameter > 1.0 && playDirection > 0) // moving forward and the frame ended
        {
            // this is the amount of time that the dx went over the last keyframe
            frameOvershoot = (frameParameter - 1.0f) * clip.keyframePool.framePool[clip.frameSequence[frameIndex]].duration; 

            if (frameIndex == clip.frameCount - 1)
            {
                frameIndex = 0;
                frameTime = 0.0f;
                clipTime = 0.0f;
                clipTime += frameOvershoot;
                frameTime += frameOvershoot;
            }
            else if (frameIndex < clip.frameCount)
            {
                frameIndex++;
                frameTime = 0.0f; // frame condition fixed
                frameTime += frameOvershoot;
            }
            //Debug.Log(frameOvershoot);
        }


        if (frameParameter < 0.0 && playDirection < 0) // moving backwards and the frame ended
        {
            frameOvershoot = (0.0f - frameParameter) * clip.keyframePool.framePool[clip.frameSequence[frameIndex]].duration;
            if (frameIndex > 0)
            {       
                frameIndex--;
                frameTime = clip.keyframePool.framePool[clip.frameSequence[frameIndex]].duration; // frame condition fixed
                frameTime += frameOvershoot; // one frame condition fixed
            }
            if (frameIndex == 0)
            {                             
                //clipOvershoot = clipTime - clipDuration;
                frameIndex = clip.frameCount-1;
                clipTime = clipDuration;
                frameTime = clip.keyframePool.framePool[clip.frameSequence[frameIndex]].duration;
            }
            //Debug.Log(frameOvershoot);
        }
    }    

    // Update Function
    private void UpdateTime()
    {
        if(playDirection > 0)
        {
            // forward
            clipTime += Time.deltaTime * timeScalar;
            frameTime += Time.deltaTime * timeScalar;
        }
        else if(playDirection == 0)
        {
            // stop
        }
        else
        {
            // rewind
            if(playDirection < 0)
            {
                clipTime -= Time.deltaTime * timeScalar;
                frameTime -= Time.deltaTime * timeScalar;
            }     
        }
        
    }

    // Set the play direction
    public void SetDirection(int newDirection)
    {
        playDirection = newDirection;
    }


    // reset the clip to either the start moving forward
    // or the end moving backwards
    public void ResetToFirstFrame()
    {
        clip.CalculateDuration();
        frameIndex = 0;
        clipTime = 0.0f;
        frameTime = 0.0f;
        SetDirection(1);
    }
    public void ResetToLastFrame()
    {
        frameIndex = clip.frameCount -1;
        clip.CalculateDuration();
        clipTime = clip.clipDuration;
        frameTime = clip.keyframePool.framePool[frameIndex].duration;
        SetDirection(-1);
    }


    //increment time by 25%
    public void IncTimeScalar(bool increase)
    {
        if (increase)
            timeScalar += 0.25f; // 25% up (from original)
        else
            timeScalar -= 0.25f; // 25% down (from original)
    }

    // Debug frame data
    public void DebugList()
    {
        Debug.Log("FRAME " + frameIndex);
        Debug.Log(frameTime);
        Debug.Log(clipTime);
    }

    // Change the clip to 'i' as long as 'i' is within the bounds of the clip-list
    public void ChangeClip(int i)
    {
        if(i <= pool.clipCount -1)
        {
            clipIndex = i;
            clip = pool.clipPool[i];
        }
        else
        {
            clipIndex = 0;
            clip = pool.clipPool[0];
        }
        
    }
}
