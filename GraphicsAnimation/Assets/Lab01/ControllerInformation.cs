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

    public GameObject controllerOne;
    public GameObject controllerTwo;
    public GameObject controllerThree;

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

        }

        if (pauseTwo == false)
        {

            controllerInformationTwo.text = "Controller 2: " + controllerTwo.GetComponent<ClipController>().controllerName;
            controllerInformationTwo.text += "\n";
            controllerInformationTwo.text += "Current Keyframe Index: " + controllerTwo.GetComponent<ClipController>().clip.keyframePool.keyframePool[controllerTwo.GetComponent<ClipController>().clip.clipIndex].index;
            controllerInformationTwo.text += "\n";
            controllerInformationTwo.text += "Current Keyframe Data: " + controllerTwo.GetComponent<ClipController>().clip.keyframePool.keyframePool[controllerTwo.GetComponent<ClipController>().clip.clipIndex].data;

        }

        if (pauseThree == false)
        {

            controllerInformationThree.text = "Controller 1: " + controllerThree.GetComponent<ClipController>().controllerName;
            controllerInformationThree.text += "\n";
            controllerInformationThree.text += "Current Keyframe Index: " + controllerThree.GetComponent<ClipController>().clip.keyframePool.keyframePool[controllerThree.GetComponent<ClipController>().clip.clipIndex].index;
            controllerInformationThree.text += "\n";
            controllerInformationThree.text += "Current Keyframe Data: " + controllerThree.GetComponent<ClipController>().clip.keyframePool.keyframePool[controllerThree.GetComponent<ClipController>().clip.clipIndex].data;

        }

        if (Input.GetKeyDown(KeyCode.P))
        {

            if(currentControllerIndex == 1)
            {

                pauseOne = !pauseOne;
            }

            if (currentControllerIndex == 2)
            {

                pauseTwo = !pauseTwo;
            }

            if (currentControllerIndex == 3)
            {

                pauseThree = !pauseThree;
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
