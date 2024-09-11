using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;



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
    [Header("Player Health")]
    public int numOfHearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public float health = 6f;
    public UnityEngine.UI.Image[] hearts;
    public AudioClip playerHit;

    public GameObject deathParticles;

    public GameObject deathScreen;
    private bool canTakeDamage = true;

    private Vector2 moveDirection;

    [Header("Dash")]
    private float dashDist;
    public float dashCooldown = 1f;
    public bool canDash = true;

    public GameObject dashParticles;
    // Start is called before the first frame update
    
    
    void Start()
    {
        dashDist = PlayerPrefs.GetInt("dashLevel", 0);
        speed =  6 + PlayerPrefs.GetInt("speedLevel", 0);
        health = 6 + PlayerPrefs.GetInt("hpLevel", 0);
        
        cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animation>();
        UpdateHealth();
        
    }

    void Update()
    {
        ProcessInputs();
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            print("DASHING");
            canDash = false;
            Dash();
            Invoke("ResetDash", dashCooldown);

        }
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

    void Dash() {
        GameObject tmp = Instantiate(dashParticles, transform.position, Quaternion.identity);
        tmp.transform.parent = null;
        if (transform.position.x + moveDirection.x * dashDist > cam.orthographicSize * cam.aspect || transform.position.x + moveDirection.x * dashDist < -(cam.orthographicSize * cam.aspect) || transform.position.y + moveDirection.y * dashDist > cam.orthographicSize || transform.position.y + moveDirection.y * dashDist < -cam.orthographicSize)
        {
            float wallDistX = cam.aspect * cam.orthographicSize - Mathf.Abs(transform.position.x);
            float wallDistY = cam.orthographicSize - Mathf.Abs(transform.position.y);
            if (wallDistX < wallDistY)
            {
                transform.position = new Vector2(transform.position.x + moveDirection.x * wallDistX, transform.position.y + moveDirection.y * dashDist);
                return;
            } else {
                transform.position = new Vector2(transform.position.x + moveDirection.x * dashDist, transform.position.y + moveDirection.y * wallDistY);
                return;
            }
        }
        transform.position = new Vector2(transform.position.x + moveDirection.x * dashDist, transform.position.y + moveDirection.y * dashDist);

    }

    void ResetDash() {
        canDash = true; 
    }

    void ProcessInputs() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move() {
        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }

    void UpdateHealth() {
        if (health >= numOfHearts)
        {
            health = numOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health) {
                hearts[i].sprite = fullHeart;
            } else {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

    }

    public void TakeDamage(float damage)
    {
        if (canTakeDamage)
        {
            AudioManager.PlaySound(playerHit, transform.position);
			canTakeDamage = false;
			anim.Play();
			health -= damage;
			if (health <= 0)
			{
                GameObject tmp = Instantiate(deathParticles, transform.position, Quaternion.identity);
                tmp.transform.parent = null;
                Time.timeScale = 0.5f;
                deathScreen.SetActive(true);
				DestroySelf();
			}
			Invoke("IFrames", 0.5f);
            UpdateHealth();

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
