using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_attack : MonoBehaviour {

    public PlayerHealth playerHealth;
    public Animator EnemyAc;
    int attackDamage=2;

    private void OnTriggerEnter(Collider Player)
    {
        if (Player.gameObject.tag == "Player")
        {

            playerHealth.TakeDamage(attackDamage);
        }
    }

    private void Update()
    {
        if (playerHealth.currentHealth > 0)//怪物攻擊
        {
            EnemyAc.SetTrigger("Attack");
           
        }
    }
    
    
    
}
