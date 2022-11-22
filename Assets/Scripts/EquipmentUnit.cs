using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EquipmentUnit : InventoryUnit
{
    public ItemType slotType;

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

    private void OnEnable()
    {
        switch (slotType)
        {
            case ItemType.Weapon:
                if (EquipManager.Instance.curWeaponData != null)
                    AddItem(EquipManager.Instance.curWeaponData);
                break;
            case ItemType.Armor:
                if (EquipManager.Instance.curArmorData != null)
                    AddItem(EquipManager.Instance.curArmorData);
                break;
            case ItemType.Servant:
                if (EquipManager.Instance.curArmorData != null)
                    AddItem(EquipManager.Instance.curServantData);
                break;
        }
    }
}
