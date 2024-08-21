using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( menuName = "ScriptableBlock", fileName = "New Block")]
public class Block : ScriptableObject
{
    public string BlockName;
    public GameObject BlockObject;
    public string ItemsNeededForBuildingBlock;
    public int BlockAmount;

    //public string ItemName;
    public int ItemID;
    public int SlotMax;

    public Sprite itemIcon;

    public bool weaponType;
}
