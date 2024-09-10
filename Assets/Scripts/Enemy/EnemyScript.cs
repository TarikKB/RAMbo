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

    public AudioClip byteDead;

    public float damage = 1f;

    public float speed = 3f;

    private GameObject player;

    public bool isFinalByte = false;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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
        if (player)
        {
            transform.right = -(player.transform.position - transform.position);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        
        
    }

    void DestroySelf()
    {
        AudioManager.PlaySound(byteDead, transform.position);
        GameObject tmp = Instantiate(bloodEffect, gameObject.transform);
        tmp.transform.parent = null;
        ScoreManager.AddScore(10);
        WaveManager.enemies.Remove(gameObject);
        Destroy(gameObject);
    }

    public void TakeDamage(float damage) {
        anim.SetTrigger("TakeDamage");
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
