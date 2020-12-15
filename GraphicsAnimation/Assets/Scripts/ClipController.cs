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
    //new SpriteRenderer renderer;
    public HierarchyState state;
    public string controllerName;

    int clipIndex;
    float clipDuration;
    float clipTime; // time that has passed since start
    float clipParameter;
    
    int frameIndex; //changes throughout the update
    float frameTime;
    float frameParameter;

    public Direction playDirection;
    float frameOvershoot;

    public float timeScalar = 1.0f;

    // return non-public variables
    
    public int GetClipIndex()
    {
        return clipIndex;
    }
    
    public float GetClipTime()
    {
        return clipTime;
    }
    
    public float GetClipDuration()
    {
        return clipDuration;
    }
    
    public float GetFrameTime()
    {
        return frameTime;
    }
    public int GetFrameIndex()
    {
        return frameIndex;
    }
    // Default Contstructor
    public ClipController()
    {
        controllerName = "INACTIVE";
        //pool = null;
        //clipIndex = 0;
        clipTime = 0.0f;
        clipParameter = 0.0f;
        //clip = null;

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
        //clipIndex = clip.GetClipIndex();
        frameIndex = startFrame;
        if (newPlayDirection == Direction.forward)
        {
            clipTime = clip.startEndTimes[0].x;
        }
        if (newPlayDirection == Direction.pause)
        {
            clipTime = clip.startEndTimes[0].x;
        }
        if (newPlayDirection == Direction.reverse)
        {
            clipTime = clip.startEndTimes[0].y;
            frameTime = clip.startEndTimes[0].y - clip.startEndTimes[0].x;
        }
        playDirection = newPlayDirection;
    }

    private void Start()
    {
        state.basePose = clip.frameSequence[frameIndex].basePose;
        state.newPose = clip.frameSequence[frameIndex].endPose;
    }

    void Update()
    {
        // apply time step
        UpdateTime();
        ResolveTime();
        DisplayFrame();
        // post
        clipParameter = clipTime / clip.GetClipDuration();
        float v = frameTime / clip.keyframePool.framePool[frameIndex].duration;
        frameParameter = v;
    }
    void DisplayFrame()
    {
        state.SetTime(frameParameter);  
    }
    public void Transition(bool isEnd)
    {
        ClipTransition Trans;
        // transition forward
        if (isEnd)
        {
            Trans = clip.EndTransition;
        }
        else // transition backwards
        {
            Trans = clip.ReverseTransition;
        }

        // set controlled clip to transition clip
        clip = Trans.targetClip;

        // recalculate the time stamps for the clip
        clip.CalculateDuration();

        // forward Transition
        if (Trans.playDirection == Direction.forward)
        {
            // input new clip data to clip controller
            if (Trans.startAtBegining)
                frameIndex = 0;
            else
                frameIndex = clip.frameSequence.Length - 1;

            clipTime = frameOvershoot;
            frameTime = frameOvershoot;
            clipParameter = (clipParameter - 1) * clipDuration;
            frameParameter = frameOvershoot / clip.frameSequence[frameIndex].duration;
            playDirection = Trans.playDirection;

            state.basePose = clip.frameSequence[0].basePose;
            state.newPose = clip.frameSequence[0].endPose;
            state.SetTime(0);

        }
        else if (Trans.playDirection == Direction.reverse)
        {
            // input new clip data to clip controller
            if (Trans.startAtBegining)
                frameIndex = 0;
            else
                frameIndex = clip.frameSequence.Length - 1;

            clipTime = clip.startEndTimes[clip.frameSequence.Length - 1].y;
            frameTime = clip.startEndTimes[frameIndex].y - clip.startEndTimes[frameIndex].x;
            frameParameter = 1.0f;
            playDirection = Trans.playDirection;

            state.basePose = clip.frameSequence[clip.frameSequence.Length - 1].basePose;
            state.newPose = clip.frameSequence[clip.frameSequence.Length - 1].endPose;
            state.SetTime(clip.clipDuration);

        }
        else // Pause Transition
        {
            // input new clip data to clip controller
            if (Trans.startAtBegining)
                frameIndex = 0;
            else
                frameIndex = Trans.targetClip.frameSequence.Length - 1;

            clipTime = Trans.targetClip.startEndTimes[0].x;
            frameTime = 0.0f;
            frameParameter = 0.0f;
            playDirection = Trans.playDirection;
          
        }
    }
    
    // determine the current frame and time within
    public void ResolveTime()
    {
        // if yes, the keyframe last (random) just ended in forward
        if (frameParameter > 1.0 && playDirection == Direction.forward) // moving forward and the frame ended
        {
            // this is the amount of time that the dx went over the last keyframe
            frameOvershoot = (frameParameter - 1.0f) * clip.frameSequence[frameIndex].duration;

            //transition to new clip
            if (clipTime > clip.clipDuration)
            {
                Transition(true);
            }
            // move to next frame
            else
            {
                frameIndex++;
                state.basePose = clip.frameSequence[frameIndex].basePose;
                state.newPose = clip.frameSequence[frameIndex].endPose;
       
                frameTime = frameOvershoot;
                frameParameter = frameOvershoot / clip.frameSequence[frameIndex].duration;
                state.SetTime(frameParameter);
            }
        }

        // if yes, the keyframe last (random) just ended in reverse
        if (frameParameter < 0.0 && playDirection == Direction.reverse) // moving backwards and the frame ended
        {
            frameOvershoot = (0.0f - frameParameter) * clip.frameSequence[frameIndex].duration;
            if (frameIndex > 0)
            {       
                frameIndex--;
                frameTime = clip.frameSequence[frameIndex].duration; // frame condition fixed
                frameTime += frameOvershoot; // one frame condition fixed
            }
            if (frameIndex == 0)
            {
                //REVERSE TRANSITION
                Transition(false);
                //Debug.Log(clipTime);
            }
            
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
        frameIndex = clip.frameSequence.Length -1;
        clip.CalculateDuration();
        clipTime = clip.GetClipDuration();
        frameTime = clip.frameSequence[frameIndex].duration;
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

    public void ChangeClip(Clip newClip)
    {
        newClip.CalculateDuration();
        float newTime = (clip.clipDuration / newClip.clipDuration) * clipTime;
        clip = newClip;
        state.SetTime(newTime);

        frameIndex = 0;
        frameTime = 0f;
        frameParameter = 0f;
        clipParameter = 0.0f;

        state.basePose = clip.frameSequence[0].basePose;
        state.newPose = clip.frameSequence[0].endPose;
        
    }

}
