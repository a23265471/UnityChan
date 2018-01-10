using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    Transform player;
 // PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
 
    //EnemyAttack enemyAttack;
    Animator anim;
    CapsuleCollider capsuleCollider;
    bool Ismoving ;
    UnityEngine.AI.NavMeshAgent nav;
    enum AIStatus {Idle,Run,Hit,Damage}
    /*------------------------*/
    public int seeRange = 3;
    public float attackRange = 0.8f;
    /*------------------------*/

    void Awake()
    {

    player = GameObject.FindGameObjectWithTag("Player").transform;
   //playerHealth = player.GetComponent<PlayerHealth>();
     enemyHealth = GetComponent<EnemyHealth>();
   // enemyAttack = GetComponent<EnemyAttack>();
     nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
     anim = GetComponent<Animator>();
     
    }
   
    void Update (){
        if (enemyHealth.currentHealth > 0 /* && playerHealth.currentHealth > 0 */)
        {
            if (Vector3.Distance(transform.position, player.position) < seeRange && Vector3.Distance(transform.position, player.position) > attackRange)
            {
                Move();
                nav.SetDestination(player.position);

            } 
            else if(Vector3.Distance(transform.position, player.position) <= attackRange|| Vector3.Distance(transform.position, player.position) >= seeRange)
            {
               
                UnMove();
                anim.SetTrigger("Attack");
            }
            
        }
        else
        {
           
            nav.enabled = false;
        }
      
	}
   
   private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, seeRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    public void Move()
    {
        Ismoving = true;
        anim.SetBool("Moving", Ismoving);
    }
    public void UnMove() {
        Ismoving = false;
        anim.SetBool("Moving", Ismoving);
    }

}
