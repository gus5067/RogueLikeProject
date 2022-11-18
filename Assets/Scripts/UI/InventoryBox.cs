using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBox : MonoBehaviour,IInteractable
{
    [SerializeField] GameObject[] inventoryUI;

    bool boxInteract = false;
    public void Interaction()
    {
        foreach(GameObject obj in inventoryUI)
        {
            obj.SetActive(!boxInteract);
        }
        boxInteract = !boxInteract;
    }
}
