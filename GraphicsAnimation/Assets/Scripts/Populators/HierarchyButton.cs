using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HierarchyButton : MonoBehaviour
{
    // Start is called before the first frame update
    HierarchyPopulator pop;

    void Start()
    {
       //pop = ScriptableObject.CreateInstance<HierarchyPopulator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickButton()
    {
        //Debug.Log("the button is clicked");
        pop.CreateNewHierarchy(5);
    }
}
