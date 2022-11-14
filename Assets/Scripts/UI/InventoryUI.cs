using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private Canvas canvas;
    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;

    private InventoryUnit[] inventoryUnits;

    private InventoryUnit currentInventoryUnit; //현재 슬롯을 저장

    private ItemData tempData; // 현재 슬롯의 데이터 임시 저장;

    private Transform tempImagePos;

    private Vector3 tempPos;

    private int siblingIndex;

    private bool isDrag;

    [SerializeField]
    private InventoryUnit target;

    private void Awake()
    {
        canvas = transform.GetComponentInParent<Canvas>();
        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        pointerEventData = new PointerEventData(null);
    }

    private void Update()
    {
        target = UIMouseRay();

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
        for(int i =0; i< inventoryUnits.Length; i++)
        {
            if (i < InventoryManager.Instance.items.Count)
                inventoryUnits[i].AddItem(InventoryManager.Instance.items[i]);
            else
                inventoryUnits[i].RemoveItem();
        }
    }

    public InventoryUnit UIMouseRay()
    {
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        graphicRaycaster.Raycast(pointerEventData, results);

        if (results.Count > 0)
        {
            InventoryUnit targetObj = results[0].gameObject.GetComponentInParent<InventoryUnit>();
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
        if(target.item != null && Input.GetMouseButtonDown(0))
        {
            isDrag = true;
            currentInventoryUnit = target;
            tempData = currentInventoryUnit.item; //드래그 시작한 슬롯의 데이터
            tempImagePos = currentInventoryUnit.icon;
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
        if(Input.GetMouseButtonUp(0) && currentInventoryUnit != null)
        {
            tempImagePos.transform.SetParent(currentInventoryUnit.transform);
            tempImagePos.transform.SetSiblingIndex(siblingIndex);
            if (target == null)
            {

                currentInventoryUnit.icon.position = tempPos;
            }
            else
            {
                SwapUnit(currentInventoryUnit, target);
            }
        }
        InventoryUIUpdate();
        isDrag = false;
    }

    public void SwapUnit(InventoryUnit curUnit, InventoryUnit targetUnit)
    {
        if(targetUnit == null)
        {
            InventoryUnit tempUnit = new InventoryUnit();

            tempUnit.item = curUnit.item;
            curUnit.item = null;
            targetUnit.item = tempUnit.item;
        }
        else
        {
            InventoryUnit tempUnit = new InventoryUnit();

            tempUnit.item = curUnit.item;
            curUnit.item = targetUnit.item;
            targetUnit.item = tempUnit.item;
        }
    }
}
