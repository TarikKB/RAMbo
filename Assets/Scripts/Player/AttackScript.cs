using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackScript : MonoBehaviour
{

    public float damage = 1f;
    public float cooldown = 1f;

    public float projectileSpeed = 5f;

    private bool canAttack = true;

    public GameObject projectile;

    private RocketScript projectileScript;

    public Transform attackPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Attack() {
        projectileScript = Instantiate(projectile, attackPoint.position, attackPoint.rotation).GetComponent<RocketScript>();
        projectileScript.speed = projectileSpeed;
        projectileScript.damage = damage;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            Attack();
            canAttack = false;
            Invoke("ResetAttack", cooldown);
        }
        
    }
    
    void ResetAttack()
    {
        canAttack = true;
    }
}
