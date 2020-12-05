﻿/*
File name: lab4display.cs
Purpose:  this display the information for lab 4
Contributors: Nick Brennan-Martin and Bradley Chamberlain
Collaborated on one PC
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lab4display : MonoBehaviour
{

    public bool modeB;
    public Text mode;
    public Text conditional;
    public GameObject player;
    public ClipController controller;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(modeB)
        {
            mode.text = "Debug Mode";
            conditional.text = controller.clip.ToString();
        }
        else
        {
            mode.text = "Normal Mode";
        }
    }
}
