using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RocketScript : MonoBehaviour
{

    private Rigidbody2D rb;
    public float speed = 5f;

    public float damage = 1f;

    public float lifetime = 1f;

    public GameObject explosion;

    public AudioClip explosionSound;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Invoke("DestroySelf", lifetime);
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.5f);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.CompareTag("Enemy"))
                {
                    colliders[i].gameObject.GetComponent<EnemyScript>().TakeDamage(damage);
                    colliders[i].gameObject.GetComponent<Rigidbody2D>().AddForce((colliders[i].transform.position - transform.position).normalized * 5f, ForceMode2D.Impulse);

                }
            }
            DestroySelf();
        }
    }

    void DestroySelf()
    {
        AudioManager.PlaySound(explosionSound, transform.position);
        Camera.main.GetComponent<CameraShake>().StartShake(0.2f, 0.2f);
        GameObject explosionEffect = Instantiate(explosion, gameObject.transform);
        explosionEffect.transform.parent = null;
        Destroy(gameObject);
    }
}
