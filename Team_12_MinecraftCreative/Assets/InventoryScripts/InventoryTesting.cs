using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class InventoryTesting : MonoBehaviour
{
    // Start is called before the first frame update

    //Inventory HUD reference
    public GameObject invHUD;
    [SerializeField] InventorySystem invSystem;

    //public List<ItemData> data;

    Block itemGenerated;
    int itemStackSize;

    [SerializeField] Block testItem1;
    [SerializeField] Block testItem2;
    [SerializeField] Block testItem3;
    [SerializeField] Block testItem4;

    [SerializeField] int itemGenCount = 1;
    private AudioManager _audioManager;
    
    //check open or not
    public bool isInventoryOpen;

    private BlockManager _playerBlockManager;
    Block GenerateItem()
    {
        
        if(itemGenCount == 1)
        {

            itemGenCount++;
            return testItem1;
            
        }
        else if(itemGenCount == 2)
        {

            itemGenCount++;
            return testItem2;
        }
        else if(itemGenCount == 3)
        {

            itemGenCount++;
            return testItem3;
        }
        else if(itemGenCount == 4)
        {

            itemGenCount++;
            return testItem4;
        }
        else
        {
            itemGenCount = 1;
            itemGenCount++;
            return testItem1;
        }




    }

    private void Awake()
    {
        _playerBlockManager = GameObject.Find("Player").GetComponent<BlockManager>();
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if (invSystem == null)
        {
            GameObject invFind = GameObject.Find("InventoryPanel");
            invSystem = invFind.GetComponent<InventorySystem>();
        }
    }

    private void Start()
    {
        isInventoryOpen = false;
        invHUD.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Open or close our inventory tab
            if (invHUD.activeSelf)
            {
                isInventoryOpen = false;
                _audioManager.PlaySound("OpenMenu");
                StartCoroutine(disableBlockPlace());
                invHUD.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f;
            }
            else if (!invHUD.activeSelf)
            { 
                
                isInventoryOpen = true;
                _audioManager.PlaySound("OpenMenu");
                StartCoroutine(disableBlockPlace());
                invHUD.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f;
            }
        }
    }

    IEnumerator disableBlockPlace()
    {
        if (isInventoryOpen)
        {
            _playerBlockManager.enabled = false;
        }

        if(!isInventoryOpen)
        {
            _playerBlockManager.enabled = true;    
        }
        
        yield return isInventoryOpen = false;
    }
}
