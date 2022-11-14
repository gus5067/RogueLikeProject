using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] InventoryUI inventoryUI;

    public List<ItemData> items = new List<ItemData>();//µ¿Àû


    public void AddItem(ItemData item)
    {
        if (items.Count >= 48)
            return;
        items.Add(item);
        inventoryUI.InventoryUIUpdate();
    }

    public void Remove(ItemData item)
    {
        if (items.Count <= 0)
            return;
        items.Remove(item);
        inventoryUI.InventoryUIUpdate();
    }
}
