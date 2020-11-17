using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public bool isActive = true;
    public GameObject platform;
    public Transform moveTowardsDown;
    public Transform moveTowardsUp;
    public Transform platformTrigger;
    public float speed = 3;

    private Vector3 originalPosition;

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            MovePlatformDown();
        }

        if (isActive)
        {
            MovePlatformUp();
        }
        //Debug.Log(isActive);
        
    }

    private void MovePlatformDown()
    {
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, moveTowardsDown.position, speed * Time.deltaTime);
    }

    private void MovePlatformUp()
    {
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, moveTowardsUp.transform.position, speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            isActive = false;
           // MovePlatformDown();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            isActive = true;
            //MovePlatformUp();
        }
    }

}
