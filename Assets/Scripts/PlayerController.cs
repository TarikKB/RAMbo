using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private Vector2 moveDirection;
    // Start is called before the first frame update
    
    
    void Start()
    {
        cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ProcessInputs();
    }
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

        Move();
        
        
    }

    void ProcessInputs() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move() {
        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }
}
