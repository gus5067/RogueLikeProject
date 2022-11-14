using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBox : MonoBehaviour,IInteractable
{
    [SerializeField] GameObject inventoryUI;

    public void Interaction()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }
}
