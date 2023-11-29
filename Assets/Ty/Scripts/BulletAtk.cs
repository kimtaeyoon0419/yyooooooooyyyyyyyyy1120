using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAtk : MonoBehaviour
{
    Rigidbody2D rb;
   

    [SerializeField]
    float BulletSpeed;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    void Update()
    {
        rb.velocity = new Vector2(BulletSpeed,rb.velocity.y);
    }
    
}
