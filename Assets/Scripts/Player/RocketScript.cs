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
            DestroySelf();
        }
    }

    void DestroySelf()
    {
        GameObject explosionEffect = Instantiate(explosion, gameObject.transform);
        explosionEffect.transform.parent = null;
        Destroy(gameObject);
    }
}
