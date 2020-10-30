/*
File name: ClipBlender.cs
Purpose: blends the current pose of 2 clips
Contributors: Nick Brennan-Martin and Bradley Chamberlain
Collaborated on one PC
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Blend the current pose of two clips using a Lerp Function
    void BlendCLipsLerp()
    {
        HierarchyState pose1 = clip01.clip.keyframePool.framePool[clip01.GetFrameIndex()].GetHierarchyState();
        HierarchyState pose2 = clip02.clip.keyframePool.framePool[clip02.GetFrameIndex()].GetHierarchyState();

        outPose = pose1.Lerp2(pose1.samplePose, pose2.samplePose, u);
    }

    // Blend Clips together by simply concatenating them
    void BlendClipsAdd()
    {
        HierarchyState pose1 = clip01.clip.keyframePool.framePool[clip01.GetFrameIndex()].GetHierarchyState();
        HierarchyState pose2 = clip02.clip.keyframePool.framePool[clip02.GetFrameIndex()].GetHierarchyState();

        outPose = pose1.Concat(pose1.samplePose, pose2.samplePose);
    }

    // Blend two clips together usin the Scale blend operation
    void BlendClipsScale()
    {
        HierarchyState pose1 = clip01.clip.keyframePool.framePool[clip01.GetFrameIndex()].GetHierarchyState();
        HierarchyState pose2 = clip02.clip.keyframePool.framePool[clip02.GetFrameIndex()].GetHierarchyState();

        outPose = pose1.Scale(pose1.samplePose, pose2.samplePose, u);
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
        }
        if (type == BlendType.Concat)
        {
            BlendClipsAdd();
        }
        if (type == BlendType.Scale)
        {
            BlendClipsScale();
        }
            
    }
}
