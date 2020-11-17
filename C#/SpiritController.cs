using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritController : MonoBehaviour
{

    public float speed = 5;
    public Rigidbody2D rb2d;

    
    // Start is called before the first frame update
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        rb2d.velocity = new Vector2(horizontalMove * speed, rb2d.velocity.y);
        rb2d.velocity = new Vector2(rb2d.velocity.x, verticalMove * speed);


    }




}
