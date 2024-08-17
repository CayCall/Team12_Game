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

    public float scrollScale = 0.1f;

    ItemData indicatedItem;


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


    // Start is called before the first frame update
    void Start()
    {
        activeInvSystem = GetComponent<InventorySystem>();
    }

    // Update is called once per frame
    void Update()
    {

        Indicating();

        Matching();




        if (Input.mouseScrollDelta.y * scrollScale * Time.deltaTime > 0)
        {
            Debug.Log("If you had a weapon");

          //coroutine 

        }
        else if (Input.mouseScrollDelta.y * scrollScale * Time.deltaTime < 0)
        {
            Debug.Log("It would have equipped");

      
        }


    }
}
