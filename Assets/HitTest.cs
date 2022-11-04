using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HitTest : MonoBehaviour
{
    [SerializeField] private GameObject hitPoint;

    public void OnBreakWalls()
    {
        Debug.Log("�Լ� ����");
        Collider2D[] colliders = Physics2D.OverlapBoxAll(hitPoint.transform.position, Vector2.one * 0.5f, 0f);
        if(colliders.Length >0)
        {
            Debug.Log("�浹 ����");
            foreach(var col in colliders)
            {
                FragileWall walls = col.GetComponent<FragileWall>();
                walls?.BreakWall(hitPoint.transform.position);
                Debug.Log("�� �μ��� ����");
            }
        }
    }


}
