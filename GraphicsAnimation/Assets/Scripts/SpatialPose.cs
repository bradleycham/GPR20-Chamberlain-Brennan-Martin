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

    SpatialPose(Matrix4x4 p, Vector3 o, Vector3 s, Vector3 t)
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
        //this.transform.position = new Vector3(worldPose.GetColumn(3).x, worldPose.GetColumn(3).y, worldPose.GetColumn(3).z);
        //this.transform.rotation = ;
        //this.transform.localScale = new Vector3(worldPose.GetRow(3).x, worldPose.GetRow(3).y, worldPose.GetRow(3).z);
    }
}
