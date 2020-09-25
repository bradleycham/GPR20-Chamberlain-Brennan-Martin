/*
File name: HierarchyState.cs
Purpose:  This data structure contains an index and parent index
as it represents a node in a tree data structure.
Contributors: Nick Brennan-Martin and Bradley Chamberlain
Collaborated on one PC
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HierarchyNode : MonoBehaviour
{

    public string name; //name of node
    public int index; //index of node
    public int parentIndex;//-1 if this is the root node

    //constructor
    HierarchyNode(string s, int i, int pI)
    {

        name = s;
        index = i;
        parentIndex = pI;
    }

    // Start is called before the first frame update
    void Start()
    {

        name = "node";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
