using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;

    [SerializeField]
    float MaxHp;
    [SerializeField]
    float NowHp;

    [SerializeField]
    float AtkDmg;
    [SerializeField]
    float AtkSpeed = 1;
    [SerializeField]
    bool Attacked = false;


    [SerializeField] 
    float Speed;
    [SerializeField] 
    float JumpPower;
    [SerializeField] 
    int MaxJumpCount = 3;
    [SerializeField] 
    int JumpCount;

    float x;

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
        if(Input.GetKeyDown(KeyCode.D)) { 
            BulletAtk();
        }
    }

    void FixedUpdate()
    {
        PlayerMove();

    }

    void PlayerMove()
    {
        x = Input.GetAxis("Horizontal");
        transform.position += new Vector3(x, 0, 0) * Speed * Time.deltaTime;
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
