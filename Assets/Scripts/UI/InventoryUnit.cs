using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class InventoryUnit : MonoBehaviour
{
    [SerializeField]
    protected ItemData item;
    public virtual ItemData Item { get { return item; } set { item = value; } }

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
        this.Item = item;
        slotImage.sprite = item.icon;
    }

    public void RemoveItem()
    {
        this.Item = null;
        slotImage.sprite = backgroundImage;
    }

}
