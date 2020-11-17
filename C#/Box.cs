using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IDamageable
{
    public int health = 10;
    public Animator anim;
    public bool indestructible = true;
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            if (!indestructible)
            {
                anim.SetBool("isBroke", true);
                Destroy(gameObject, 5f);
            }

        }
    }
}
