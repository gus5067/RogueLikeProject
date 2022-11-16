using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemData/Weapon")]
public class WeaponItemData : ItemData
{
    public string toolTip;

    public int attackValue;

    public Vector2 hitArea;

    public int power;
}
