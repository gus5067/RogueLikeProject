using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
        for(int i =0; i< inventoryUnits.Length; i++)
        {
            if (i < InventoryManager.Instance.items.Count)
                inventoryUnits[i].AddItem(InventoryManager.Instance.items[i]);
            else
                inventoryUnits[i].RemoveItem();
        }
    }

    public InventoryUnit UIMouseRay(int i)
    {
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

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
        if(target.item != null && Input.GetMouseButtonDown(0))
        {
            isDrag = true;
            currentInventoryUnit = target;
            tempItem = currentInventoryUnit.item; //�巡�� ������ ������ ������
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

            if(nextTarget != null)
            {
                SwapUnit(currentInventoryUnit, nextTarget);
            }
            currentInventoryUnit = null;
        }
        isDrag = false;
    }

    public void SwapUnit(InventoryUnit curUnit, InventoryUnit targetUnit)
    {
        Debug.Log(curUnit + " �� " + targetUnit + " ���� ����");

        if(curUnit.item != null && targetUnit.item == null)
        {
            Debug.Log("if�� ù ��°" + curUnit.item + " �� " + targetUnit.item);
            targetUnit.AddItem(curUnit.item);
            curUnit.RemoveItem();
        }
        else if(curUnit.item != null)
        {
            Debug.Log("if�� �� ��°" + curUnit.item + " �� " + targetUnit.item);
            tempItem = targetUnit.item;
            Debug.Log("if�� �� ��° temp : " + tempItem);
            targetUnit.AddItem(curUnit.item);
            curUnit.AddItem(tempItem);
        }
        else
            Debug.Log("else��");
        //InventoryUIUpdate();
    }
}
