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

    public void Interpolation(HierarchicalPosePool hpp)
    {

        samplePose.currentPose = hpp.spatialPoses;
    }

    public void Concatentation()
    {

        for(int i = 0; i < localSpacePose.currentPose.Length; i++)
        {

            localSpacePose.currentPose[i].translation += samplePose.currentPose[i].translation;
        }
    }

    public void Conversion()
    {

        Matrix4x4 trans = Matrix4x4.TRS(localSpacePose.currentPose[0].translation, Quaternion.identity, localSpacePose.currentPose[0].scale);
    }

    public void Kinematic()
    {

        ForwardKinematic ks = new ForwardKinematic();
        ks.KinematicsSolveForwardPartial(this, 0, 12);
    }
}
