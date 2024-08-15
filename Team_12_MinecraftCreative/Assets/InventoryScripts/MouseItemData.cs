using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MouseItemData : MonoBehaviour
{
    public Image itemSprite;
    public TMP_Text itemStackSize;
    public InventorySlot asgInvSlot;


    public void ClearSlot()
    {
        asgInvSlot.ClearSlot();
        itemStackSize.text = "";
        itemStackSize.color = Color.clear;
        itemSprite.sprite = null;
        itemSprite.color = Color.clear;
    }

    public void UpdateMouseSlot(InventorySlot invSlot)
    {
        asgInvSlot.UpdateInventorySlot(invSlot.itemData, invSlot.stackSize);
        itemSprite.sprite = invSlot.itemData.itemIcon;
        itemStackSize.text = invSlot.stackSize.ToString();
        itemSprite.color = Color.white;
    }



    // Start is called before the first frame update
    void Start()
    {
        itemSprite.color = Color.clear;
        itemStackSize.text = "";


    }

    // Update is called once per frame
    void Update()
    {
        if (asgInvSlot.itemData != null)
        {
            transform.position = Input.mousePosition;
        }

        if (asgInvSlot.itemData == null)
        {
            ClearSlot();
        }
    }
}
