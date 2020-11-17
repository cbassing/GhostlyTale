using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingDarts : MonoBehaviour
{
    public float impactRadius = .2f;
    public int damageAmount = 1;
    public Transform impactPoint;
    public LayerMask damageableLayer;
    public Rigidbody2D rb2d;
    public float dartSpeed = 5;

    private void Start()
    {
        rb2d.AddRelativeForce(transform.up * dartSpeed, ForceMode2D.Impulse);
    }


    void Update()
    {
        transform.Translate(Vector3.up * dartSpeed * Time.deltaTime);
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
