using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BottleAtk : MonoBehaviour
{
    public float speed;

    public float distance;
    public LayerMask isLayer;
    [SerializeField]
    GameObject bottlePtc;
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right * 7;
        rb.velocity = Vector2.up * 5;
        Invoke("DestroyBottle", 2);
    }
    void Update()
    {
       
        if (transform.rotation.y == 0)
        {
            transform.Translate(transform.right * -1 * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
    }
    public void DestroyBottle()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Monster") { 
            Destroy(gameObject);
            Destroy(Instantiate(bottlePtc,transform.position,Quaternion.identity), 1f);
        }
    }
}
