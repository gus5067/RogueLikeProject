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
    private InventoryUnit currentInventoryUnit; //현재 슬롯을 저장

    private ItemData tempItem; // 현재 슬롯의 데이터 임시 저장;
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
            tempItem = currentInventoryUnit.Item; //드래그 시작한 슬롯의 데이터
            tempImagePos = currentInventoryUnit.iconTransform;
            siblingIndex = tempImagePos.transform.GetSiblingIndex();//자식 순서 저장
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
        Debug.Log(curUnit + " 과 " + targetUnit + " 스왑 실행");

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
        Debug.Log(curUnit + " 과 " + targetUnit + " 스왑 실행");

        if (targetUnit.slotType != type)
            return;

        if (targetUnit.Item == null)//타겟 빈칸, 현재 아이템 있음
        {
            Debug.Log("첫 번째 분기");
            if (curUnit.Item == null)
                return;
            targetUnit.AddItem(curUnit.Item);
            InventoryManager.Instance.Remove(curUnit.Item, InventoryManager.Instance.items);
        }
        else if (curUnit.Item != null)//타겟에 아이템 있음, 현재 아이템 있음
        {
            Debug.Log("두 번째 분기");
            tempItem = targetUnit.Item; //temp에 장비템 보관
            targetUnit.AddItem(curUnit.Item);//장비칸 데이터 변경
            InventoryManager.Instance.Remove(curUnit.Item, InventoryManager.Instance.items);// 인벤토리 창 템 지우기
            InventoryManager.Instance.AddItem(tempItem, InventoryManager.Instance.items);
        }
    }

}
