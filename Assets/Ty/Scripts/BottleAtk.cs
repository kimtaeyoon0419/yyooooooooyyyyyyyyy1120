using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BottleAtk : MonoBehaviour
{
    public float speed;

    public float distance;
    public LayerMask isLayer;
    void Start()
    {
        Invoke("DestroyBottle", 2);
    }
    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);
        if(ray.collider != null)
        {
            if(ray.collider.tag == "Monster")
            {
                Debug.Log("ИэСп");
            }
            DestroyBottle();
        }
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
}
