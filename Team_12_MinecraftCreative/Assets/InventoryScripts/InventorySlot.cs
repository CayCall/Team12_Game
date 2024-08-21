using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{


    public Block itemData;

    public int stackSize;

    public GameObject indicator;

    public BlockManager blockManager;


    public InventorySlot()
    {
        ClearSlot();
    }

    public InventorySlot(Block item, int amount)
    {
        itemData = item;
        stackSize = amount;
    }


    public void ClearSlot()
    {
        //Debug.Log("Slot cleared");
        itemData = null;
        stackSize = -1;
    }

    public void AsignSlot(InventorySlot invSlot)
    {
        //Debug.Log("Assigning slot " + this.gameObject);
        if (itemData == invSlot.itemData)
        {
            AddToStack(invSlot.stackSize);
        }
        else
        {
            itemData = invSlot.itemData;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }
    }


    public void UpdateInventorySlot(Block item, int amount)
    {
        itemData = item;
        stackSize = amount;
    }


    public void AddToStack(int amount)
    {
        if (stackSize + amount > itemData.SlotMax)
        {
            stackSize = itemData.SlotMax;
            int fillAmount = stackSize + amount - itemData.SlotMax;

            FillUpOtherSlot(itemData,fillAmount);
        }
        else
        {
            stackSize = stackSize + amount;
        }

    }


    public void RemoveFromStack(int amount)
    {
        if (amount >= stackSize)
        {
            TakeToZero();
        }
        else if (stackSize > 0 && amount < stackSize)
        {
            stackSize -= amount;
        }

        else
        {
            Debug.Log("No such type is available");
        }
    }


    public int RemoveRightClick()
    {
        //will determine how much to remove from a stack
        if(stackSize > 1)
        {
            int amountToRemove = stackSize / 2;

            stackSize -= amountToRemove;
            return amountToRemove;
            
        }

        else
        {
            ClearSlot();
            return 1;
        }
    }

    public void FillUpOtherSlot(Block item, int amount)
    {
        //this function will reference our inventory system, and basically look for somewhere else to add to stack
        //an empty slot within our inventory
    }

    public void TakeToZero()
    {
        Debug.Log("Depleted Slot");
        ClearSlot();
    }


    // Start is called before the first frame update
    void Start()
    {
        indicator.SetActive(false);

        blockManager = GetComponent<BlockManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
