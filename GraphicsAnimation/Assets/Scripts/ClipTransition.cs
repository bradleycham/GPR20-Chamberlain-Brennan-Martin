﻿/*
File name: ClipTransition.cs
Purpose: this structure will hold the data pertinent to transitioning clips
Contributors: Nick Brennan-Martin and Bradley Chamberlain
Collaborated on one PC
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipTransition : MonoBehaviour
{
    public Clip targetClip;
    public bool startAtBegining;
    public Direction playDirection;
}
