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
    Clip currentClip;
    public float a;
    public float b;
    bool clipSwitch;

    public ClipController controller;
    public void CheckClip()
    {
        if(condition == conditionType.greaterThan)
        {
            if (a > b)
                currentClip = mainClip;
        }
        if (condition == conditionType.lessThan)
        {
            if (a < b)
                currentClip = mainClip;
        }
        if (condition == conditionType.equal)
        {
            if (a == b)
                currentClip = mainClip;
        }
        if (condition == conditionType.greaterThanOrEqual)
        {
            if (a >= b)
                currentClip = mainClip;
        }
        if (condition == conditionType.lessThanOrEqual)
        {
            if (a <= b)
                currentClip = mainClip;
        }
        currentClip = altClip;
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
        Clip temp = currentClip;
        CheckClip();
        if(currentClip != temp)
        {
            controller.ChangeClip(currentClip);
        }
    }
}
