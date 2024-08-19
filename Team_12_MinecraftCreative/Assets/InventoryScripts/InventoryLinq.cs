using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryLinq : MonoBehaviour
{
    //Reference our other inventory system, and constantly update any changes
    public InventorySystem mainInvSystem;
    InventorySystem activeInvSystem;

    //indicator on prefab component control
    GameObject highlightComponent;

    [SerializeField] int invIndex = 0;

    public float scrollScale = 0.01f;

    ItemData indicatedItem;

    float scrollTimer = 0f;
    [SerializeField]float scrollCooldown = 0.1f;


    void Indicating()
    {
       foreach(InventorySlot slot in activeInvSystem.slots)
        {
            if(activeInvSystem.slots.IndexOf(slot) == invIndex)
            {
                slot.indicator.SetActive(true);
                indicatedItem = slot.itemData; //we can then use this information to determine what kind of block to build
            }

            else
            {
                slot.indicator.SetActive(false);    
            }
        }
    }

    void Matching()
    {
        for (int i = 0; i < 9 ; i++)
        {
            InventorySlot slot = activeInvSystem.slots[i];
            slot.itemData = mainInvSystem.slots[i].itemData;
            slot.stackSize = mainInvSystem.slots[i].stackSize;
       
        }
    }


    public ItemData LinqIndicatedItem() //function to call the item you wish to build or use based on its item data
    {
        if (indicatedItem != null)
        {
            return indicatedItem;
        }

        Debug.Log("We cannot do anything");
        return null;
    }


    public void InvMouseWheelDown(int number)
    {

        if (number == -1)
        {
            Debug.Log("Error expected");
        }

        else if (number != -1)
        {
           

            for (int i = number + 1; i < activeInvSystem.slots.Count; i++)
            {

                invIndex = i;

                InventorySlot slot = activeInvSystem.slots[i];
                InventoryDisplay slotObj = slot.GetComponent<InventoryDisplay>();
                if (slot.itemData != null)
                {
                    //actions can be performed based on index

                    /*if (slot.itemData.weaponType == true)
                    {
                        GameObject findObj = GameObject.Find("ActiveWeapon");
                        ActiveWeapon mouseWield = findObj.GetComponent<ActiveWeapon>();
                        mouseWield.SetWeapon(slot.itemData);

                        slotObj.DefaultActivate();*//*
                        return;
                    }*/
                }


            }
        }
    }

    public void InvMouseWheelUp(int number)
    {

        if (number == -1)
        {
            Debug.Log("Error expected");
        }

        else if (number != -1)
        {


            for (int i = number - 1; i >= 0; i--)
            {

                invIndex = i;

                InventorySlot slot = activeInvSystem.slots[i];
                InventoryDisplay slotObj = slot.GetComponent<InventoryDisplay>();
                if (slot.itemData != null)
                {

                  //actions based of index are possible

                  /*  if (slot.itemData.weaponType == true)
                    {
                        GameObject findObj = GameObject.Find("ActiveWeapon");
                        ActiveWeapon mouseWield = findObj.GetComponent<ActiveWeapon>();
                        mouseWield.SetWeapon(slot.itemData);

                        slotObj.DefaultActivate();
                        return;
                    }*/
                }
            }
        }
    }





    // Start is called before the first frame update
    void Start()
    {
        activeInvSystem = GetComponent<InventorySystem>();
        scrollTimer = 0.3f;

        
    }

    // Update is called once per frame
    void Update()
    {

        Indicating();

        Matching();

        scrollTimer += Time.deltaTime;


        if (Input.mouseScrollDelta.y * scrollScale * Time.deltaTime > 0)
        {
            
            if(scrollTimer > scrollCooldown)
            {
                
                if (invIndex >= 8)
                {
                    invIndex = 8;
                }
                else
                {
                    invIndex++;
                    scrollTimer = 0;
                }

            }


            //InvMouseWheelUp(invIndex);

          //coroutine 

        }
        else if (Input.mouseScrollDelta.y * scrollScale * Time.deltaTime < 0)
        {
           

            if(scrollTimer > scrollCooldown)
            {
                ;
                if(invIndex <= 0)
                {
                    invIndex = 0;
                }
                else
                {
                    invIndex--;
                    scrollTimer = 0;
                }
               
            }

            //InvMouseWheelDown(invIndex);

      
        }


    }
}
