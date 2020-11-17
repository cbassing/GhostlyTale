using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    EnemyAI enemyAI;
    Rigidbody2D rb;
    public Transform[] patrolPoints;
    public float startWaitTime;
    public float speed;
    private float waitTime;
    //private int randomSpot;
    private int currentPointIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        transform.position = patrolPoints[0].position;
        rb = GetComponentInParent<Rigidbody2D>();
        transform.rotation = patrolPoints[0].rotation;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);

        if (transform.position == patrolPoints[currentPointIndex].position)
        {
                transform.rotation = patrolPoints[currentPointIndex].rotation;

            if (waitTime <= 0)
            {
                //randomSpot = Random.Range(0, patrolPoints.Length);
                if (currentPointIndex + 1 < patrolPoints.Length)
                {
                    currentPointIndex++;
                }
                else
                {
                    currentPointIndex = 0;
                }

                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }



        }
        
    }


}
