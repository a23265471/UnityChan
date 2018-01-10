using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    Transform playerPos;
    Animator anim;
    GameObject player;
   // PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    EnemyMovement enemyMovement;
    bool playerInRange;
    float timer;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos =player.transform;
     // playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyMovement = GetComponent<EnemyMovement>();
        anim = GetComponent<Animator>();
       
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player )
        {
            playerInRange = true;
            
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player )
        {
            playerInRange = false;
        }
    }


    void Update()
    {

        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0 && Vector3.Distance(transform.position, playerPos.position) <= enemyMovement.attackRange)
        {
            Attack();
        }

    /*    if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDie");
        }
     */
    }


    void Attack()
    {
        timer = 0f;

     /*   if (playerHealth.currentHealth > 0 )
        {
            anim.SetTrigger("Attack");
            playerHealth.TakeDamage(attackDamage);
        }
      */
    }
}
