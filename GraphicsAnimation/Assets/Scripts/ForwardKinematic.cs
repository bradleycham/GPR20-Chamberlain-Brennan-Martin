using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardKinematic : MonoBehaviour
{
    public void KinematicsSolveForwardPartial(HierarchyState hState, int first, int nodeCount)
    {
        for(int i = 0; i < hState.samplePose.currentPose.Length; i ++)
        {
            if (hState.hierarchy.treeDepth[i].parentIndex < 0)
            {
                // this is the root node
                hState.objectSpacePose.currentPose[i].orientation = hState.localSpacePose.currentPose[i].orientation;
                hState.objectSpacePose.currentPose[i].translation = hState.localSpacePose.currentPose[i].translation;
                hState.objectSpacePose.currentPose[i].scale = hState.localSpacePose.currentPose[i].scale;
            }
            else // forward kinematics
            {
                hState.objectSpacePose.currentPose[i].orientation = hState.objectSpacePose.currentPose[hState.hierarchy.treeDepth[i].parentIndex].orientation + hState.localSpacePose.currentPose[i].orientation;
                hState.objectSpacePose.currentPose[i].translation = hState.objectSpacePose.currentPose[hState.hierarchy.treeDepth[i].parentIndex].translation + hState.localSpacePose.currentPose[i].translation;
                hState.objectSpacePose.currentPose[i].scale = new Vector3((hState.objectSpacePose.currentPose[hState.hierarchy.treeDepth[i].parentIndex].scale.x * hState.localSpacePose.currentPose[i].scale.x),
                    (hState.objectSpacePose.currentPose[hState.hierarchy.treeDepth[i].parentIndex].scale.y * hState.localSpacePose.currentPose[i].scale.y),
                    (hState.objectSpacePose.currentPose[hState.hierarchy.treeDepth[i].parentIndex].scale.z * hState.localSpacePose.currentPose[i].scale.z));
            }
        }

        Debug.Log("hello");
    }


}
