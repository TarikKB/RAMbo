using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    [Header("External GameObjects")]
    private Camera cam;

    [Header("Player Components")]
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    [Header("Player Attributes")]
    public float speed = 5f;
    public float health = 10f;
    // Start is called before the first frame update
    
    
    void Start()
    {
        cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 mousePos = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
        float angleRad = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad; // Offset by 90 degrees

        transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);

        if (mousePos.x < transform.position.x)
        {
            transform.Rotate(180f, 0f, 0f);
        }
        
        
    }
}
