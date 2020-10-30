/*
File name: HierarchyState.cs
Purpose:  This data structure contains a world transform and local transform 
to describe an object in space
Contributors: Nick Brennan-Martin and Bradley Chamberlain
Collaborated on one PC
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialPose : MonoBehaviour
{

    public Matrix4x4 worldPose; //World Space
    
    //object space
    public Vector3 orientation;
    public Vector3 scale;
    public Vector3 translation;

    
    //defualt
    public SpatialPose()
    {
        worldPose = Matrix4x4.identity;//should be pose = Transform.Identity
        orientation = new Vector3(0f, 0f, 0f);
        scale = new Vector3(1f, 1f, 1f);
        translation = new Vector3(0f, 0f, 0f);
    }

    //constructor
    public SpatialPose(Matrix4x4 p, Vector3 o, Vector3 s, Vector3 t)
    {

        worldPose = p;
        orientation = o;
        scale = s;
        translation = t;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = translation;
    }

    //identity blend
    public SpatialPose Identity()
    {
        SpatialPose newIdentPose = new SpatialPose(Matrix4x4.identity, Vector3.one, Vector3.one, Vector3.zero);
        return newIdentPose;
    }

    //construct blend
    public SpatialPose Construct(SpatialPose controlPose, SpatialPose plusPose)
    {
        controlPose.orientation += plusPose.orientation;
        controlPose.translation += plusPose.translation;
        controlPose.scale += plusPose.scale;
        //controlPose.worldPose += plusPose.worldPose;
        return controlPose;
    }

    //copy blend
    public SpatialPose Copy(SpatialPose copy)
    {
        SpatialPose poseCopy = new SpatialPose();
        poseCopy = copy;
        return copy;
    }

    //invert blend
    public SpatialPose InvertPose(SpatialPose inPose)
    {
        SpatialPose poseInv = new SpatialPose();
        poseInv.scale = -inPose.scale;
        poseInv.translation = -inPose.translation;
        poseInv.orientation = -inPose.orientation;
        return poseInv;
    }
}
