/*
File name: ControllerInformation.cs
Purpose: Displays information of the controller 
Contributors: Nick Brennan-Martin and Bradley Chamberlain
Collaborated on one PC
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerInformation : MonoBehaviour
{

    public Text controllerInformationOne;
    public Text controllerInformationTwo;
    public Text controllerInformationThree;
    public Text currentController;

    public ClipController[] controllers;

    bool pauseOne;
    bool pauseTwo;
    bool pauseThree;

    int currentControllerIndex;
    int currentClipIndex;

    // Start is called before the first frame update
    void Start()
    {

        pauseOne = false;
        pauseTwo = false;
        pauseThree = false;
        currentControllerIndex = 1;
        currentController.text = "Controller 1 active";
    }

    // Update is called once per frame
    void Update()
    {
        // writing to the canvas
        if (pauseOne == false)
        {

            controllerInformationOne.text = "Controller 1: " + controllers[0].controllerName;
            controllerInformationOne.text += "\n";
            controllerInformationOne.text += "Clip Name: " + controllers[0].clip.clipName;
            controllerInformationOne.text += "\n";
            controllerInformationOne.text += "KeyframeInSequence: " + controllers[0].frameIndex;
            controllerInformationOne.text += "\n";
            controllerInformationOne.text += "Current Keyframe Index: " + controllers[0].clip.frameSequence[controllers[0].frameIndex];
            controllerInformationOne.text += "\n";
            controllerInformationOne.text += "Current Keyframe Data: " + controllers[0].clip.keyframePool.framePool[controllers[0].clip.frameSequence[controllers[0].frameIndex]].data;
            controllerInformationOne.text += "\n";

            controllerInformationOne.text += "Time Direction: " + controllers[0].playDirection;
            controllerInformationOne.text += "\n";

            controllerInformationOne.text += "Time Scale: " + controllers[0].timeScalar;
            controllerInformationOne.text += "\n";
            controllerInformationOne.text += "Current ClipTime: " + controllers[0].clipTime;
            controllerInformationOne.text += "\n";
            controllerInformationOne.text += "Current KeyFrameTime: " + controllers[0].frameTime;
        }

        if (pauseTwo == false)
        {
            controllerInformationTwo.text = "Controller 2: " + controllers[1].controllerName;
            controllerInformationTwo.text += "\n";
            controllerInformationTwo.text += "Clip Name: " + controllers[1].clip.clipName;
            controllerInformationTwo.text += "\n";
            controllerInformationTwo.text += "KeyframeInSequence: " + controllers[1].frameIndex;
            controllerInformationTwo.text += "\n";
            controllerInformationTwo.text += "Current Keyframe Index: " + controllers[1].clip.frameSequence[controllers[1].frameIndex];
            controllerInformationTwo.text += "\n";
            controllerInformationTwo.text += "Current Keyframe Data: " + controllers[1].clip.keyframePool.framePool[controllers[1].clip.frameSequence[controllers[1].frameIndex]].data;
            controllerInformationOne.text += "\n";

            controllerInformationTwo.text += "Time Direction: " + controllers[1].playDirection;
            controllerInformationTwo.text += "\n";

            controllerInformationTwo.text += "Time Scale: " + controllers[1].timeScalar;
            controllerInformationTwo.text += "\n";
            controllerInformationTwo.text += "Current ClipTime: " + controllers[1].clipTime;
            controllerInformationTwo.text += "\n";
            controllerInformationTwo.text += "Current KeyFrameTime: " + controllers[1].frameTime;
        }

        if (pauseThree == false)
        {

            controllerInformationThree.text = "Controller 3: " + controllers[2].controllerName;
            controllerInformationThree.text += "\n";
            controllerInformationThree.text += "Clip Name: " + controllers[2].clip.clipName;
            controllerInformationThree.text += "\n";
            controllerInformationThree.text += "KeyframeInSequence: " + controllers[2].frameIndex;
            controllerInformationThree.text += "\n";
            controllerInformationThree.text += "Current Keyframe Index: " + controllers[2].clip.frameSequence[controllers[2].frameIndex];
            controllerInformationThree.text += "\n";
            controllerInformationThree.text += "Current Keyframe Data: " + controllers[2].clip.keyframePool.framePool[controllers[2].clip.frameSequence[controllers[2].frameIndex]].data;
            controllerInformationThree.text += "\n";

            controllerInformationThree.text += "Time Direction: " + controllers[2].playDirection;
            controllerInformationThree.text += "\n";

            controllerInformationThree.text += "Time Scale: " + controllers[2].timeScalar;
            controllerInformationThree.text += "\n";
            controllerInformationThree.text += "Current ClipTime: " + controllers[2].clipTime;
            controllerInformationThree.text += "\n";
            controllerInformationThree.text += "Current KeyFrameTime: " + controllers[2].frameTime;
        }

        // play directions
        if (Input.GetKeyDown(KeyCode.Q)) // rewind
        {
            controllers[currentControllerIndex - 1].SetDirection(-1);
        }
        if (Input.GetKeyDown(KeyCode.W)) // pause
        {
            controllers[currentControllerIndex-1].SetDirection(0);   
        }
        if (Input.GetKeyDown(KeyCode.E)) // forward
        {
            controllers[currentControllerIndex - 1].SetDirection(1);
        }

        if (Input.GetKeyDown(KeyCode.F)) // set to first
        {
            controllers[currentControllerIndex - 1].ResetToFirstFrame();
        }
        if (Input.GetKeyDown(KeyCode.L)) // set to last
        {
            controllers[currentControllerIndex - 1].ResetToLastFrame();
        }

        //Change time scale
        if (Input.GetKeyDown(KeyCode.LeftArrow) )
        {

            controllers[currentControllerIndex - 1].IncTimeScalar(false);

        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            controllers[currentControllerIndex - 1].IncTimeScalar(true);
        }

        // change clip
        if (Input.GetKeyDown(KeyCode.Space))
        {
              //controllers[currentControllerIndex].ChangeClip(controllers[currentControllerIndex].clipIndex+1);          
        }

        //Choose controller to edit
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            currentControllerIndex = 1;
            currentController.text = "Controller 1 active";
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            currentControllerIndex = 2;
            currentController.text = "Controller 2 active";
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentControllerIndex = 3;
            currentController.text = "Controller 3 active";
        }

    }
}
