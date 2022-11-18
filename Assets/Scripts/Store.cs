using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject storeUI;

    [SerializeField] ShopSlot[] shopSlots; //실제 상점 아이템들

    [SerializeField] ItemData[] shopItems; //상점에 놓일 미리 정해진 목록
    public void Interaction()
    {
        storeUI.SetActive(!storeUI.activeSelf);

        for (int i = 0; i < shopSlots.Length; i++)
        {
            shopSlots[i].SetShopSlot(shopItems[i]);
        }
    }


}
