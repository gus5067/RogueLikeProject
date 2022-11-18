using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemData/Weapon")]
public class WeaponItemData : ItemData
{
    public int value;

    public Vector2 hitArea;

    public int power;
}
