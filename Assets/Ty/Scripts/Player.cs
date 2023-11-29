using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private TrailRenderer tr;


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
    float defaultSpeed;

    [SerializeField]
    float JumpPower;
    [SerializeField]
    int MaxJumpCount = 3;
    [SerializeField]
    int JumpCount;

    [SerializeField]
    bool isDashing;
    [SerializeField]
    bool CanDash = true;
    [SerializeField]
    float DashPower = 24f;
    [SerializeField]
    float DashTime = 0.2f;
    [SerializeField]
    float DashCooldown = 0f;
    

    float x;

    [Header("Skill")]
    [SerializeField]
    Transform SkillPos1;
    [SerializeField]
    Transform SkillPos2;
    [SerializeField]
    Transform SkillPos3;
    [SerializeField]
    Transform SkillPos4;
    [SerializeField]
    Transform SkillPos5;

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
        defaultSpeed = Speed;

    }
    private void Update()
    {
        if (isDashing)
        {
            return;
        }
        PlayerMove();
        PlayerDash();
        PlayerJump();
        if (Input.GetKeyDown(KeyCode.D))
        {
            LightningAtk();
        }
        
        if (Input.GetKeyDown(KeyCode.F) && CanDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        
    }


    void PlayerMove()
    {
        x = Input.GetAxis("Horizontal") * Speed;
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localScale = new Vector2(-1, 1);
            SkillPos1.transform.localScale = new Vector2(-1, 1);
            SkillPos2.transform.localScale = new Vector2(-1, 1);
            SkillPos3.transform.localScale = new Vector2(-1, 1);
            SkillPos4.transform.localScale = new Vector2(-1, 1);
            SkillPos5.transform.localScale = new Vector2(-1, 1);

        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localScale = new Vector2(1, 1);
            SkillPos1.transform.localScale = new Vector2(1, 1);
            SkillPos2.transform.localScale = new Vector2(1, 1);
            SkillPos3.transform.localScale = new Vector2(1, 1);
            SkillPos4.transform.localScale = new Vector2(1, 1);
            SkillPos5.transform.localScale = new Vector2(1, 1);
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
    void PlayerDash()
    {
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Ground")
        {
            JumpCount = MaxJumpCount;
        }
    }
    void LightningAtk()
    {
        Instantiate(Bullet, SkillPos1.transform.position, Quaternion.identity);
        Instantiate(Bullet, SkillPos2.transform.position, Quaternion.identity);
        Instantiate(Bullet, SkillPos3.transform.position, Quaternion.identity);
        Instantiate(Bullet, SkillPos4.transform.position, Quaternion.identity);
        Instantiate(Bullet, SkillPos5.transform.position, Quaternion.identity);
    }
    private IEnumerator Dash()
    {
        CanDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * DashPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(DashTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(DashCooldown);
        CanDash = true;
    }
}
