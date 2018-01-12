using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicAttack : MonoBehaviour {

    public int TakeDamage;

   private void OnTriggerEnter(Collider Enemy)
    {
        if (Enemy.gameObject.tag == "Enemy")
        {
            EnemyHealth.enemyHealth.TakeDamage(TakeDamage);
            Debug.Log(EnemyHealth.enemyHealth.currentHealth);
        }
    }

    
    

}
