using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUnit : MonoBehaviour
{
    private ItemData item;
    [SerializeField]
    private Image slotImage;

    public void AddItem(ItemData item)
    {
        this.item = item;
        slotImage.sprite = item.icon;
    }

    public void RemoveItem()
    {
        this.item = null;
        slotImage.sprite = null;
    }
}
