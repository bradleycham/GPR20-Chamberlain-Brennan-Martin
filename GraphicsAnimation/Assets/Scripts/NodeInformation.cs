/*
File name: NodeInformation.cs
Purpose: Displays information of the hierarchy 
Contributors: Nick Brennan-Martin and Bradley Chamberlain
Collaborated on one PC
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeInformation : MonoBehaviour
{

    public Text nodeInformationOne;
    public Text nodeInformationTwo;
    public Text nodeInformationThree;
    public Text currentHierarchy;

    public HierarchyState[] controllers;

    bool pauseOne;
    bool pauseTwo;
    bool pauseThree;

    int currentHierarchyIndex;
    int currentClipIndex;

    // Start is called before the first frame update
    void Start()
    {

        pauseOne = false;
        pauseTwo = false;
        pauseThree = false;
        currentHierarchyIndex = 1;
        currentHierarchy.text = "Hierarchy 1 active";
    }

    // Update is called once per frame
    void Update()
    {
        // writing to the canvas
        if (pauseOne == false)
        {

            nodeInformationOne.text = "Controller 1: ";
            nodeInformationOne.text += "\n";
            
            for(int i = 0; i < controllers[0].hierarchy.treeDepth.Length; i++)
            {

                nodeInformationOne.text += "Node " + i + ": \n";
                nodeInformationOne.text += "Position:  " + controllers[0].hierarchy.treeDepth[i].transform.position + "\n";
                nodeInformationOne.text += "Rotation:  " + controllers[0].hierarchy.treeDepth[i].transform.rotation + "\n";
                nodeInformationOne.text += "Scale:  " + controllers[0].hierarchy.treeDepth[i].transform.localScale + "\n";
            }
        }

        //if (pauseTwo == false)
        //{
        //    nodeInformationTwo.text = "Controller 2: ";
        //    nodeInformationTwo.text += "\n";

        //    for (int i = 0; i < controllers[1].hierarchy.treeDepth.Length; i++)
        //    {

        //        nodeInformationTwo.text += "Node " + i + ": + \n";
        //        nodeInformationTwo.text += "Position:  " + controllers[1].hierarchy.treeDepth[i].transform.position + "\n";
        //        nodeInformationTwo.text += "Rotation:  " + controllers[1].hierarchy.treeDepth[i].transform.rotation + "\n";
        //        nodeInformationTwo.text += "Scale:  " + controllers[1].hierarchy.treeDepth[i].transform.localScale + "\n";
        //    }
        //}

        //if (pauseThree == false)
        //{

        //    nodeInformationThree.text = "Controller 3: ";
        //    nodeInformationThree.text += "\n";

        //    for (int i = 0; i < controllers[2].hierarchy.treeDepth.Length; i++)
        //    {

        //        nodeInformationThree.text += "Node " + i + ": + \n";
        //        nodeInformationThree.text += "Position:  " + controllers[2].hierarchy.treeDepth[i].transform.position + "\n";
        //        nodeInformationThree.text += "Rotation:  " + controllers[2].hierarchy.treeDepth[i].transform.rotation + "\n";
        //        nodeInformationThree.text += "Scale:  " + controllers[2].hierarchy.treeDepth[i].transform.localScale + "\n";
        //    }
        //}

        // play directions
        if (Input.GetKeyDown(KeyCode.Q)) // rewind
        {
           // controllers[currentHierarchyIndex - 1].SetDirection(Direction.reverse);
        }
        if (Input.GetKeyDown(KeyCode.W)) // pause
        {
           // controllers[currentHierarchyIndex - 1].SetDirection(Direction.pause);
        }
        if (Input.GetKeyDown(KeyCode.E)) // forward
        {
           // controllers[currentHierarchyIndex - 1].SetDirection(Direction.forward);
        }

        if (Input.GetKeyDown(KeyCode.F)) // set to first
        {
           // controllers[currentHierarchyIndex - 1].ResetToFirstFrame();
        }
        if (Input.GetKeyDown(KeyCode.L)) // set to last
        {
           // controllers[currentHierarchyIndex - 1].ResetToLastFrame();
        }

        //Change time scale
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

           // controllers[currentHierarchyIndex - 1].IncTimeScalar(false);

        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            //controllers[currentHierarchyIndex - 1].IncTimeScalar(true);
        }

        // change clip
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //controllers[currentControllerIndex].ChangeClip(controllers[currentControllerIndex].clipIndex+1);          
        }

        //Choose controller to edit
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            currentHierarchyIndex = 1;
            currentHierarchy.text = "Hierarchy 1 active";
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            currentHierarchyIndex = 2;
            currentHierarchy.text = "Hierarchy 2 active";
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentHierarchyIndex = 3;
            currentHierarchy.text = "Hierarchy 3 active";
        }

    }
}
