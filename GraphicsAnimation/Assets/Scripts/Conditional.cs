/*
File name: Conditional.cs
Purpose: to change the current animating clip on a controller based on ingame data
Contributors: Nick Brennan-Martin and Bradley Chamberlain
Collaborated on one PC
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conditional : MonoBehaviour
{
    public enum conditionType
    {
        greaterThan,
        lessThan,
        equal,
        greaterThanOrEqual,
        lessThanOrEqual
    }

    //public bool playMain;
    public conditionType condition;
    public Clip mainClip;
    public Clip altClip;

    bool moving;
    public bool crouch;
    public Clip crouchClip;

    public Clip currentClip;
    public float a;
    public float b;
    public bool clipSwitch;

    public ClipController controller;
    public void CheckClip()
    {
        /*
        if(condition == conditionType.greaterThan)
        {
            if (a > b)
                currentClip = altClip;
        }
        else if (condition == conditionType.lessThan)
        {
            if (a < b)
                currentClip = altClip;
        }
        else if (condition == conditionType.equal)
        {
            if (a == b)
                currentClip = altClip;
        }
        else if (condition == conditionType.greaterThanOrEqual)
        {
            if (a >= b)
                currentClip = altClip;
        }
        else if (condition == conditionType.lessThanOrEqual)
        {
            if (a <= b)
                currentClip = altClip;
        }*/
        /*
        if(Input.GetKeyDown(KeyCode.C))
        {
            crouch = !crouch;
            //currentClip = crouchClip;
        }
        if (clipSwitch)
            currentClip = altClip;
        else
            currentClip = mainClip;
        */
        if(Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.D))
        {
            moving = true;
        }
        else { moving = false; }
        if(Input.GetKeyDown(KeyCode.C))
        {
            crouch = !crouch;
        }
    }

    public Clip GetCurrentClip()
    {
        return currentClip;
    }

    private void Start()
    {
        clipSwitch = false;
        currentClip = mainClip;
    }

    private void Update()
    {
        /*
        if (Input.anyKey)
        {
            clipSwitch = true;
        }
        else if(!Input.anyKey)
        {
            clipSwitch = false;
        }
        Clip temp = currentClip;
        
        if(currentClip != temp)
        {
             controller.ChangeClip(currentClip);
        }
        */
        CheckClip();
        if (moving && crouch)
        {
            if (controller.clip != crouchClip)
                controller.ChangeClip(crouchClip);
        }
        if(moving && !crouch)
        {
            
            if (controller.clip != altClip)
                controller.ChangeClip(altClip);
        }
        if(!moving && !crouch)
        {
            if (controller.clip != mainClip)
                controller.ChangeClip(mainClip);
        }
    }
}
