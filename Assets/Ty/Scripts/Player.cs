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
    float distanceBetweenLightnings;//번개들의 거리
    [SerializeField]
    int numberOfLightnings;//번개 갯수
    [SerializeField]
    float delayBetweenLightnings; // 번개가 내리칠 시간 딜레이
    
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
                    Debug.Log("술통");
                    bottleSpawn = Instantiate(BottlePrefab, SkillPos.position, transform.rotation);
                    NowBottleCoolTime = BottleCoolTime;
                    isBottleAtk = true; // 수정: 여기서 isBottleAtk를 true로 설정
                    Invoke("bottleDestroy", 2);
                }
            }
        }
        else // 수정: else로 추가
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("말승균 사랑해요");
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
    //    //playerDirection = 플레이어의 방향
    //    //distanceBetweenLightnings = 번개들의 거리
    //    float playerDirection = Mathf.Sign(transform.rotation.y); //플레이어의 방향을 playerDirection에 받아서 고정시킴
    //    float startPositionX = SkillPos.position.x;// 번개의 시작 위치를 플레이어의 SkillPos 위치로 고정함 하지만 번개는 화면 전체에 치니까 y은 받지 않음

    //    for (int i = 0; i < numberOfLightnings; i++)
    //    {

    //        GameObject lightningInstance = Instantiate(lightningPrefab, new Vector2(startPositionX, 0), Quaternion.identity);//startPositon에 받은 좌표에 번개를 생성함

    //        // 번개를 일정한 거리마다 생성하기 위해 이동 거리 계산 (플레이어 방향) * 번개 거리 * ( i + 1) < - 이 부분 덕분에 첫번째는 1배 두번째는 2배로 일정한 간격으로 이동
    //        Vector2 offset = new Vector2(playerDirection, 0) * distanceBetweenLightnings * (i + 1);
    //        lightningInstance.transform.position += (Vector3)offset; //Vector2 offset에 구한 값을 lightningInstance에 더해줘서 실제로 움직이게 함
    //                                                                 //lightningInstance.transform.position은 Unity에서 3D 공간상의 위치를 나타내는 Vector3 타입이기 때문에 명시적으로 Vector3를 변환해야함 

    //        DestroyLightningAfterTime(lightningInstance, delayBetweenLightnings); // 일정 시간이 지난 후 번개를 파괴하기 위한 함수 호출


    //        yield return new WaitForSeconds(delayBetweenLightnings);// 일정한 시간만큼 기다림
    //    }
    //}
    //void DestroyLightningAfterTime(GameObject lightning, float delay)//번개 파괴 함수
    //{
    //    // delay 시간이 지난 후 번개를 파괴
    //    Destroy(lightning, delay);
    //}
}
