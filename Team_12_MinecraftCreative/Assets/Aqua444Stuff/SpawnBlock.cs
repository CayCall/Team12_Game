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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Block = Blocks[0];
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Block = Blocks[1];
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Block = Blocks[2]; 
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Block = Blocks[3];
        }

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
