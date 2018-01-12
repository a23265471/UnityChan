using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    Transform playerPos;
    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    EnemyMovement enemyMovement;
    bool playerInRange;
    float timer;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos =player.transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyMovement = GetComponent<EnemyMovement>();
        anim = GetComponent<Animator>();
       
    }
    
    void Update()
    {

       if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDie");
        }
    
    }


    void Attack()
    {
        timer = 0f;

       if (playerHealth.currentHealth > 0 )//怪物攻擊
        {
            anim.SetTrigger("Attack");
           
        }
     
    }
}
