using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemTest : MonoBehaviour
{
    [SerializeField]
    private ItemData item;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            InventoryManager.Instance.AddItem(item);

    }
}
