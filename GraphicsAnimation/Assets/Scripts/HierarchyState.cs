/*
File name: HierarchyState.cs
Purpose:  This is a controller-type data stucture that allows for manipulating
Hierarchical Poses
Contributors: Nick Brennan-Martin and Bradley Chamberlain
Collaborated on one PC
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HierarchyState : MonoBehaviour
{

    public Hierarchy hierarchy;

    public HierarchicalPose samplePose;
    public HierarchicalPose localSpacePose;
    public HierarchicalPose objectSpacePose;

    public HierarchicalPose basePose;
    public HierarchicalPose newPose;

    public bool isKinematic = false;

    HierarchyState(Hierarchy h, HierarchicalPose sp, HierarchicalPose lsp, HierarchicalPose osp)
    {

        hierarchy = h;
        samplePose = sp;
        localSpacePose = lsp;
        objectSpacePose = osp;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isKinematic)
        {
            Interpolation(newPose);
            Concatenation();
            Conversion();
            Kinematic();
        }
    }

    public void Interpolation(HierarchicalPose hp)
    {
        // step function, no dt involved
        samplePose = hp;
    }

    public void Concatenation()
    {
        localSpacePose = new HierarchicalPose(samplePose.currentPose.Length);
        for(int j = 0; j < samplePose.currentPose.Length; j++)
        {
            samplePose.currentPose[j] = new SpatialPose();
        }
        if (basePose.currentPose.Length == localSpacePose.currentPose.Length)
            for(int i = 0; i < localSpacePose.currentPose.Length; i++)
            {
                localSpacePose.currentPose[i].translation = basePose.currentPose[i].translation + samplePose.currentPose[i].translation;
                localSpacePose.currentPose[i].orientation = basePose.currentPose[i].orientation + samplePose.currentPose[i].orientation;
                localSpacePose.currentPose[i].scale = new Vector3
                    (samplePose.currentPose[i].scale.x * basePose.currentPose[i].scale.x,
                     samplePose.currentPose[i].scale.y * basePose.currentPose[i].scale.y,
                     samplePose.currentPose[i].scale.z * basePose.currentPose[i].scale.z);
            }
        else
            Debug.Log("ERROR: Imbalanced hierarchy lengths"); 
    }

    public void Conversion()
    {

        for(int i = 0; i < samplePose.currentPose.Length; i ++)
        {
            samplePose.currentPose[i].worldPose = Matrix4x4.TRS(
                samplePose.currentPose[i].translation,
                Quaternion.Euler(samplePose.currentPose[i].orientation.x, samplePose.currentPose[i].orientation.y, samplePose.currentPose[i].orientation.z),
                samplePose.currentPose[i].scale);
        }
    }

    public void Kinematic()
    {
        ForwardKinematic ks = new ForwardKinematic();
        ks.KinematicsSolveForwardPartial(this);
    }
}
