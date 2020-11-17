using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{

    public LayerMask isSkeleton;
    public bool canFollow = false;
    public bool canSpawn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Skeleton"))
        {
            canFollow = true;
            canSpawn = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Skeleton"))
        {
            canFollow = false;
            canSpawn = false;
        }
    }



}
