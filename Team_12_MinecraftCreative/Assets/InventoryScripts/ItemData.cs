using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject/Inventory Item")]
public class ItemData : ScriptableObject
{
    public string ItemName;
    public int ItemID;
    public int SlotMax;

    public Sprite itemIcon;

    public bool weaponType;

  /*  public int damage;
    public float reloadSpeed;
    public float fireRate;
    public int magazineSize;
    public int ammoMax;
    public float accuracy;
    public string ammoNeeded;*/

    [TextArea(4, 4)]
    public string Description;

}
