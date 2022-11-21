using Cinemachine.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public InventoryUI inventoryUI;
    public List<ItemData> items = new List<ItemData>();
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
