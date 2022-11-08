using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HitTest : MonoBehaviour
{
    [SerializeField] private GameObject hitPoint;
    [SerializeField] private Vector2 hitArea;

    public void OnBreakWalls()

    {
        Collider2D col = Physics2D.OverlapBox(hitPoint.transform.position, hitArea, 0f);

        FragileWall walls = col.GetComponent<FragileWall>();
        walls?.BreakWall(hitPoint.transform.position);


    }

    public void OnAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(hitPoint.transform.position, hitArea, 0f);
        if (colliders.Length > 0)
        {
            foreach (var col in colliders)
            {
                Monster mons = col.GetComponent<Monster>();
                mons?.HitDamage(10);
                mons?.TakeFoce((col.transform.position - transform.position).normalized, 10);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(hitPoint.transform.position, hitArea);
    }
}
