using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {
    public static EnemyHealth enemyHealth;

    public int startingHealth = 100;
    public int currentHealth;
    [Header("Enemy Health")]
    public Image healthBar;
    public Transform healthPos;
    public bool isDamaged;

    Animator anim;
    ParticleSystem hitParticle;
    CapsuleCollider capssuleCollider;
    bool Die;
    

    void Awake()
    {
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        enemyHealth=this;
        isDamaged = false;
    }

    void Update() {
         healthPos.LookAt(Camera.main.transform.position);
    }

    public void TakeDamage(int amount) //怪物被打
    {
        isDamaged = true;

        currentHealth -= amount;
        healthBar.fillAmount = currentHealth * 0.01f;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            anim.SetTrigger("Die");
        }
    }

    IEnumerator stop()
    {
        yield return new WaitForSeconds(0.3f);
        isDamaged = false;
    }


}