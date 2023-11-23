using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;

    [Header("Player Stats")]
    [SerializeField]
    float MaxHp;
    [SerializeField]
    float NowHp;

    [Space(10f)]
    [SerializeField]
    float AtkDmg;
    [SerializeField]
    float AtkSpeed = 1;
    [SerializeField]
    bool Attacked = false;
    [SerializeField]
    GameObject AttackFose;

    [Space(10f)]
    [SerializeField]
    float Speed;
    [SerializeField]
    float JumpPower;
    [SerializeField]
    int MaxJumpCount = 3;
    [SerializeField]
    int JumpCount;


    float x;

    [Header("Bullet")]
    [SerializeField]
    Transform BulletPos;
    [SerializeField]
    GameObject Bullet;




    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        MaxHp = 50;
        NowHp = 50;
        AtkDmg = 10;

    }
    private void Update()
    {
        PlayerJump();
        if (Input.GetKeyDown(KeyCode.D))
        {
            BulletAtk();
        }
    }

    void FixedUpdate()
    {
        PlayerMove();

    }

    void PlayerMove()
    {
        x = Input.GetAxis("Horizontal") * Speed;
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localScale = new Vector2(-1, 1);  
            
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        rb.velocity = new Vector2(x, rb.velocity.y);

    }
    void PlayerJump()
    {
        if (JumpCount != 0)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                JumpCount--;
                rb.velocity = Vector2.up * JumpPower;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Ground")
        {
            JumpCount = MaxJumpCount;
        }
    }
    void BulletAtk()
    {
        Instantiate(Bullet, BulletPos.transform.position, Quaternion.identity);
    }
}
