using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        if (other.CompareTag("Skeleton"))
        {
            DoPickup(other.gameObject);
        }
       
    }

    protected virtual void DoPickup(GameObject go)
    {
        Destroy(gameObject);
    }
}
