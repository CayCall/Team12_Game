using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{

    public List<InventorySlot> slots;

    bool itemAdded;


    //Maybe we can have a type of bool to make sure the item doesn't already exist in our system, 
    //before we go ahead and add it into a null space

    public InventorySystem(int size)
    {
        slots = new List<InventorySlot>(size);

        for (int i = 0; i < size; i++)
        {
            slots.Add(new InventorySlot());
        }
    }



    public bool AddToInventory(ItemData itemToAdd, int amountToAdd)
    {

        foreach (InventorySlot slot in slots)
        {
            if (slot.itemData == itemToAdd)
            {
                if (!itemAdded)
                {

                }
                slot.AddToStack(amountToAdd);
                Debug.Log("We added on top of a system");
                //itemAdded = true;
                return true;

            }
            else if (slot.itemData != null)
            {
                Debug.Log("This is filled up");

            }

           
        }

        //split so that we ensure all slots  are searched first before we try put inventory into an empty slot
        foreach(InventorySlot slot in slots)
        {
            if(slot.itemData == null)
            {
                slot.UpdateInventorySlot(itemToAdd,amountToAdd);
                Debug.Log("we found an empty slot");
                return true;
            }
        }


        /* else
         {
             itemAdded = false;
         }*/
        Debug.Log("No space in inventory");
        return false;

    }


    public int LinkAmmo(ItemData ammoType)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            InventorySlot slot = slots[i];
            if (slot.itemData == ammoType)
            {
                return slot.stackSize;

            }

        }

        return -1;
    }



    public void TakeAmmo(ItemData ammoType, int amountToTake)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            InventorySlot slot = slots[i];
            if (slot.itemData == ammoType)
            {
                slot.RemoveFromStack(amountToTake);
                //Debug.Log("We took from the system");
                break;
            }

        }
    }

/*
    public void CheckHighlights(InventorySlot invSlot)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot != invSlot)
            {
                slot.GetComponent<InventoryDisplay>().HighlightOff();
            }
        }
    }*/


    public void CheckActive(InventorySlot invSlot)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot != invSlot)
            {
                slot.GetComponent<InventoryDisplay>().ActivateOff();
            }
        }
    }


    /*public void BtnLootToDrop()
    {
        foreach (InventorySlot slot in slots)
        {
            InventoryDisplay slotObj = slot.GetComponent<InventoryDisplay>();
            if (slotObj.GetInvSlotHighlight())
            {
                slotObj.LootToDrop();
                return;
            }
        }
    }
*/

  /*  public void BtnWeaponToActivate()
    {
        foreach (InventorySlot slot in slots)
        {
            InventoryDisplay slotObj = slot.GetComponent<InventoryDisplay>();
            if (slotObj.GetInvSlotHighlight())
            {
                slotObj.ActivateWeapon();
                return;
            }
        }
    }*/

   /* public void LootWeaponToActivate(ItemData lootedItem)
    {
        foreach (InventorySlot slot in slots)
        {
            InventoryDisplay slotObj = slot.GetComponent<InventoryDisplay>();
            if (lootedItem == slot.itemData)
            {
                GameObject findObj = GameObject.Find("ActiveWeapon");
                ActiveWeapon lootWield = findObj.GetComponent<ActiveWeapon>();
                lootWield.SetWeapon(slot.itemData);

                slotObj.DefaultActivate();
                return;
            }
        }
    }
*/
  /*  public void LootWeaponToDeactivation(ItemData lootedItem)
    {
        foreach (InventorySlot slot in slots)
        {
            InventoryDisplay slotObj = slot.GetComponent<InventoryDisplay>();
            if (lootedItem == slot.itemData)
            {
                CheckActive(slot);
                CheckHighlights(slot);
                return;
            }
        }
    }
*/
  /*  public void WeaponMouseWheelDown(int mouseWeapon)
    {

        if (mouseWeapon == -1)
        {
            Debug.Log("Error expected");
        }

        else if (mouseWeapon != -1)
        {
            *//*  foreach (InventorySlot slot in slots)
              {
                  InventoryDisplay slotObj = slot.GetComponent<InventoryDisplay>();
                  if (slot.itemData.weaponType == true)
                  {

                          GameObject findObj = GameObject.Find("ActiveWeapon");
                          ActiveWeapon mouseWield = findObj.GetComponent<ActiveWeapon>();
                          mouseWield.SetWeapon(slot.itemData);

                          slotObj.DefaultActivate();
                          return;

                  }

              }*//*


            for (int i = mouseWeapon + 1; i < slots.Count; i++)
            {

                InventorySlot slot = slots[i];
                InventoryDisplay slotObj = slot.GetComponent<InventoryDisplay>();
                if (slot.itemData != null)
                {
                    if (slot.itemData.weaponType == true)
                    {
                        GameObject findObj = GameObject.Find("ActiveWeapon");
                        ActiveWeapon mouseWield = findObj.GetComponent<ActiveWeapon>();
                        mouseWield.SetWeapon(slot.itemData);

                        slotObj.DefaultActivate();
                        return;
                    }
                }


            }
        }
    }*/


 /*   public int WeaponNumber(ItemData mouseWeapon)
    {
        int weaponNumber = 0;

        for (int i = 0; i < slots.Count; i++)
        {
            InventorySlot slot = slots[i];
            if (slot.itemData == mouseWeapon)
            {
                weaponNumber = i;
                return weaponNumber;
            }
        }

        return -1;
    }
*/
 /*   public void WeaponMouseWheelUp(int mouseWeapon)
    {

        if (mouseWeapon == -1)
        {
            Debug.Log("Error expected");
        }

        else if (mouseWeapon != -1)
        {


            for (int i = mouseWeapon - 1; i >= 0; i--)
            {

                InventorySlot slot = slots[i];
                InventoryDisplay slotObj = slot.GetComponent<InventoryDisplay>();
                if (slot.itemData != null)
                {
                    if (slot.itemData.weaponType == true)
                    {
                        GameObject findObj = GameObject.Find("ActiveWeapon");
                        ActiveWeapon mouseWield = findObj.GetComponent<ActiveWeapon>();
                        mouseWield.SetWeapon(slot.itemData);

                        slotObj.DefaultActivate();
                        return;
                    }
                }
            }
        }
    }
*/

    // Start is called before the first frame update
    void Start()
    {
        itemAdded = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
