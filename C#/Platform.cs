using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    private PlatformEffector2D oneWay;
    private SkeletonController player;

    // Start is called before the first frame update
    void Start()
    {
        oneWay = GetComponent<PlatformEffector2D>();
        player = FindObjectOfType<SkeletonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            player.jumpForce = 0;

            if (player.jumpForce == 0)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                oneWay.surfaceArc = 0;
            }
        }
        else
        {
            oneWay.surfaceArc = 180;
            player.jumpForce = player.originalJumpForce;
        }
    }
}
