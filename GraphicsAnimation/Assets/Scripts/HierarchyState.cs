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
}
