using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject storeUI;

    [SerializeField] ShopSlot[] shopSlots; //���� ���� �����۵�

    [SerializeField] ItemData[] shopItems; //������ ���� �̸� ������ ���
    public void Interaction()
    {
        storeUI.SetActive(!storeUI.activeSelf);

        for (int i = 0; i < shopSlots.Length; i++)
        {
            shopSlots[i].SetShopSlot(shopItems[i]);
        }
    }


}
