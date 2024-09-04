using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject bloodEffect;
    public Sprite byte0Sprite;
    public Sprite byte1Sprite;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public float health = 1f;

    public float damage = 1f;

    public float speed = 3f;

    private GameObject player; 


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");

        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            sr.sprite = byte0Sprite;
        }
        else
        {
            sr.sprite = byte1Sprite;
        }
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.right = -(player.transform.position - transform.position);
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        
    }

    void DestroySelf()
    {
        GameObject tmp = Instantiate(bloodEffect, gameObject.transform);
        tmp.transform.parent = null;
        Destroy(gameObject);
    }

    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            DestroySelf();
        }


    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}
