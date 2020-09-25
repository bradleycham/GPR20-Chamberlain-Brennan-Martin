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
    public SpatialPose[] currentPose;
    HierarchicalPose(SpatialPose[] cp)
    {
        currentPose = cp;
    }
    public HierarchicalPose(int length)
    {
        currentPose = new SpatialPose[length];
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
