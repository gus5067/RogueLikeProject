using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : Singleton<EquipManager>
{
    public WeaponItemData curWeaponData;
    public ArmorItemData curArmorData;
    public void SetEquip(ItemData item)
    {
        Debug.Log("ÇÔ¼ö ½ÇÇàµÊ");
        switch (item.type)
        {
            case ItemType.Weapon:
                curWeaponData = item as WeaponItemData;
                break;
            case ItemType.Armor:
                curArmorData = item as ArmorItemData;
                break;
        }
    }
}
