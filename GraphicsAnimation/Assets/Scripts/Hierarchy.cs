
/*
File name: HierarchyState.cs
Purpose:  This data structure uses a tree algorithm to sort 
nodes by relation to their parent
Contributors: Nick Brennan-Martin and Bradley Chamberlain
Collaborated on one PC
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hierarchy : MonoBehaviour
{

    //node tree length and count
    public HierarchyNode[] treeDepth;
    public int nodeCount;

    //constructor
    public Hierarchy(HierarchyNode[] list, int count)
    {

        treeDepth = list;
        nodeCount = count;
    }
    public Hierarchy(int count)
    {
        nodeCount = count;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
