using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HitTest : MonoBehaviour
{
    [SerializeField] private GameObject hitPoint;

    public void OnBreakWalls()
    {
        Debug.Log("함수 실행");
        Collider2D[] colliders = Physics2D.OverlapBoxAll(hitPoint.transform.position, Vector2.one, 0f);
        if(colliders.Length >0)
        {
            Debug.Log("충돌 있음");
            foreach(var col in colliders)
            {
                FragileWall walls = col.GetComponent<FragileWall>();
                walls?.BreakWall(hitPoint.transform.position);
                Debug.Log("벽 부수기 실행");
            }
        }
    }

    public void OnAttack()
    {
        Debug.Log("공격 실행");
        Collider2D[] colliders = Physics2D.OverlapBoxAll(hitPoint.transform.position, Vector2.one, 0f);
        if (colliders.Length > 0)
        {
            Debug.Log("충돌 있음");
            foreach (var col in colliders)
            {
                Monster mons = col.GetComponent<Monster>();
                mons?.HitDamage(10);
                mons?.TakeForce((col.transform.position - transform.position).normalized, 10);
                Debug.Log("벽 부수기 실행");
            }
        }
    }


}
