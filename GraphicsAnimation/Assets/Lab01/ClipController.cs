using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipController : MonoBehaviour
{
    ClipPool pool;
    Clip clip;
    string controllerName;

    static int clipIndex; // stays static
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
        clipIndex = 0;
        clipTime = 0.0f;
        clipParameter = 0.0f;
        clip = pool.clipPool[clipIndex];

        frameIndex = 0;
        frameTime = 0.0f;
        frameParameter = 0.0f;

        playDirection = 1;
    }

    public ClipController(int startIndex, int startFrame, uint playState)
    {
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
            if(frameIndex < clip.frameCount)
            {
                frameIndex++;
                // determine overshhot algorithm
                //frameOvershoot = c
            }
            else
            {
                frameIndex = 0;
                //loop
                //determine overshoot
            }
        }

        // post
        clipParameter = clipTime * clip.durationInv;
        frameParameter = frameTime * clip.framePool[frameIndex].durationInv;

        // create looping feature

        //clip time, and frametime
    }
}
