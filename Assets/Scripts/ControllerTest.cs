using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PlayerMoveType
{
    TopDown, Platform
}
[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class ControllerTest : MonoBehaviour
{

    [SerializeField] Animator anim;
    Rigidbody2D rb;

    [SerializeField] private PlayerMoveType moveType;

    [SerializeField]
    private float speed;
    public float Speed
    {
        get { return speed; }
        set
        {
            speed = value;
        }
    }

    [SerializeField] private int maxVelocity;

    //[SerializeField, Range(1f, 10f)]
    //private float accelSpeed;

    float inputX;
    float inputY;
    float initScaleX;

    [SerializeField] RectTransform hitPoint;

    public event UnityAction<int> onChangeWeapon;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        initScaleX = transform.localScale.x;
    }
    private void Update()
    {
        Move();
        switch (moveType)
        {
            case PlayerMoveType.TopDown:
              
                HitPointMove();

                if (Input.GetKeyDown(KeyCode.K))
                {
                    anim.SetTrigger("AxeAttack");
                    onChangeWeapon?.Invoke(0);
                }
                else if (Input.GetKeyDown(KeyCode.J))
                {
                    anim.SetTrigger("Attack");
                    onChangeWeapon?.Invoke(1);
                }
                break;
            case PlayerMoveType.Platform:
                break;

        }

    }
    public void Move()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        if (inputX > 0)
        {
            transform.localScale = new Vector3(-1f * initScaleX, transform.localScale.y, transform.localScale.z);
        }
        else if (inputX < 0)
        {
            transform.localScale = new Vector3(initScaleX, transform.localScale.y, transform.localScale.z);
        }
        if (inputX != 0 || inputY != 0)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }

        switch(moveType)
        {
            case PlayerMoveType.Platform:
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                }
                if(Mathf.Abs(inputX) > 0)
                {
                    if (Mathf.Abs(rb.velocity.x) < maxVelocity)
                    {
                        rb.AddForce(new Vector2(inputX * Speed * Time.deltaTime, 0));
                        Debug.Log("힘");
                    }
                    else
                    {
                        Debug.Log("최대속도 이동");
                        rb.velocity = new Vector2(maxVelocity * inputX, rb.velocity.y);
                    }
                }
                else
                {
                    break;
                }

                break;
            //if(Mathf.Abs(inputX) != 0)
            //{
            //    Speed += accelSpeed * inputX * Time.deltaTime;
            //    rb.velocity = new Vector2(Speed, rb.velocity.y);
            //}
            //else
            //{
            //    if(rb.velocity.x > 0)
            //    {
            //        Speed -= 2 * accelSpeed * Time.deltaTime;
            //        if(rb.velocity.x < 0)
            //        {
            //            Speed = 0;
            //        }
            //    }
            //    else if(rb.velocity.x < 0)
            //    {
            //        Speed += 2 * accelSpeed * Time.deltaTime;
            //        if (rb.velocity.x > 0)
            //        {
            //            Speed = 0;
            //        }
            //    }

            //}


            case PlayerMoveType.TopDown:
                transform.Translate(new Vector2(Mathf.RoundToInt(inputX), Mathf.RoundToInt(inputY)) * Time.deltaTime * Speed);
                break;
        }
       
    }
    public void HitPointMove()
    {
        if(Mathf.Abs(inputX) > 0 || Mathf.Abs(inputY) > 0)
        {
            hitPoint.anchoredPosition = new Vector2(-0.5f * Mathf.Abs(inputX), 0.3f + inputY * 0.6f);
        }
        else
        {
            hitPoint.anchoredPosition = new Vector2(-0.5f, 0.3f);
        }
    }
}
