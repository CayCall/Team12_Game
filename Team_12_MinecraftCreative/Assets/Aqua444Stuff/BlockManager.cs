using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BlockManager : MonoBehaviour
{
    public Transform shootingPoint;

    private GameObject BlockObject;

    private Vector3 mousePos;
    private Vector3 objectPos;

    public GameObject Block;

    public Transform parent;

    private Color normalColor;
    public Color highlightedColor;

    [SerializeField] Renderer Rendered;

    GameObject lastHightlightedBlock;

    public List<GameObject> Blocks = new List<GameObject>();

    private void Start()
    {
        BlockObject = Block;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Block = Blocks[0];
            normalColor = Block.GetComponent<MeshRenderer>().sharedMaterial.color;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Block = Blocks[1];

            normalColor = Block.GetComponent<MeshRenderer>().sharedMaterial.color;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Block = Blocks[2];
            normalColor = Block.GetComponent<MeshRenderer>().sharedMaterial.color;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Block = Blocks[3];
            normalColor = Block.GetComponent<MeshRenderer>().sharedMaterial.color;
        }


        if (Input.GetMouseButtonDown(0))
        {
            BuildBlock(BlockObject);
        }
        if (Input.GetMouseButtonDown(1))
        {
            DestroyBlock();
        }
        HighlightBlock();
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
    }
}
