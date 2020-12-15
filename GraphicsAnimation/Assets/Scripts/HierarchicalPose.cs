/*
File name: HierarchyState.cs
Purpose:  This data structure contains a list of spatial poses that 
represent spacial data
Contributors: Nick Brennan-Martin and Bradley Chamberlain
Collaborated on one PC
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HierarchicalPose : MonoBehaviour
{

    //array of spatialPoses
    public SpatialPose[] currentPose;

    //concstructors
    public HierarchicalPose(SpatialPose[] cp)
    {
        currentPose = cp;
    }
    public HierarchicalPose(int length)
    {
        currentPose = new SpatialPose[length];
    }
    public void AddNode(SpatialPose pose, int index)
    {
        currentPose[index] = pose;
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
