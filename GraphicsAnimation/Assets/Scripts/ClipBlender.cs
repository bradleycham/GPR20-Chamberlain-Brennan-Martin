/*
File name: ClipBlender.cs
Purpose: blends the current pose of 2 clips
Contributors: Nick Brennan-Martin and Bradley Chamberlain
Collaborated on one PC
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BlendType
{
    Lerp,
    Concat,
    Scale,
};



public class ClipBlender : MonoBehaviour
{
    // Start is called before the first frame update
    public ClipController clip01;
    public ClipController clip02;
    public HierarchicalPose outPose;
    [Range(0, 1)]
    public float u;
    public BlendType type;

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

    // Blend the current pose of two clips using a Lerp Function
    void BlendCLipsLerp()
    {
        //HierarchyState pose1 = clip01.clip.keyframePool.framePool[clip01.GetFrameIndex()].GetHierarchyState();
        //HierarchyState pose2 = clip02.clip.keyframePool.framePool[clip02.GetFrameIndex()].GetHierarchyState();

        //pose1.Lerp(outPose, pose1.samplePose, pose2.samplePose, u);
    }

    // Blend Clips together by simply concatenating them
    void BlendClipsAdd()
    {
        //HierarchyState pose1 = clip01.clip.keyframePool.framePool[clip01.GetFrameIndex()].GetHierarchyState();
       //HierarchyState pose2 = clip02.clip.keyframePool.framePool[clip02.GetFrameIndex()].GetHierarchyState();

        //pose1.Concat(outPose, pose1.samplePose, pose2.samplePose);
    }

    // Blend two clips together usin the Scale blend operation
    void BlendClipsScale()
    {
        //HierarchyState pose1 = clip01.clip.keyframePool.framePool[clip01.GetFrameIndex()].GetHierarchyState();
        //HierarchyState pose2 = clip02.clip.keyframePool.framePool[clip02.GetFrameIndex()].GetHierarchyState();

        //pose1.Scale(outPose, pose1.samplePose, pose2.samplePose, u);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(type == BlendType.Lerp)
        {
            BlendCLipsLerp();
            arrow2.enabled = true;
            treeLeef2.enabled = true;
            operation.text = " LERP ";
            arrow4.enabled = true;
            treeLeefU.enabled = true;
            treeLeef1.text = node0.name;
            treeLeef2.text = node1.name;
            treeLeefFinal.text = nodef.name;
        }
        if (type == BlendType.Concat)
        {
            BlendClipsAdd();
            arrow2.enabled = true;
            treeLeef2.enabled = true;
            operation.text = " + ";
            arrow4.enabled = false;
            treeLeefU.enabled = false;
            treeLeef1.text = node0.name;
            treeLeef2.text = node1.name;
            treeLeefFinal.text = nodef.name;
        }
        if (type == BlendType.Scale)
        {
            BlendClipsScale();
            arrow2.enabled = false;
            treeLeef2.enabled = false;
            operation.text = " x ";
            arrow4.enabled = true;
            treeLeefU.enabled = true;
            treeLeef1.text = node0.name;
            treeLeef2.text = node1.name;
            treeLeefFinal.text = nodef.name;
        }
            
    }
}
