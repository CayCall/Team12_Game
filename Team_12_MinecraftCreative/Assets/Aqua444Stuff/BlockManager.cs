using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class BlockManager : MonoBehaviour
{
    public Transform shootingPoint;

    private Block BlockObject;

    public Text BlockInfo;

    public Block[] AvailableBuildingBlocks;
    int CurrentBlockIndex = 0;

    private GameObject Block;

    public Transform parent;

    public Color normalColor;
    public Color highlightedColor;

    GameObject lastHightlightedBlock;

    private bool isInventoryOpen;

    private void Update()
    {
        if (!isInventoryOpen )
        {
            if (Input.GetMouseButtonDown(0))
            {
                BuildBlock(BlockObject.BlockObject);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            DestroyBlock();
        }
        HighlightBlock();
        ChangeCurretBlock();
    }

    private void Start()
    {
        SetText();
    }

    void BuildBlock(GameObject block)
    {
        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out RaycastHit hitInfo))
        {

            if (hitInfo.transform.tag == "Block")
            {
                Vector3 spawnPosition = new Vector3(Mathf.RoundToInt(hitInfo.point.x + hitInfo.normal.x / 2), Mathf.RoundToInt(hitInfo.point.y + hitInfo.normal.y / 2), Mathf.RoundToInt(hitInfo.point.z + hitInfo.normal.z / 2));
                Instantiate(block, spawnPosition, Quaternion.identity, parent);
            }
            else
            {
                Vector3 spawnPosition = new Vector3(Mathf.RoundToInt(hitInfo.point.x), Mathf.RoundToInt(hitInfo.point.y), Mathf.RoundToInt(hitInfo.point.z));
                Instantiate(block, spawnPosition, Quaternion.identity, parent);
            }
        }
    }

    void ChangeCurretBlock()
    {
        float Scroll = Input.mouseScrollDelta.y;
        if (Scroll > 0)
        {
            CurrentBlockIndex++;
            if (CurrentBlockIndex > AvailableBuildingBlocks.Length - 1)
            {
                CurrentBlockIndex = 0;
            }
        }
        else if (Scroll < 0)
        {
            CurrentBlockIndex--;
            if (CurrentBlockIndex < 0)
            {
                CurrentBlockIndex = AvailableBuildingBlocks.Length - 1; 
            }
        }
        BlockObject = AvailableBuildingBlocks[CurrentBlockIndex];
        SetText();
    }

    void SetText() //Change/ Remove this
    {
        BlockInfo.text = BlockObject.BlockName + "\n" + BlockObject.BlockAmount + "x" + BlockObject.ItemsNeededForBuildingBlock;
    }
    void DestroyBlock()
    {
        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.tag == "Block")
            {
                Destroy(hitInfo.transform.gameObject);
            }
        }
    }

    void HighlightBlock()
    {
        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.tag == "Block")
            {
                if (lastHightlightedBlock == null)
                {
                    lastHightlightedBlock = hitInfo.transform.gameObject;
                    hitInfo.transform.gameObject.GetComponent<Renderer>().material.color = highlightedColor;
                }
                else if (lastHightlightedBlock != hitInfo.transform.gameObject)
                {
                    lastHightlightedBlock.GetComponent<Renderer>().material.color = normalColor;
                    hitInfo.transform.gameObject.GetComponent<Renderer>().material.color = highlightedColor;
                    lastHightlightedBlock = hitInfo.transform.gameObject;
                }
            }
        }
        else if (lastHightlightedBlock != null)
        {
            lastHightlightedBlock.GetComponent<Renderer>().material.color = normalColor;
            lastHightlightedBlock = null;
        }
    }
}

//https://www.youtube.com/watch?v=J4y_orHzcXQ
//https://www.youtube.com/watch?v=IiSP02A_Eak
