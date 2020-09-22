using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HierarchyNode : MonoBehaviour
{

    string name;
    int index;
    int parentIndex;//-1 if this is the root node

    HierarchyNode(string s, int i, int pI)
    {

        name = s;
        index = i;
        parentIndex = pI;
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
