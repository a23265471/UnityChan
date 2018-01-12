using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public bool isDead;
    bool damaged;
    private Animator PlayerAC;
	// Use this for initialization
	void Awake () {
        currentHealth = startingHealth;
        PlayerAC = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	public void TakeDamage (int amount) {
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        if (currentHealth <= 0 && !isDead) {
            Death();
        }
	}
    private void Death() {
        isDead = true;
        PlayerAC.SetTrigger("Die");
        GameManager.Game_manager.GameOver();
    }
}
