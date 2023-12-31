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
    float DashPower;
    [SerializeField]
    float DashingTime;
    [SerializeField]
    float DashingCooldown;



    float x;

    [Header("Skill")]
    [SerializeField]
    Transform SkillPos;
    [SerializeField]
    GameObject lightningPrefab;
    [SerializeField]
    GameObject BottlePrefab;
    [SerializeField]
    float distanceBetweenLightnings;//�������� �Ÿ�
    [SerializeField]
    int numberOfLightnings;//���� ����
    [SerializeField]
    float delayBetweenLightnings; // ������ ����ĥ �ð� ������
    
    public float BottleCoolTime;
    private float NowBottleCoolTime = 0;
    private bool isBottleAtk = false;
    GameObject bottleSpawn;

    private float curtime;

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
    void Update()
    {
        if (isDashing)
        {
            return;
        }
        PlayerMove();
        PlayerJump();
        if (Input.GetKeyDown(KeyCode.D))
        {
            //UseLightningSkill();
        }
        
        if (Input.GetKeyDown(KeyCode.F) && CanDash)
        {
            StartCoroutine(Dash());
        }
        if (!isBottleAtk)
        {
            if (NowBottleCoolTime <= 0)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    Debug.Log("����");
                    bottleSpawn = Instantiate(BottlePrefab, SkillPos.position, transform.rotation);
                    NowBottleCoolTime = BottleCoolTime;
                    isBottleAtk = true; // ����: ���⼭ isBottleAtk�� true�� ����
                    Invoke("bottleDestroy", 2);
                }
            }
        }
        else // ����: else�� �߰�
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("���±� ����ؿ�");
                Destroy(bottleSpawn);
                isBottleAtk = false;
            }
        }

        NowBottleCoolTime -= Time.deltaTime;
        

    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

    }

    void bottleDestroy()
    {
        Destroy(bottleSpawn);
        isBottleAtk = false;
    }

    void PlayerMove()
    {
        x = Input.GetAxis("Horizontal") * Speed;
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            SkillPos.transform.localScale = new Vector3(-1, 0);

        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            SkillPos.transform.localScale = new Vector3(1, 0);
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
    void LightningAtk()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Ground")
        {
            JumpCount = MaxJumpCount;
        }
    }

    private IEnumerator Dash()
    {
        CanDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;

        rb.gravityScale = 0f;

        rb.velocity = new Vector2(transform.localScale.x * DashPower, 0f);
        tr.emitting = true;

        yield return new WaitForSeconds(DashingTime);
        tr.emitting = false;

        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(DashingCooldown);
        CanDash = true;
    }
    //public void UseLightningSkill()
    //{
    //    StartCoroutine(GenerateLightnings());
    //}
    //IEnumerator GenerateLightnings()
    //{
    //    //playerDirection = �÷��̾��� ����
    //    //distanceBetweenLightnings = �������� �Ÿ�
    //    float playerDirection = Mathf.Sign(transform.rotation.y); //�÷��̾��� ������ playerDirection�� �޾Ƽ� ������Ŵ
    //    float startPositionX = SkillPos.position.x;// ������ ���� ��ġ�� �÷��̾��� SkillPos ��ġ�� ������ ������ ������ ȭ�� ��ü�� ġ�ϱ� y�� ���� ����

    //    for (int i = 0; i < numberOfLightnings; i++)
    //    {

    //        GameObject lightningInstance = Instantiate(lightningPrefab, new Vector2(startPositionX, 0), Quaternion.identity);//startPositon�� ���� ��ǥ�� ������ ������

    //        // ������ ������ �Ÿ����� �����ϱ� ���� �̵� �Ÿ� ��� (�÷��̾� ����) * ���� �Ÿ� * ( i + 1) < - �� �κ� ���п� ù��°�� 1�� �ι�°�� 2��� ������ �������� �̵�
    //        Vector2 offset = new Vector2(playerDirection, 0) * distanceBetweenLightnings * (i + 1);
    //        lightningInstance.transform.position += (Vector3)offset; //Vector2 offset�� ���� ���� lightningInstance�� �����༭ ������ �����̰� ��
    //                                                                 //lightningInstance.transform.position�� Unity���� 3D �������� ��ġ�� ��Ÿ���� Vector3 Ÿ���̱� ������ ��������� Vector3�� ��ȯ�ؾ��� 

    //        DestroyLightningAfterTime(lightningInstance, delayBetweenLightnings); // ���� �ð��� ���� �� ������ �ı��ϱ� ���� �Լ� ȣ��


    //        yield return new WaitForSeconds(delayBetweenLightnings);// ������ �ð���ŭ ��ٸ�
    //    }
    //}
    //void DestroyLightningAfterTime(GameObject lightning, float delay)//���� �ı� �Լ�
    //{
    //    // delay �ð��� ���� �� ������ �ı�
    //    Destroy(lightning, delay);
    //}
}
