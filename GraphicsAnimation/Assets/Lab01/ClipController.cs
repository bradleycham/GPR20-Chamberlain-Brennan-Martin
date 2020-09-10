using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipController : MonoBehaviour
{
    ClipPool pool;
    Clip clip;
    string controllerName;

    int clipIndex;
    float clipTime; // time that has passed since start
    float clipParameter;
    
    int frameIndex;
    float frameTime;
    float frameParameter;

    uint playDirection;

    public ClipController()
    {
        controllerName = "INACTIVE";
        clipIndex = 0;
        clipTime = 0.0f;
        clipParameter = 0.0f;

        frameIndex = 0;
        frameTime = 0.0f;
        frameParameter = 0.0f;

        playDirection = 1;
    }

    public ClipController(int clipIndex, int frameIndex, uint playState)
    {
        this.clipIndex = clipIndex;
        
        this.frameIndex = frameIndex;
        playDirection = playState;
    }

    void Update()
    {
        // apply time step
        clipTime += Time.deltaTime;
        frameTime += Time.deltaTime;

        // resolve time 

        // post
        clipParameter = 

        // create looping feature

        // realtime, clip time, and 
    }
}
