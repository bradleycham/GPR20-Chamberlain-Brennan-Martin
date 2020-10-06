﻿/*
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

    //elements of a hierarchyState
    public Hierarchy hierarchy;

    public HierarchicalPose samplePose;
    public Matrix4x4[] objectTransformList;
    public Matrix4x4[] localTransformList;
    //public HierarchicalPose localSpacePose;
    //public HierarchicalPose objectSpacePose;

    public HierarchicalPose basePose;
    public HierarchicalPose newPose;

    //check which kinematic is used
    public bool isKinematic = false;

    //constructor
    HierarchyState(Hierarchy h, HierarchicalPose sp, Matrix4x4[] lsp, Matrix4x4[] osp)
    {

        hierarchy = h;
        samplePose = sp;
        localTransformList = lsp;
        objectTransformList = osp;
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
            ConcatenationConversion();//2
            Kinematic();
        }
        
    }

    //interpolation
    public void Interpolation(HierarchicalPose hp)
    {
        // step function, no dt involved
        for(int i = 0; i < samplePose.currentPose.Length; i++)
        {
            samplePose.currentPose[i].translation = hp.currentPose[i].translation;
            samplePose.currentPose[i].orientation = hp.currentPose[i].orientation;
            samplePose.currentPose[i].scale = hp.currentPose[i].scale;

        }
    }

    //concatentation and conversion of the matrices
    public void ConcatenationConversion()
    {

        if (basePose.currentPose.Length == samplePose.currentPose.Length)
            for(int i = 0; i < samplePose.currentPose.Length; i++)
            {
                localTransformList[i] = Matrix4x4.TRS(basePose.currentPose[i].translation + samplePose.currentPose[i].translation,
                    Quaternion.Euler(basePose.currentPose[i].orientation.x + samplePose.currentPose[i].orientation.x,
                    basePose.currentPose[i].orientation.y + samplePose.currentPose[i].orientation.y,
                    basePose.currentPose[i].orientation.z + samplePose.currentPose[i].orientation.z),
                    new Vector3
                    (samplePose.currentPose[i].scale.x * basePose.currentPose[i].scale.x,
                     samplePose.currentPose[i].scale.y * basePose.currentPose[i].scale.y,
                     samplePose.currentPose[i].scale.z * basePose.currentPose[i].scale.z));
            }
        else
            Debug.Log("ERROR: Imbalanced hierarchy lengths"); 
    }


    //forward kinematic
    public void Kinematic()
    {
        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {
            if (hierarchy.treeDepth[i].parentIndex == -1)
            {
                Debug.Log("0");
                objectTransformList[i] = localTransformList[i];
            }
            else // forward kinematics
            {
                Debug.Log("1");
                objectTransformList[i] = objectTransformList[hierarchy.treeDepth[i].parentIndex] * localTransformList[i].transpose;
            }
        }
        //Debug.Log("hello");
        //set the new position
        for (int i = 0; i < samplePose.currentPose.Length; i++)
        {
            samplePose.currentPose[i].transform.position = samplePose.currentPose[i].translation + samplePose.currentPose[hierarchy.treeDepth[i].parentIndex].translation;
            Debug.Log(hierarchy.treeDepth[i].parentIndex);
        }
    }
}
