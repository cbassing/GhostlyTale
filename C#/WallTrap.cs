using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrap : MonoBehaviour
{
    public bool isActive = true;
    public Transform[] shootPoints;
    public GameObject dart;
    public float attackRate = 1;
    public float triggerVolume;
    public Transform trapTrigger;
    public LayerMask isObject;

    private float nextAttackTime;

    private void Start()
    {
        
        
    }

    private void Update()
    {
        if (isActive)
        {
            ShootDarts();
        }
    }

    void ShootDarts()
    {

        if (Time.time >= nextAttackTime)
        {
                for (int i = 0; i < shootPoints.Length; i++)
                {
                    Instantiate(dart, shootPoints[i].position, transform.rotation);
                }
            nextAttackTime = Time.time + 1f / attackRate;
        }
        


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            isActive = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            isActive = true;
        }
    }
}
