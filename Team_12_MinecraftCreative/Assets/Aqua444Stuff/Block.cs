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
}
