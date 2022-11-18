using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ShopSlot : MonoBehaviour
{
    public ItemData shopSlotData;

    public Image image;

    TextMeshProUGUI tooltip;

    private void Awake()
    {
        image = transform.GetChild(1).GetComponent<Image>();
        tooltip = TooltipManager.Instance.tooltip;
    }
    public void SetShopSlot(ItemData itemData)
    {
        shopSlotData = itemData;
        image.sprite = itemData.icon;
    }

    public void Purchase()
    {
        if (InventoryManager.Instance.items.Count >= 48)
            return;
        GameManager.Instance.Money -= shopSlotData.price;
        InventoryManager.Instance.AddItem(shopSlotData, InventoryManager.Instance.items);
    }

    public void ShowTooltip()
    {
        tooltip.transform.parent.gameObject.SetActive(true);
        tooltip.transform.parent.position = transform.position;
        tooltip.text = shopSlotData.toolTip;
    }

    public void ShowOffTooltip()
    {
        tooltip.text = null;
        tooltip.transform.parent.gameObject.SetActive(false);
    }
}
