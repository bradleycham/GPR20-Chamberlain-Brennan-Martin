using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialPose : MonoBehaviour
{

    public Transform pose; //relative to parent
    public Vector3 orientation;
    public Vector3 scale;
    public Vector3 translation;

    //defualt
    SpatialPose()
    {

        pose = null;//should be pose = Transform.Identity
        orientation = new Vector3(0f, 0f, 0f);
        scale = new Vector3(1f, 1f, 1f);
        translation = new Vector3(0f, 0f, 0f);
    }

    SpatialPose(Transform p, Vector3 o, Vector3 s, Vector3 t)
    {

        pose = p;
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
        
    }
}
