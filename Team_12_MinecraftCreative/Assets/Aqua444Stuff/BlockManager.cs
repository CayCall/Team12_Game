using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class BlockManager : MonoBehaviour
{
    public Transform shootingPoint;
    [SerializeField]float shootingDistance = 8f;
    public Block BlockObject;
    //public Text BlockInfo;
    //public Block[] AvailableBuildingBlocks;
    public List <Block> AvailableBuildingBlocks = new List <Block>();
    int CurrentBlockIndex = 0;
    public Transform parent;
    public Color normalColor;
    public Color highlightedColor;
    GameObject lastHightlightedBlock;
    public InventoryLinq Linq;
    public GameObject InventoryPanel;

    public ParticleSystem Dust;
     
    private GameObject Block;
    private bool isInventoryOpen;
    private AudioManager _audioManager;

    private void Update()
    {
        if (!isInventoryOpen )
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(BlockObject != null)
                {
                   BuildBlock(BlockObject.BlockObject);
                }
              
            }
        }
       
        if (Input.GetMouseButtonDown(1))
        {
            DestroyBlock();
        }
        HighlightBlock();
        //ChangeCurretInventorySlot();
        ChangeCurretBlock();
    }

    private void Start()
    {
        //SetText();
        Linq = InventoryPanel.GetComponent<InventoryLinq>();
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void BuildBlock(GameObject block)
    {
        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out RaycastHit hitInfo,shootingDistance))
        {
            Debug.DrawRay(shootingPoint.position, shootingPoint.forward, Color.red);
            if (hitInfo.transform.tag == "Block")
            {
                Vector3 spawnPosition = new Vector3(Mathf.RoundToInt(hitInfo.point.x + hitInfo.normal.x / 2), Mathf.RoundToInt(hitInfo.point.y + hitInfo.normal.y / 2), Mathf.RoundToInt(hitInfo.point.z + hitInfo.normal.z / 2));
                Instantiate(block, spawnPosition, Quaternion.identity, parent);
                _audioManager.PlaySound("PlaceBlock");
            }
            else if (hitInfo.transform.tag == "Non-build" || hitInfo.transform.tag == "Player")
            {
                    
            }
            else
            {
                Vector3 spawnPosition = new Vector3(Mathf.RoundToInt(hitInfo.point.x), Mathf.RoundToInt(hitInfo.point.y), Mathf.RoundToInt(hitInfo.point.z));
                Instantiate(block, spawnPosition, Quaternion.identity, parent);
                  _audioManager.PlaySound("PlaceBlock");
            }
        }
    }

    void ChangeCurretBlock()
    {
        float Scroll = Input.mouseScrollDelta.y;
        if (Scroll > 0)
        {
            _audioManager.PlaySound("ItemSlotMove");
            BlockObject = null;
            CurrentBlockIndex++;
            if (CurrentBlockIndex > Linq.invIndex)
            {
                CurrentBlockIndex = 0;
            }
        }
        else if (Scroll < 0)
        {
            _audioManager.PlaySound("ItemSlotMove");
            BlockObject = null;
            CurrentBlockIndex--;
            if (CurrentBlockIndex < 0)
            {
                CurrentBlockIndex = Linq.invIndex; 
            }
        }


        foreach( Block blockItem in AvailableBuildingBlocks)
        {

           // BlockObject = AvailableBuildingBlocks[Linq.invIndex];
            if (blockItem == Linq.LinqIndicatedItem())
            {
                BlockObject = blockItem;
            }
        }
       
            
        
        //SetText();
    }

    void DestroyBlock()
    {
        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out RaycastHit hitInfo, shootingDistance))
        {
            if (hitInfo.transform.tag == "Block")
            {
                GameObject Explosion = Instantiate(Dust.gameObject,hitInfo.transform.position, Quaternion.identity);
                Destroy(Explosion, 3.0f);
                Destroy(hitInfo.transform.gameObject);
                _audioManager.PlaySound("DestroyBlock");
            }
        }
    }

    void HighlightBlock()
    {
        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out RaycastHit hitInfo, shootingDistance))
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
