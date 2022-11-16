using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamageable, IForceable
{
    [SerializeField]
    private MonsterData data;
    bool isGenerated = false;
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

    [SerializeField] private float viewRadius;

    [SerializeField] private GameObject target;

    [SerializeField] private LayerMask layerMask;

    [SerializeField]
    private GameObject damageTextPrefab;

    [SerializeField]
    private Transform offsetPosition;

    private new SpriteRenderer renderer;

    private Animator anim;

    private bool isOnceHit;

    private bool isAttack;

    Coroutine tempCo;

    [SerializeField] private int damage;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        var manager = FindObjectOfType<MonsterManager>();
        int dungeonNum = GameManager.Instance.DungeonNum;
        int monsterNum = Random.Range(0, manager.stageData[dungeonNum].stageMonsters.Length);

        data = manager.stageData[dungeonNum].stageMonsters[monsterNum];
    }

    private void Update()
    {
        if (isGenerated == false)
        {
            Init(data);
            return;
        }
        if (isAttack == false)
            MonsterView();
    }
    public void Init(MonsterData monsterData)
    {
        isGenerated = true;
        this.Hp = monsterData.hp;
        this.speed = monsterData.speed;
        this.viewRadius = monsterData.viewRadius;
        this.damage = monsterData.damage;
        this.renderer.sprite = monsterData.sprite;
        if (monsterData.animator != null)
            this.anim.runtimeAnimatorController = monsterData.animator;
        gameObject.name = monsterData.name;
    }
    public virtual void HitDamage(int damage)
    {
        GameObject damageText = Instantiate(damageTextPrefab);
        damageText.transform.position = offsetPosition.position;
        damageText.GetComponent<DamageText>().SetText(damage);
        Hp -= damage;
        isOnceHit = true;
        if (Hp > 0)
        {
            tempCo = StartCoroutine(Blink());
        }
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
            if (anim.runtimeAnimatorController != null)
                anim.SetBool("Walk", true);
        }
        else
        {
            if (isOnceHit)
            {
                FollowTarget();
                if (anim.runtimeAnimatorController != null)
                    anim.SetBool("Walk", true);
            }
            else
            {
                if (anim.runtimeAnimatorController != null)
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
            if (player != null)
            {
                if (isAttack == false)
                    StartCoroutine(AttackRoutine());
                player.HitDamage(damage);
            }
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

    public IEnumerator AttackRoutine()
    {
        isAttack = true;
        yield return new WaitForSeconds(1f);
        isAttack = false;
    }
}
