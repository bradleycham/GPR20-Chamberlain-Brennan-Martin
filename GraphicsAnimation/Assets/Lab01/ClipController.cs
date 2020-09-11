/*
File name: ClipController.cs
Purpose:  
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

    public int clipIndex; // stays static
    public float clipDuration;
    public float clipTime; // time that has passed since start
    float clipParameter;
    
    public int frameIndex; //changes throughout the update
    public float frameTime;
    float frameParameter;

    public int playDirection;
    float frameOvershoot;

    float clipOvershoot;
    public float timeScalar = 1.0f;

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

    public ClipController(int startIndex, int startFrame, int playState, ClipPool newPool)
    {
        pool = newPool;
        clipIndex = startIndex;
        frameIndex = startFrame;
        playDirection = playState;
    }

    void Update()
    {
        // apply time step
        UpdateTime();
        ResolveTime();

        // resolve time 
        //what does a clip do: scrolls through keyframes
        

        // post
        clipParameter = clipTime / clip.clipDuration;
        float v = frameTime / clip.keyframePool.keyframePool[clip.frameSequence[frameIndex]].duration;
        frameParameter = v;
        //DebugList();
        Debug.Log(frameIndex);

        // create looping feature
    }

    public void ResolveTime()
    {
        if (frameParameter > 1.0 && playDirection > 0) // moving forward and the frame ended
        {
            frameOvershoot = (frameParameter - 1.0f) * clip.keyframePool.keyframePool[clip.frameSequence[frameIndex]].duration;
            if (frameIndex < clip.frameCount && frameParameter > 1.0)
            {
                frameIndex++;
                frameTime = 0.0f; // frame condition fixed
                // determine overshhot algorithm
                //frameOvershoot = c
            }
            if(frameIndex == clip.frameCount)
            {
                frameIndex = 0;
                frameTime = 0.0f;
                clipTime = clipDuration;

                //loop
                //determine overshoot
            }
            //frameTime += frameOvershoot; // one frame condition fixed
        }


        if (frameParameter < 0.0 && playDirection < 0) // moving backwards and the frame ended
        {
            frameOvershoot = (0.0f - frameParameter) * clip.keyframePool.keyframePool[clip.frameSequence[frameIndex]].duration;
            if (frameIndex > 0)
            {       
                frameIndex--;
                frameTime = clip.keyframePool.keyframePool[clip.frameSequence[frameIndex]].duration; // frame condition fixed
                frameTime += frameOvershoot; // one frame condition fixed
            }
            if (frameIndex == 0)
            {                             
                //clipOvershoot = clipTime - clipDuration;
                frameIndex = clip.frameCount-1;
                clipTime = clipDuration;
                frameTime = clip.keyframePool.keyframePool[clip.frameSequence[frameIndex]].duration;
            }
        }
    }    

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

    public void SetDirection(int newDirection, float newTimeScalar)
    {
        playDirection = newDirection;
        timeScalar = newTimeScalar;
    }
    
    public void IncTimeScalar(bool increase)
    {
        if (increase)
            timeScalar *= 1.25f; // 25% up
        else
            timeScalar *= 0.75f; // 25% down
    }

    public void DebugList()
    {
        Debug.Log("FRAME " + frameIndex);
        Debug.Log(frameTime);
        Debug.Log(clipTime);
    }
}
