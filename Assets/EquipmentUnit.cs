using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EquipmentUnit : InventoryUnit
{
    public ItemType slotType;

    public ItemData preItem;

    public override ItemData Item
    {
        get => base.Item;
        set
        {
            Debug.Log("프로퍼티 들어옴");
            base.Item = value;
            EquipManager.Instance.SetEquip(base.Item);
        }


    }
}
