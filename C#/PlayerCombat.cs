using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //public Animator anim;
    public LayerMask enemyLayer;
    public float attackRate = 2f;
    public Animator anim;
    public GameObject projectile;

    private float nextAttackTime = 0f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private int damageAmount = 20;


    void Update()
    {
        //if(Time.time >= nextAttackTime)
        //{
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //Attack();
                anim.SetTrigger("Attack");
                Debug.Log(gameObject.name + " Attacking");
                nextAttackTime = Time.time + 1f / attackRate;
            }
        //}

    }

    void Attack()
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach(Collider2D obj in hitObjects)
        {
            Debug.Log(this.gameObject.name + " has hit: " + obj.name);
            IDamageable damage = obj.GetComponent<IDamageable>();
            if (damage != null)
            {
                damage.TakeDamage(damageAmount);
            }

        }
    }

    public void Shoot()
    {
        Instantiate(projectile, attackPoint.position, transform.rotation);
    }
    private void OnDrawGizmos()
    {
        // Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(attackPoint.position, attackRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }
}
