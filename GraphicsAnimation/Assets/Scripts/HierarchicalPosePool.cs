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

    public Hierarchy nodePool;
    public SpatialPose[] spatialPoses;
    public HierarchicalPose[] hierarchicalPoses;
    //Euler order - global flag
    public int hierarchicalPoseCount;
    public int spatialPoseCount;

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
