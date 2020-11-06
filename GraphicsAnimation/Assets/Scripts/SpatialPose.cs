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
    public Vector3 translation;
    public Vector3 orientation;
    public Vector3 scale;
    

    
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
        transform.localRotation = Quaternion.Euler(orientation);
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

    //smoothstep interpolation  blend oepration
    public SpatialPose Smoothstep(SpatialPose hp, SpatialPose hp2, float t)
    {

        SpatialPose temp = hp;

            float alpha = t * t * (3 - 2 * t);

            temp.translation = hp.translation * alpha + (hp2.translation * (1 - alpha));
            temp.orientation = hp.orientation * alpha + (hp2.orientation * (1 - alpha));
            temp.scale = hp.scale * alpha + (hp2.scale * (1 - alpha));

        return temp;
    }

    //descale blend oepration
    public SpatialPose Descale(SpatialPose samplePose, SpatialPose nextPose, float t)
    {

        SpatialPose temp = samplePose;
        SpatialPose invert = InvertPose(nextPose);

            temp.scale = samplePose.scale * (1 - t) + invert.scale * t;

        return temp;
    }

    //convert blend oepration
    public SpatialPose Convert(SpatialPose samplePose)
    {

        SpatialPose temp = samplePose;


            temp.worldPose = Matrix4x4.TRS(
                samplePose.translation,
                Quaternion.Euler(samplePose.orientation.x, samplePose.orientation.y, samplePose.orientation.z),
                samplePose.scale);


        return temp;
    }

    //revert blend oepration
    public SpatialPose Revert(SpatialPose samplePose)
    {

        SpatialPose temp = samplePose;


            temp.translation = new Vector3(samplePose.worldPose.m00, samplePose.worldPose.m01, samplePose.worldPose.m02);
            temp.orientation = new Vector3(samplePose.worldPose.m10, samplePose.worldPose.m11, samplePose.worldPose.m12);
            temp.scale = new Vector3(samplePose.worldPose.m20, samplePose.worldPose.m21, samplePose.worldPose.m22);

        return temp;
    }

    //forwardkinematic blend oepration
    public SpatialPose ForwaredKinematic(Hierarchy hier, Matrix4x4[] localTransform, Matrix4x4[] objectTransform, SpatialPose samplePose)
    {

        SpatialPose temp = samplePose;

        for (int i = 0; i < hier.treeDepth.Length; i++)
        {
            if (hier.treeDepth[i].parentIndex == -1)
            {

                objectTransform[i] = localTransform[i];
            }
            else // forward kinematics
            {

                objectTransform[i] = objectTransform[hier.treeDepth[i].parentIndex] * localTransform[i].transpose;
            }
        }

        for (int i = 0; i < hier.treeDepth.Length; i++)
        {
            //samplePose.transform.position = samplePose.translation + hier.treeDepth[i + ].parentIndex;
        }

        return temp;
    }

    //inverse kinematic blend oepration
    public SpatialPose InverseKinematic(Hierarchy hier, Matrix4x4[] localTransform, Matrix4x4[] objectTransform, SpatialPose samplePose)
    {

        SpatialPose temp = samplePose;

        for (int i = 0; i < hier.treeDepth.Length; i++)
        {
            if (hier.treeDepth[i].parentIndex == -1)
            {

                localTransform[i] = objectTransform[i];
            }
            else // forward kinematics
            {

                localTransform[hier.treeDepth[i].parentIndex] = localTransform[i] * objectTransform[i].transpose;
            }
        }

        for (int i = hier.treeDepth.Length; i > 0; i--)
        {

            //temp.currentPose[hier.treeDepth[i].parentIndex].transform.position = samplePose.currentPose[i].translation + samplePose.currentPose[hier.treeDepth[i].parentIndex].translation;
        }

        return temp;
    }
}
