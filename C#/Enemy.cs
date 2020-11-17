using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{

    public int health;
    public int damageAmount;
    public float speed;
    public float attackRate;
    public Transform attackPoint = null;
    public float attackRange = .2f;
    public LayerMask isPlayer;
    public bool isBoss;
    public GameObject bossDrop;
    public Animator anim;
    public GameObject deathEffect;
    private float nextAttackTime;

    private void Start()
    {
        //isBoss = GameObject.Find("BossBat");
        bossDrop.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        bossDrop.GetComponent<Collider2D>().enabled = false;

    }



    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            if (isBoss)
            {
                bossDrop.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                bossDrop.GetComponent<Collider2D>().enabled = true;
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
            else
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }

    }

    public void Attack()
    {
        //anim.SetTrigger("Attacking");
        Debug.Log(gameObject.name + " Attacking");

        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, isPlayer);

        if (Time.time >= nextAttackTime)
        {
            foreach (Collider2D obj in hitObjects)
            {
                IDamageable damage = obj.GetComponent<IDamageable>();
                Debug.Log(this.gameObject.name + " has hit: " + obj.name);
                if (damage != null)
                {
                    damage.TakeDamage(damageAmount);

                }

            }
        }
        nextAttackTime = Time.time + 1f / attackRate;
    }



    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
