using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProximity : MonoBehaviour
{

    public Transform followPoint;
    public float followRadius;
    public EnemyAI enemyAI;
    public EnemyPatrol patrol;
    public LayerMask inRange;


    bool canFollow;

    Collider2D col2d;

    // Start is called before the first frame update
    void Start()
    {
        col2d = GetComponent<Collider2D>();
        enemyAI.enabled = false;
        patrol.enabled = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ShouldFollow();
    }

    void ShouldFollow()
    {
        bool canFollow = Physics2D.OverlapCircle(followPoint.position, followRadius, inRange);


        if (!canFollow)
        {
            enemyAI.enabled = false;
            patrol.enabled = true;
            return;
        }
        else
        {
            //StartCoroutine("continueFollowing");
            patrol.enabled = false;
            enemyAI.enabled = true;
        }



    }



    private void OnDrawGizmos()
    {
        // Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(attackPoint.position, attackRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(followPoint.position, followRadius);

    }

}
