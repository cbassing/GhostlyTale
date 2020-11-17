using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public int damageAmount = 1000;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Skeleton"))
        {
            IDamageable damage = other.GetComponent<IDamageable>();
            if (damage != null)
            {
                damage.TakeDamage(damageAmount);
            }

        }

    }
}

