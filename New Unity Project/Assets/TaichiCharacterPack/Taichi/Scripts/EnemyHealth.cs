using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {
    public int startingHealth = 100;
    public int currentHealth;
    [Header("Enemy Health")]
    public Image healthBar;
    public Transform healthPos;


    Animator anim;
    ParticleSystem hitParticle;
    CapsuleCollider capssuleCollider;
    bool Die;
    

    void Awake()
    {
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
       
    }

    void Update() {
         healthPos.LookAt(Camera.main.transform.position);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBar.fillAmount = currentHealth / startingHealth;
        if (currentHealth == 0)
        {
            anim.SetTrigger("Die");
        }
    }



}