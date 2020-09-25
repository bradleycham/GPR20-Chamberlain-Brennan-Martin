/*
File name: HierarchicalPosePool.cs
Purpose:  This data structura is used for pooling together
hierarchical poses
Contributors: Nick Brennan-Martin and Bradley Chamberlain
Collaborated on one PC
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Euler Channels
enum Channel
{
    //for future implementaiton
    rotation,
    translation
}

public class HierarchicalPosePool : MonoBehaviour
{

    //elements of a hierarchicalPosePool
    public Hierarchy nodePool;
    //public SpatialPose[] spatialPoses; // do we need a list of all poses when we dont use pointers?
    public HierarchicalPose[] hierarchicalPoses;
    //Euler order - global flag
    public int hierarchicalPoseCount;
    public int spatialPoseCount;

    //constructors
    HierarchicalPosePool(Hierarchy h, /*SpatialPose[] sp,*/ HierarchicalPose[] hp, int hCount, int sCount)
    {
        nodePool = h;
        //spatialPoses = sp;
        hierarchicalPoses = hp;
        hierarchicalPoseCount = hCount;
        spatialPoseCount = sCount;
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
