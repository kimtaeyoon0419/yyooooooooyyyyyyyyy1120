using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    [SerializeField]
    int MaxHP = 10;
    [SerializeField]
    int CurHP = 10;
    [SerializeField]
    Slider HpBar;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HpBar.value = (float)CurHP / (float)MaxHP;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bottlePtc")
        {
            CurHP--;
        }
    }
}   
