using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private Canvas canvas;
    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;

    private InventoryUnit[] inventoryUnits;
    
    [SerializeField]
    private InventoryUnit currentInventoryUnit; //���� ������ ����

    private ItemData tempItem; // ���� ������ ������ �ӽ� ����;
    [SerializeField]
    private Transform tempImagePos;

    private Vector3 tempPos;

    private int siblingIndex;

    private bool isDrag;

    [SerializeField]
    private InventoryUnit target;

    [SerializeField]
    private InventoryUnit nextTarget;

    private void Awake()
    {
        canvas = transform.GetComponentInParent<Canvas>();
        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        pointerEventData = new PointerEventData(null);
    }

    private void OnEnable()
    {
        InventoryManager.Instance.inventoryUI = this;
        InventoryUIUpdate();
    }
    private void Update()
    {
        target = UIMouseRay(0);
        if(!isDrag && target != null)
        {
            OnDragBegin();
        }
        OnDragStay();
        OnDragEnd();
    }
    public void InventoryUIUpdate()
    {
        inventoryUnits = GetComponentsInChildren<InventoryUnit>();
        if (InventoryManager.Instance.items.Count <= 0)
        {
            foreach(InventoryUnit unit in inventoryUnits)
            {
                unit.RemoveItem();
            }
            return;
        }
            
        for (int i = 0; i < inventoryUnits.Length; i++)
        {
            if (i < InventoryManager.Instance.items.Count)
                inventoryUnits[i].Item = InventoryManager.Instance.items[i];
            else
                inventoryUnits[i].Item = null;
        }

        foreach (InventoryUnit unit in inventoryUnits)
        {
            unit.AddItem(unit.Item);
        }
    }

    public InventoryUnit UIMouseRay(int i)
    {
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new();

        graphicRaycaster.Raycast(pointerEventData, results);

        if (results.Count > i)
        {
                InventoryUnit targetObj = results[i].gameObject.GetComponentInParent<InventoryUnit>();
                if (targetObj != null)
                    return targetObj;
                else
                   return null;
        }
        else
            return null;
    }

    public void OnDragBegin()
    {
        if(target.Item != null && Input.GetMouseButtonDown(0))
        {
            isDrag = true;
            currentInventoryUnit = target;
            tempItem = currentInventoryUnit.Item; //�巡�� ������ ������ ������
            tempImagePos = currentInventoryUnit.iconTransform;
            siblingIndex = tempImagePos.transform.GetSiblingIndex();//�ڽ� ���� ����
            tempImagePos.transform.SetParent(canvas.transform);
            tempPos = tempImagePos.transform.position;
        }
    }

    public void OnDragStay()
    {
        if (currentInventoryUnit != null && Input.GetMouseButton(0))
        {
            tempImagePos.position = Input.mousePosition;
        }
    }

    public void OnDragEnd()
    {
        nextTarget = UIMouseRay(1);
        if (Input.GetMouseButtonUp(0) && currentInventoryUnit != null)
        {
            
            tempImagePos.transform.SetParent(currentInventoryUnit.transform);
            tempImagePos.transform.SetSiblingIndex(siblingIndex);
            currentInventoryUnit.iconTransform.position = tempPos;

            if (nextTarget != null)
            {
                if (nextTarget is EquipmentUnit)
                    SwapUnit(currentInventoryUnit, nextTarget as EquipmentUnit, tempItem.type);
                else
                    SwapUnit(currentInventoryUnit, nextTarget);
            }
            currentInventoryUnit = null;
        }
        isDrag = false;
    }

    public void SwapUnit(InventoryUnit curUnit, InventoryUnit targetUnit)
    {
        Debug.Log(curUnit + " �� " + targetUnit + " ���� ����");

        if(curUnit.Item != null && targetUnit.Item == null)
        {
            targetUnit.AddItem(curUnit.Item);
            curUnit.RemoveItem(); 
        }
        else if(curUnit.Item != null)
        {
            tempItem = targetUnit.Item;
            targetUnit.AddItem(curUnit.Item);
            curUnit.AddItem(tempItem);
        }
        
    }

    public void SwapUnit(InventoryUnit curUnit, EquipmentUnit targetUnit, ItemType type)
    {
        Debug.Log(curUnit + " �� " + targetUnit + " ���� ����");

        if (targetUnit.slotType != type)
            return;

        if (targetUnit.Item == null)//Ÿ�� ��ĭ, ���� ������ ����
        {
            Debug.Log("ù ��° �б�");
            if (curUnit.Item == null)
                return;
            targetUnit.AddItem(curUnit.Item);
            InventoryManager.Instance.Remove(curUnit.Item, InventoryManager.Instance.items);
        }
        else if (curUnit.Item != null)//Ÿ�ٿ� ������ ����, ���� ������ ����
        {
            Debug.Log("�� ��° �б�");
            tempItem = targetUnit.Item; //temp�� ����� ����
            targetUnit.AddItem(curUnit.Item);//���ĭ ������ ����
            InventoryManager.Instance.Remove(curUnit.Item, InventoryManager.Instance.items);// �κ��丮 â �� �����
            InventoryManager.Instance.AddItem(tempItem, InventoryManager.Instance.items);
        }
    }

}
