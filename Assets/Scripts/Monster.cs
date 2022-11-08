using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamageable, IForceable
{
    [SerializeField] private int hp;
    public int Hp { get { return hp; } set { hp = value; } }

    private Rigidbody2D rb;
    [SerializeField, Range(0f, 10f)] private float viewRadius;

    [SerializeField] private GameObject target;
    [SerializeField] private float speed;
    public float Speed { get { return speed; } }

    [SerializeField] private LayerMask layerMask;

    private SpriteRenderer[] renderers;
    private Animator anim;

    private bool isOnceHit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
    }
    public void HitDamage(int damage)
    {
        Hp -= damage;
        isOnceHit = true;
        StartCoroutine(Blink());

    }
    private void Update()
    {
        MonsterView();
    }
    IEnumerator Blink()
    {
        WaitForSeconds waitTime = new WaitForSeconds(0.2f);

        for(int i =0; i < renderers.Length; i++)
        {
            renderers[i].color = new Color(1, 0, 0, 0.5f); 
        }
        yield return waitTime;
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].color = new Color(1, 1, 1, 1f);
        }
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
            if(isOnceHit)
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

    public void TakeFoce(Vector2 dir, int power)
    {
        rb.AddForce(dir * power, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player?.HitDamage(5);
        }
    }
}
