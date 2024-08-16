using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject firstPerson;
    private GameObject ThirdPerson;
    [SerializeField]private GameObject[] Cameras;
    private int changeTime;

    private void Start()
    {
        GetReferences();
    }

    private  void GetReferences()
    {
        firstPerson = GameObject.Find("Main Camera");
        ThirdPerson = GameObject.Find("ThirdPersonCamera");
    }

    private void Update()
    {
        changeCamera();   
    }

    private void changeCamera()
    {
       if (Input.GetKeyDown(KeyCode.G))
        {
            
        }   
    }
}
