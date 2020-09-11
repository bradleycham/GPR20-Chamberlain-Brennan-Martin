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

    public ClipController controllerOne;
    public ClipController controllerTwo;
    public ClipController controllerThree;

    bool pauseOne;
    bool pauseTwo;
    bool pauseThree;

    int currentControllerIndex;

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

        if (pauseOne == false)
        {

            controllerInformationOne.text = "Controller 1: " + controllerOne.GetComponent<ClipController>().controllerName;
            controllerInformationOne.text += "\n";
            controllerInformationOne.text += "Current Keyframe Index: " + controllerOne.GetComponent<ClipController>().clip.keyframePool.keyframePool[controllerOne.GetComponent<ClipController>().clip.clipIndex].index;
            controllerInformationOne.text += "\n";
            controllerInformationOne.text += "Current Keyframe Data: " + controllerOne.GetComponent<ClipController>().clip.keyframePool.keyframePool[controllerOne.GetComponent<ClipController>().clip.clipIndex].data;
            controllerInformationOne.text += "\n";
            controllerInformationOne.text += "Time Scale: " + controllerOne.timeScalar;
            controllerInformationOne.text += "\n";
            controllerInformationOne.text += "Current ClipTime: " + controllerOne.clipTime;
            controllerInformationOne.text += "\n";
            controllerInformationOne.text += "Current KeyFrameTime: " + controllerOne.frameTime;
        }

        if (pauseTwo == false)
        {
            // THE THIRD TIME SCALE DOESNt REACT
            controllerInformationTwo.text = "Controller 2: " + controllerTwo.GetComponent<ClipController>().controllerName;
            controllerInformationTwo.text += "\n";
            controllerInformationTwo.text += "Current Keyframe Index: " + controllerTwo.GetComponent<ClipController>().clip.keyframePool.keyframePool[controllerTwo.GetComponent<ClipController>().frameIndex].index;
            controllerInformationTwo.text += "\n";
            controllerInformationTwo.text += "Current Keyframe Data: " + controllerTwo.GetComponent<ClipController>().clip.keyframePool.keyframePool[controllerTwo.GetComponent<ClipController>().frameIndex].data;
            controllerInformationTwo.text += "\n";
            controllerInformationTwo.text += "Time Scale: " + controllerTwo.timeScalar;
            controllerInformationTwo.text += "\n";
            controllerInformationTwo.text += "Current ClipTime: " + controllerTwo.clipTime;
            controllerInformationTwo.text += "\n";
            controllerInformationTwo.text += "Current KeyFrameTime: " + controllerTwo.frameTime;
        }

        if (pauseThree == false)
        {

            controllerInformationThree.text = "Controller 1: " + controllerThree.GetComponent<ClipController>().controllerName;
            controllerInformationThree.text += "\n";
            controllerInformationThree.text += "Current Keyframe Index: " + controllerThree.GetComponent<ClipController>().clip.keyframePool.keyframePool[controllerThree.GetComponent<ClipController>().frameIndex].index;
            controllerInformationThree.text += "\n";
            controllerInformationThree.text += "Current Keyframe Data: " + controllerThree.GetComponent<ClipController>().clip.keyframePool.keyframePool[controllerThree.GetComponent<ClipController>().frameIndex].data;
            controllerInformationThree.text += "\n";
            controllerInformationThree.text += "Time Scale: " + controllerThree.timeScalar;
            controllerInformationThree.text += "\n";
            controllerInformationThree.text += "Current ClipTime: " + controllerThree.clipTime;
            controllerInformationThree.text += "\n";
            controllerInformationThree.text += "Current KeyFrameTime: " + controllerThree.frameTime;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {

            if(currentControllerIndex == 1)
            {

                controllerOne.SetDirection(0, 1.0f);
            }

            if (currentControllerIndex == 2)
            {

                controllerTwo.SetDirection(0, 1.0f);

            }

            if (currentControllerIndex == 3)
            {

                controllerTwo.SetDirection(0, 1.0f);

            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow))
        {

            if (currentControllerIndex == 1)
            {

                controllerOne.IncTimeScalar(false);
            }

            if (currentControllerIndex == 2)
            {

                controllerTwo.IncTimeScalar(false);

            }

            if (currentControllerIndex == 3)
            {

                controllerTwo.IncTimeScalar(false);

            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            if (currentControllerIndex == 1)
            {

                controllerOne.IncTimeScalar(true);
            }

            if (currentControllerIndex == 2)
            {

                controllerTwo.IncTimeScalar(true);

            }

            if (currentControllerIndex == 3)
            {

                controllerTwo.IncTimeScalar(true);

            }
        }

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
