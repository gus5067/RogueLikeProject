using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemTest : MonoBehaviour
{
    [SerializeField]
    private ItemData[] items;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            InventoryManager.Instance.AddItem(items[0], InventoryManager.Instance.items);
        if(Input.GetKeyDown(KeyCode.Alpha2))
            InventoryManager.Instance.AddItem(items[1], InventoryManager.Instance.items);
    }
}
