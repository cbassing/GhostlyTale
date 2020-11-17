using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public float impactRadius = .2f;
    public int damageAmount = 1;
    public Transform impactPoint;
    public LayerMask damageableLayer;
    public Rigidbody2D rb2d;
    public float projectileSpeed = 5;
    public GameObject hitEffect;

    private void Start()
    {
        
        
    }


    void Update()
    {
        transform.Translate(Vector3.right * projectileSpeed * Time.deltaTime);

        DartHit();
    }
    public void DartHit()
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(impactPoint.position, impactRadius, damageableLayer);
        foreach (Collider2D obj in hitObjects)
        {
            Debug.Log(obj.transform.name);
            IDamageable damage = obj.GetComponent<IDamageable>();
            Debug.Log(this.gameObject.name + " has hit: " + obj.name);
            if (damage != null)
            {
                damage.TakeDamage(damageAmount);
            }
            GameObject effect = Instantiate(hitEffect, impactPoint.position, transform.rotation);
            Destroy(this.gameObject);


        }


    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(impactPoint.position, impactRadius);
    }



}
