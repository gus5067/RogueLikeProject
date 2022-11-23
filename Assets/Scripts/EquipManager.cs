using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EquipManager : Singleton<EquipManager>
{
    public WeaponItemData curWeaponData;
    [SerializeField]
    private ArmorItemData curArmorData;
    public ArmorItemData CurArmorData
    {
        get
        {
            return curArmorData;
        }
        set
        {
            curArmorData = value;
            if (value != null)
            {
                GameManager.Instance.PlayerMaxHp = playerOrgHp + value.defendValue;
            }
            else if(value == null)
            {
                GameManager.Instance.PlayerMaxHp = playerOrgHp;
            }
        }
    }
    private int playerOrgHp = 100;
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
        CurArmorData = null;
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
                CurArmorData = item as ArmorItemData;
                break;
            case ItemType.Servant:
                curServantData = item as ServantData;
                break;
        }
    }
}
