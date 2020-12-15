
/*
File name: SpawnRagdoll
Purpose:  This script will spawn an appropriate ragdoll when the player inputs Q
Contributors: Nick Brennan-Martin and Bradley Chamberlain
Collaborated on one PC
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRagdoll : MonoBehaviour
{
    public GameObject doll;
    public GameObject bones;
    //public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            //doll.transform.parent = null;
            //Vector3 camPos = cam.transform.position;
            bones.SetActive(false);
            doll.SetActive(true);
            //cam.transform.parent = doll.transform;
            //cam.transform.position = camPos;
        }
    }
}
