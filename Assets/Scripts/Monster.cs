using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Moster_AttackType
{
    Normal, Fire, Ice
}
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

    private bool isDie;

    private AudioSource au;

    Coroutine tempCo;

    [SerializeField] private int damage;

    private Moster_AttackType attack_type;
    private void Awake()
    {
        au = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        var manager = FindObjectOfType<MonsterManager>();
        int dungeonNum = GameManager.Instance.DungeonNum;
        int monsterNum = Random.Range(0, manager.stageData[dungeonNum].stageMonsters.Length);
        if(data == null)
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
        this.au.clip = monsterData.audioClip;
        au.Play();
        this.Hp = monsterData.hp;
        this.speed = monsterData.speed;
        this.viewRadius = monsterData.viewRadius;
        this.damage = monsterData.damage;
        this.renderer.sprite = monsterData.sprite;
        this.attack_type = monsterData.attackType;
        if (monsterData.animator != null)
            this.anim.runtimeAnimatorController = monsterData.animator;
        gameObject.name = monsterData.name;
    }
    public virtual void HitDamage(int damage)
    {
        if(!isDie)
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
    }
    IEnumerator Blink()
    {
        WaitForSeconds waitTime = new(0.2f);

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
        if (target != null && !isDie)
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
        if (!isDie)
            rb.AddForce(dir * power, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (isDie)
                return;
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                if (isAttack == true)
                    return;
                if (isAttack == false)
                    StartCoroutine(AttackRoutine());
                MonsterAttack(player);
            }
        }
    }

    public void MonsterAttack(Player player)
    {
        switch (attack_type)
        {
            case Moster_AttackType.Normal:
                player.HitDamage(damage);
                break;
            case Moster_AttackType.Fire:
                player.HitDamage(damage);
                player.PlayerState = PlayerState.Burn;
                break;
            case Moster_AttackType.Ice:
                player.HitDamage(damage);
                player.PlayerState = PlayerState.Frozen;
                break;
        }
    }
    public virtual void Die()
    {
        isDie = true;
        GameManager.Instance.Money += (Random.Range(0, 200) * GameManager.Instance.DungeonNum);
        GetComponent<Collider2D>().isTrigger = true;
        if (tempCo != null)
        {
            StopCoroutine(tempCo);
        }
        StartCoroutine(DieRoutine());
    }

    public IEnumerator AttackRoutine()
    {
        isAttack = true;
        yield return new WaitForSeconds(1f);
        isAttack = false;
    }

    public IEnumerator DieRoutine()
    {
        float color = 1;
        while (color > 0f)
        {
            renderer.color = new Color(1, 1, 1, color);
            color -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        gameObject.SetActive(false);
       
    }
}
