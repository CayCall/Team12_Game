using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTesting : MonoBehaviour
{
    // Start is called before the first frame update

    //Inventory HUD reference
    public GameObject invHUD;
    [SerializeField] InventorySystem invSystem;

    //public List<ItemData> data;

    ItemData itemGenerated;
    int itemStackSize;

    [SerializeField] ItemData testItem1;
    [SerializeField] ItemData testItem2;
    [SerializeField] ItemData testItem3;
    [SerializeField] ItemData testItem4;

    [SerializeField] int itemGenCount = 1;

    ItemData GenerateItem()
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


    void Start()
    {
        if (invSystem == null)
        {
            GameObject invFind = GameObject.Find("InventoryPanel");
            invSystem = invFind.GetComponent<InventorySystem>();
        }
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
                Time.timeScale = 0f;
            }
            else if (!invHUD.activeSelf)
            {
                invHUD.SetActive(true);
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.None;
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
