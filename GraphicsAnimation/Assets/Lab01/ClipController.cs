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
    float clipTime; // time that has passed since start
    float clipParameter;
    
    int frameIndex; //changes throughout the update
    float frameTime;
    float frameParameter;

    uint playDirection;
    float frameOvershoot;

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

    public ClipController(int startIndex, int startFrame, uint playState, ClipPool newPool)
    {
        pool = newPool;
        clipIndex = startIndex;
        frameIndex = startFrame;
        playDirection = playState;
    }

    void Update()
    {
        // apply time step
        clipTime += Time.deltaTime;
        frameTime += Time.deltaTime;

        // resolve time 
        //what does a clip do: scrolls through keyframes
        if(frameParameter >= 1.0)
        {
            Debug.Log(frameIndex);

            frameOvershoot = (frameParameter - 1.0f) * clip.keyframePool.keyframePool[frameIndex].duration;
            if(frameIndex < clip.frameCount)
            {
                frameIndex++;
                frameTime = 0.0f;
                // determine overshhot algorithm
                //frameOvershoot = c
            }
            else
            {
                frameIndex = 0;
                clipTime = 0.0f;

                //loop
                //determine overshoot
            }
            frameTime += frameOvershoot;
            clipTime += frameOvershoot;
        }

        // post
        clipParameter = clipTime / clip.clipDuration;
        float v = frameTime / clip.keyframePool.keyframePool[frameIndex].duration;
        frameParameter = v;

        // create looping feature

        //clip time, and frametime
    }
}
