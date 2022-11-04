using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamageable
{
    [SerializeField] private int hp;
    public int Hp { get { return hp; } set { hp = value; } }

    private Rigidbody2D rb;
    [SerializeField, Range(0f, 10f)] private float viewRadius;

    [SerializeField] private GameObject target;
    [SerializeField] private float speed;
    public float Speed { get { return speed; } }

    [SerializeField] private LayerMask layerMask;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void HitDamage(int damage)
    {
        Hp -= damage;
    }

    public void TakeForce(Vector2 dir, int power)
    {
        rb.AddForce(dir * power , ForceMode2D.Impulse);
    }

    private void Update()
    {
        MonsterView();
        FollowTarget();
    }
    public void MonsterView()
    {
        Collider2D viewTarget = Physics2D.OverlapCircle(transform.position, viewRadius, layerMask);

        if(viewTarget != null)
        {
            target = viewTarget.gameObject;   
        }
        else
        {
            return;
        }
       
    }
    public void FollowTarget()
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime);
        }
        else
        {
            return;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }

}
