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

    private Animator anim;

    private bool isHit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    public void HitDamage(int damage)
    {
        Hp -= damage;
        isHit = true;
    }

    public void TakeForce(Vector2 dir, int power)
    {
        rb.AddForce(dir * power , ForceMode2D.Impulse);
    }

    private void Update()
    {
        MonsterView();
       
    }
    public void MonsterView()
    {
        Collider2D viewTarget = Physics2D.OverlapCircle(transform.position, viewRadius, layerMask);

        if(viewTarget != null)
        {
            target = viewTarget.gameObject;
            FollowTarget();
            anim.SetBool("Walk", true);
        }
        else
        {
            if(isHit)
            {
                FollowTarget();
                anim.SetBool("Walk", true);
            }
            else
            {
                anim.SetBool("Walk", false);
                return;
            }
           
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
