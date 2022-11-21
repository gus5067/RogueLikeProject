using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : Singleton<EquipManager>
{
    public WeaponItemData curWeaponData;
    public ArmorItemData curArmorData;
    public ServantData curServantData;
    public void SetEquip(ItemData item)
    {
        switch (item.type)
        {
            case ItemType.Weapon:
                curWeaponData = item as WeaponItemData;
                break;
            case ItemType.Armor:
                curArmorData = item as ArmorItemData;
                break;
            case ItemType.Servant:
                curServantData = item as ServantData;
                break;
        }
    }
}
