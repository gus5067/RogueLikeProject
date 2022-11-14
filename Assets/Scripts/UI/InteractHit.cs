using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractHit : MonoBehaviour
{
    private Player player;
    [SerializeField]
    int targetNum;
    private void Awake()
    {
        player = GetComponentInParent<Player>();
        targetNum = LayerMask.NameToLayer("Interact");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == targetNum)
        {
            player.curTarget = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == targetNum)
        {
            player.curTarget = null;
        }
    }
}
