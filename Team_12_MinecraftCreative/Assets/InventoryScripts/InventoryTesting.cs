using System;
using System.Collections;
using System.Collections.Generic;
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
    
    //check open or not
    public bool isInventoryOpen;
    

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
                invHUD.SetActive(false);
                isInventoryOpen = false;
                Time.timeScale = 1f;
            }
            else if (!invHUD.activeSelf)
            {
                invHUD.SetActive(true);
                isInventoryOpen = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {

            itemGenerated = GenerateItem();
            itemStackSize = Random.Range(1, 20);


            // Generate and add a random item to inventory
            if (invSystem.AddToInventory(itemGenerated, itemStackSize))
            {
                Debug.Log("Item got added to Invenotry");
            }
            else
            {
                Debug.Log("Cannot add to inventory");
            }

        }
    }

}
