using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EquipManager : Singleton<EquipManager>
{
    public WeaponItemData curWeaponData;
    public ArmorItemData curArmorData;
    public ServantData curServantData;

    private new void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += ClearManager;
    }

    public void ClearManager(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "MiningCaveScene")
            return;
        curWeaponData = null;
        curArmorData = null;
        curServantData = null;
    }
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
