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

    [SerializeField] private int jumpPower;

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
    float inputX;
    float inputY;
    float initScaleX;

    [SerializeField] RectTransform hitPoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        initScaleX = transform.localScale.x;
    }

    private void FixedUpdate()
    {
        Move();
        HitPointMove();
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
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                }
                if(Mathf.Abs(inputX) > 0)
                {
                    if (Mathf.Abs(rb.velocity.x) < maxVelocity)
                    {
                        rb.AddForce(new Vector2(inputX * Speed * Time.deltaTime, 0));
                    }
                    else
                    {
                        rb.velocity = new Vector2(maxVelocity * inputX, rb.velocity.y);
                    }
                }
                else
                {
                    break;
                }

                break;
            case PlayerMoveType.TopDown:
                transform.Translate(new Vector2(Mathf.RoundToInt(inputX), Mathf.RoundToInt(inputY)) * Time.deltaTime * Speed);
                break;
        }
       
    }
    public void HitPointMove()
    {
        switch(moveType)
        {
            case PlayerMoveType.TopDown:
                 if (Mathf.Abs(inputX) > 0 || Mathf.Abs(inputY) > 0)
                {
                    hitPoint.anchoredPosition = new Vector2(-0.5f * Mathf.Abs(inputX), 0.3f + inputY * 0.6f);
                }
                else
                {
                    hitPoint.anchoredPosition = new Vector2(-0.5f, 0.3f);
                }
                break;
            case PlayerMoveType.Platform:
                if (Mathf.Abs(inputX) > 0)
                {
                    hitPoint.anchoredPosition = new Vector2(-0.5f * Mathf.Abs(inputX), 0.3f);
                }
                else
                {
                    hitPoint.anchoredPosition = new Vector2(-0.5f, 0.3f);
                }
                break;
        }
       
    }
}
