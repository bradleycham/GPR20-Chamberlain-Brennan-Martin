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

public enum Direction : int
{
    reverse = -1,
    pause = 0,
    forward = 1
}
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

    public Direction playDirection;
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

        playDirection = Direction.forward;
    }

    // Constructor overload
    public ClipController(Clip newClip, int startFrame, Direction newPlayDirection)
    {
        //pool = newPool;
        clip = newClip;
        clipIndex = clip.clipIndex;
        frameIndex = startFrame;
        if (newPlayDirection == Direction.forward)
        {
            clipTime = clip.startEndTimes[startFrame].x;
        }
        if (newPlayDirection == Direction.pause)
        {
            clipTime = clip.startEndTimes[startFrame].x;
        }
        if (newPlayDirection == Direction.reverse)
        {
            clipTime = clip.startEndTimes[startFrame].y;
            frameTime = clip.startEndTimes[startFrame].y - clip.startEndTimes[startFrame].x;
        }
        playDirection = newPlayDirection;
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

    public void Transition(bool isEnd)
    {
        

        if (isEnd)
        {
            ClipTransition Trans;
            Trans = clip.EndTransition;
            //set to new clip
            //set to new clip time
            //set frame time
            //set direction
            //set currentframe
            //set first and last frames
            // set clip to the new clip and redo vars
            clip = Trans.targetClip;
            frameIndex = clip.EndTransition.startFrame;
            clipTime = clip.startEndTimes[Trans.startFrame].x;
            frameTime = 0.0f;
            playDirection = Trans.playDirection;
        }
        else if(!isEnd)
        {

        }
            
    }
    
    // determine the current frame and time within
    public void ResolveTime()
    {
        // if yes, the keyframe last (random) just ended in forward
        if (frameParameter > 1.0 && playDirection == Direction.forward) // moving forward and the frame ended
        {
            // this is the amount of time that the dx went over the last keyframe
            frameOvershoot = (frameParameter - 1.0f) * clip.keyframePool.framePool[clip.frameSequence[frameIndex]].duration; 

            //transition to new clip
            if (frameIndex == clip.frameCount - 1)
            {
                //FORWARD TRANSITION
                Transition(true);
            }
            // move to next frame
            else if (frameIndex < clip.frameCount)
            {
                frameIndex++;
                frameTime = 0.0f; // frame condition fixed
                frameTime += frameOvershoot;
            }
            //Debug.Log(frameOvershoot);
        }

        // if yes, the keyframe last (random) just ended in reverse
        if (frameParameter < 0.0 && playDirection == Direction.reverse) // moving backwards and the frame ended
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
        if(playDirection == Direction.forward)
        {
            // forward
            clipTime += Time.deltaTime * timeScalar;
            frameTime += Time.deltaTime * timeScalar;
        }
        else if(playDirection == Direction.pause)
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
    public void SetDirection(Direction newDirection)
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
        SetDirection(Direction.forward);
    }
    public void ResetToLastFrame()
    {
        frameIndex = clip.frameCount -1;
        clip.CalculateDuration();
        clipTime = clip.clipDuration;
        frameTime = clip.keyframePool.framePool[frameIndex].duration;
        SetDirection(Direction.reverse);
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
