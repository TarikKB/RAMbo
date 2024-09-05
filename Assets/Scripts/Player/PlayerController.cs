using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    [Header("External GameObjects")]
    private Camera cam;
    public GameObject rocketLauncher;

    [Header("Player Components")]
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animation anim;

    [Header("Player Attributes")]
    public float speed = 5f;
    public float health = 10f;

    private bool canTakeDamage = true;

    private Vector2 moveDirection;
    // Start is called before the first frame update
    
    
    void Start()
    {
        cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animation>();
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
            rocketLauncher.transform.position = new Vector3(rocketLauncher.transform.position.x, rocketLauncher.transform.position.y, 1f);

        } else {
            rocketLauncher.transform.position = new Vector3(rocketLauncher.transform.position.x, rocketLauncher.transform.position.y, 1f);
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

    public void TakeDamage(float damage)
    {
        if (canTakeDamage)
        {
			canTakeDamage = false;
			anim.Play();
			health -= damage;
			if (health <= 0)
			{

				DestroySelf();
			}
			Invoke("IFrames", 0.5f);

		}
        
    }

    private void IFrames()
    {
        canTakeDamage = true;
    }

    void DestroySelf() {
        Destroy(gameObject);
    }
}
