using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.DungeonNum = 1;
        LoadManager.LoadScene("DungeonScene");
    }
}
