using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{


    public ItemData itemData;

    public int stackSize;

    public InventorySlot()
    {
        ClearSlot();
    }

    public InventorySlot(ItemData item, int amount)
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


    public void UpdateInventorySlot(ItemData item, int amount)
    {
        itemData = item;
        stackSize = amount;
    }


    public void AddToStack(int amount)
    {
        if (stackSize + amount > itemData.SlotMax)
        {
            FillUpOtherSlot();
        }
        else
        {
            stackSize = stackSize + amount;
        }

    }


    public void RemoveFromStack(int amount)
    {
        if (amount > stackSize)
        {
            TakeToZero();
        }
        else if (stackSize > 0 && amount <= stackSize)
        {
            stackSize -= amount;
        }

        else
        {
            Debug.Log("No such type is available");
        }
    }

    public void FillUpOtherSlot()
    {

    }

    public void TakeToZero()
    {

    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
