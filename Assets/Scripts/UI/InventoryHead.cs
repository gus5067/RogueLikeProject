using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryHead : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    Vector3 offset;
    Vector2 mouseOffset;
    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - transform.parent.position;
        mouseOffset = (Vector2)transform.position - eventData.position;
        Debug.Log("begin");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + mouseOffset;
        transform.parent.position = transform.position - offset;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("drop");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("end");
    }
}
