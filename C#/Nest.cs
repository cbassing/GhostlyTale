using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int health = 50;
    [SerializeField]
    private GameObject wasp;
    [SerializeField]
    private Transform detectionZone;
    [SerializeField]
    private float detectionRadius;
    [SerializeField]
    private LayerMask isPlayer;
    [SerializeField]
    private float attackRate = 5f;
    [SerializeField]
    private KillBox killBox;

    private bool canSpawn;
    private SkeletonController player;

    public float nextAttackTime;
    public Transform[] spawnPoints;
    public int random;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<SkeletonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (killBox.canSpawn)
        {
            SpawnWasps();
            
        }
       
    }

    private void SpawnWasps()
    {
        random = Random.Range(0, spawnPoints.Length);

        if (Time.time >= nextAttackTime)
        {

            Instantiate(wasp, spawnPoints[random].position, transform.rotation);
            nextAttackTime = Time.time + 1f / attackRate;
        }

    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if ( health < 0)
        {
            Destroy(gameObject);
        }
    }

    //private void OnDrawGizmos()
    //{

    //    Gizmos.color = Color.magenta;
    //    Gizmos.DrawWireSphere(detectionZone.position, detectionRadius);

    //}

}
