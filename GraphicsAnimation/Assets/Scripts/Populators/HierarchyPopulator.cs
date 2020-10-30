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
        hierarchy = Instantiate(hierarchyTemplate, new Vector3(0f,0f,0f), Quaternion.identity);
        Hier = hierarchy.GetComponent<Hierarchy>();
        //Hierarchy tempH = new Hierarchy(0);
        //Hier = tempH; 
        for(int i = 0; i < count; ++i)
        {
            GameObject node = Instantiate(nodeTemplate, hierarchy.transform);
            Node = node.GetComponent<HierarchyNode>();
            Node.index = i;
            Node.parentIndex = i - 1;
            Hier.AppendNode(Node);
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
