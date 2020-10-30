/*
File name: TreeVisualizer.cs
Purpose:  This program will visualize the tree of blend operations
Contributors: Nick Brennan-Martin and Bradley Chamberlain
Collaborated on one PC
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeVisualizer : MonoBehaviour
{

    public Text treeLeef1;
    public Text treeLeef2;
    public Text treeLeefFinal;
    public Text operation;
    public Text treeLeefU;
    public Text arrow1;
    public Text arrow2;
    public Text arrow3;
    public Text arrow4;

    public GameObject node0;
    public GameObject node1;
    public GameObject nodef;

    public enum blendOperation { add, lerp, scale};
    public blendOperation op;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(op == blendOperation.scale)
        {

            arrow2.enabled = false;
            treeLeef2.enabled = false;
            operation.text = " x ";
            arrow4.enabled = true;
            treeLeefU.enabled = true;
        }

        if (op == blendOperation.add)
        {

            arrow2.enabled = true;
            treeLeef2.enabled = true;
            operation.text = " + ";
            arrow4.enabled = false;
            treeLeefU.enabled = false;
        }

        if (op == blendOperation.lerp)
        {

            arrow2.enabled = true;
            treeLeef2.enabled = true;
            operation.text = " LERP ";
            arrow4.enabled = true;
            treeLeefU.enabled = true;
        }
    }
}
