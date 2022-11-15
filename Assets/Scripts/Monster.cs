using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour, IDamageable, IForceable
{
    [SerializeField] private int hp;
    public int Hp
    {
        get
        {
            return hp;
        }
        set
        {
            if (value <= 0)
            {
                hp = 0;
                Die();
            }
            else
            {
                hp = value;
            }

        }
    }

    [SerializeField] private float speed;
    public float Speed { get { return speed; } }

    private Rigidbody2D rb;

    [SerializeField, Range(0f, 10f)] private float viewRadius;

    [SerializeField] private GameObject target;

    [SerializeField] private LayerMask layerMask;

    private SpriteRenderer renderer;

    private Animator anim;

    private bool isOnceHit;

    Coroutine tempCo;

    [SerializeField] private int damage;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }
    public virtual void HitDamage(int damage)
    {
        Hp -= damage;
        isOnceHit = true;
        if(Hp > 0)
        {
            tempCo = StartCoroutine(Blink());
        }
       
    }
    private void Update()
    {
        MonsterView();
    }

    IEnumerator Blink()
    {
        WaitForSeconds waitTime = new WaitForSeconds(0.2f);

        renderer.color = new Color(1, 0, 0, 0.5f);

        yield return waitTime;

        renderer.color = new Color(1, 1, 1, 1f);
    }
    public void MonsterView()
    {
        Collider2D viewTarget = Physics2D.OverlapCircle(transform.position, viewRadius, layerMask);

        if (viewTarget != null)
        {
            target = viewTarget.gameObject;
            FollowTarget();
            anim.SetBool("Walk", true);
        }
        else
        {
            if (isOnceHit)
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

    public void TakeForce(Vector2 dir, int power)
    {
        rb.AddForce(dir * power, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player?.HitDamage(damage);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player?.HitDamage(damage);
        }
    }

    public virtual void Die()
    {
        if(tempCo != null)
        {
            StopCoroutine(tempCo);
        }
        gameObject.SetActive(false);
    }

}
