using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Skeleton"))
        {
            IDamageable damage = other.GetComponent<IDamageable>();
            Debug.Log(this.gameObject.name + " has hit: " + other.name);
            if (damage != null)
            {
                damage.TakeDamage(enemy.damageAmount);
            }
        }

    }
}
