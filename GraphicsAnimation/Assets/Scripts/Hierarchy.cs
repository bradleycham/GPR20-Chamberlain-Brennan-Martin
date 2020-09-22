using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hierarchy : MonoBehaviour
{

    HierarchyNode[] treeDepth;
    int nodeCount;

    Hierarchy(HierarchyNode[] list, int count)
    {

        treeDepth = list;
        nodeCount = count;
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
