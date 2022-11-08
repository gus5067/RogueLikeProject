using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadTrigger : MonoBehaviour
{
    [SerializeField] Collider2D headTrigger;
    [SerializeField] LayerMask layerMask;

    int layerNum;
    private void Start()
    {
        layerNum = LayerMask.NameToLayer("Platform");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == layerNum)
            headTrigger.isTrigger = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == layerNum)
            headTrigger.isTrigger = false;
    }

}
