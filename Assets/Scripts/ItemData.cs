using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Weapon, Armor, Servant
}
[CreateAssetMenu(menuName = "ItemData/item")]
public abstract class ItemData : ScriptableObject
{
    public ItemType type;
    public new string name;
    public Sprite icon;
    public int price;
    [TextArea]
    public string toolTip;
}
