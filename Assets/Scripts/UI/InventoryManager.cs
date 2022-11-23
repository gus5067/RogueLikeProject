using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryManager : Singleton<InventoryManager>
{
    public InventoryUI inventoryUI;
    public List<ItemData> items = new List<ItemData>();

    private new void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += ClearManager;
    }

    public void ClearManager(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "MiningCaveScene")
            return;
        items.Clear();
    }


    public void AddItem(ItemData item, List<ItemData> itemList)
    {
        if (itemList.Count >= 48)
            return;
        itemList.Add(item);
        if (inventoryUI != null)
            inventoryUI.InventoryUIUpdate();
    }

    public void Remove(ItemData item, List<ItemData> itemList)
    {
        if (itemList.Count <= 0)
            return;
        itemList.Remove(item);
        if (inventoryUI != null)
            inventoryUI.InventoryUIUpdate();
    }
}
