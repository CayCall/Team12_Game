using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : MonoBehaviour
{
    private Vector3 mousePos;
    private Vector3 objectPos;
    public GameObject Block;

    public List <GameObject> Blocks = new List <GameObject>();

    private void Update()
    {
        // click left mouse button to place block
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Input.mousePosition;
            mousePos.z = 2.0f;    
            //if camera changes change camera.main
            objectPos = Camera.main.ScreenToWorldPoint(mousePos);
            //instantiate block at mouse pointer position
            Instantiate(Block, objectPos, Quaternion.identity);
        }
    }
}
