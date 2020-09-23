using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HierarchyState : MonoBehaviour
{

    Hierarchy hierarchy;
    HierarchicalPose samplePose;
    HierarchicalPose localSpacePose;
    HierarchicalPose objectSpacePose;

    HierarchyState(Hierarchy h, HierarchicalPose sp, HierarchicalPose lsp, HierarchicalPose osp)
    {

        hierarchy = h;
        samplePose = sp;
        localSpacePose = lsp;
        objectSpacePose = osp;
    }

    public void ForwwardKinematic(HierarchyState hs)
    {

        for(int i = 0; i < hs.hierarchy.nodeCount; i++)
        {

            if(hs.hierarchy.treeDepth[i].parentIndex == -1)
            {

                objectSpacePose = localSpacePose;
            }

            else
            {

                //objectSpacePose = hs.hierarchy.treeDepth[i].parentIndex
            }
        }
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
