using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class InventoryUnit : MonoBehaviour
{
    public ItemData item;

    public Transform iconTransform;
    [SerializeField] private Image slotImage;

    [SerializeField] private Sprite backgroundImage;

    private void Start()
    {
        if (slotImage.sprite == null)
            slotImage.sprite = backgroundImage;
        iconTransform = transform.GetChild(0).transform.GetChild(1).transform;
    }
    public void AddItem(ItemData item)
    {
        if (item == null)
        {
            RemoveItem();
            return;
        }
        this.item = item;
        slotImage.sprite = item.icon;
    }

    public void RemoveItem()
    {
        this.item = null;
        slotImage.sprite = backgroundImage;
    }

}
