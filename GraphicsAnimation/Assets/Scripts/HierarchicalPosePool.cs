using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Channel
{

    rotation,
    translation
}

public class HierarchicalPosePool : MonoBehaviour
{

    Hierarchy nodePool;
    SpatialPose[] spatialPoses;
    HierarchicalPose[] hierarchicalPoses;
    //Euler order - global flag
    int hierarchicalPoseCount;
    int spatialPoseCount;

    HierarchicalPosePool(Hierarchy h, SpatialPose[] sp, HierarchicalPose[] hp, int hCount, int sCount)
    {

        nodePool = h;
        spatialPoses = sp;
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
