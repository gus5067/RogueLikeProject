using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemData/item")]
public abstract class ItemData : ScriptableObject
{
    public new string name;
    public Sprite icon;

}
