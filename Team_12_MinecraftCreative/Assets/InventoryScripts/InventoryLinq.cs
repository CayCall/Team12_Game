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


    void Indicating()
    {
       
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

    // Start is called before the first frame update
    void Start()
    {
        activeInvSystem = GetComponent<InventorySystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Matching();
    }
}
