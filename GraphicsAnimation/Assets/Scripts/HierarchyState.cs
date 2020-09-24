using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HierarchyState : MonoBehaviour
{

    public Hierarchy hierarchy;
    public HierarchicalPose samplePose;
    public HierarchicalPose localSpacePose;
    public HierarchicalPose objectSpacePose;

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
        
    }

    public void Interpolation(HierarchyState hs, HierarchicalPose hp)
    {
        // step function, no dt involved
        hs.samplePose = hp;
    }

    public void Concatentation(HierarchicalPose lhs, HierarchicalPose rhs)
    {
        if(lhs.currentPose.Length == rhs.currentPose.Length)
            for(int i = 0; i < lhs.currentPose.Length; i++)
            {
                lhs.currentPose[i].translation += rhs.currentPose[i].translation;           
                lhs.currentPose[i].orientation += rhs.currentPose[i].orientation;
                lhs.currentPose[i].scale = new Vector3
                    (lhs.currentPose[i].scale.x * rhs.currentPose[i].scale.x,
                     lhs.currentPose[i].scale.y * rhs.currentPose[i].scale.y,
                     lhs.currentPose[i].scale.z * rhs.currentPose[i].scale.z);
            }
        else
            Debug.Log("ERROR: Imbalanced hierarchy lengths"); 
    }

    public void Conversion()
    {

        for(int i = 0; i < samplePose.currentPose.Length; i ++)
        {
            samplePose.currentPose[i].pose = Matrix4x4.TRS(
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
