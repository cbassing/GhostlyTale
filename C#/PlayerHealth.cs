using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int currentHealth;
    public int maxHealth = 100;
    public bool isDead;

    public HealthBar healthBar;


    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }


    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        healthBar.SetHealth(currentHealth);

        GetComponent<SpriteRenderer>().material.color = Color.red;
        Invoke("HurtStatus", .3f);

        if (currentHealth <= 0)
        {
            isDead = true;
        }

    }




    void HurtStatus()
    {
        GetComponent<SpriteRenderer>().material.color = Color.white;
    }
}
