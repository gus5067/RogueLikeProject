using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class InventoryUnit : MonoBehaviour
{
    public ItemData item;

    public Transform icon;
    [SerializeField] private Image slotImage;

    private void Start()
    {
        icon = transform.GetChild(0).transform.GetChild(1).transform;
    }
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
