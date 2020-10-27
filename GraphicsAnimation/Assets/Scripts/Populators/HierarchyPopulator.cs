using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HierarchyPopulator : MonoBehaviour 
{ 
    public GameObject hierarchyTemplate;
    Hierarchy Hier;
    public GameObject nodeTemplate;
    HierarchyNode Node;
    public void CreateNewHierarchy(int count)
    {
        GameObject hierarchy;
        hierarchy = Instantiate(hierarchyTemplate, new Vector3(0f,0f,0f), Quaternion.identity) as GameObject;
        Hier = hierarchy.GetComponent<Hierarchy>();
        Hierarchy tempH = new Hierarchy(count);
        Hier = tempH; 
        for(int i = 0; i < count; ++i)
        {
            GameObject node = Instantiate(nodeTemplate, hierarchy.transform) as GameObject;
            node.transform.localPosition = new Vector3(i, 0f, 0f);
            Node = node.GetComponent<HierarchyNode>();
            Node.index = i;
            Node.parentIndex = i - 1;
            Hier.AddNode(Node,i);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //CreateNewHierarchy(5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
