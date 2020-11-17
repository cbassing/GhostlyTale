using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    public float speed = 3f;
    public float pushSpeed = 4f;
    private float originalSpeed;
    private bool isGrounded;
    private bool isPushing;
    public float jumpForce = 3f;
    public float originalJumpForce;
    public float checkRadius = .2f;
    public Transform groundCheck;
    public Transform frontCheck;
    public LayerMask whatIsGround;
    public int extraJumpValue;
    public Animator[] anim;
    public int CurrentSkeletonID;

    private EmptySkeleton empty;
    private GameObject platform;
    private bool facingRight = true;
    private bool onPlatform = false;
    private int extraJumps;

    public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        originalSpeed = speed;
        originalJumpForce = jumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            
        }

        if (empty != null)
        {
            CurrentSkeletonID = empty.skeletonID;
        }

    }

    public void Movement()
    {
        
        float horizontalMove = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
        isPushing = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);
        anim[CurrentSkeletonID].SetFloat("isRunning", Mathf.Abs(horizontalMove));
        if (horizontalMove > 0 && facingRight == false)
        {
            Flip();
        }
        else if (horizontalMove < 0 && facingRight == true)
        {
            Flip();
        }

        if (isPushing)
        {
            speed = pushSpeed;
        }
        else
        {
            speed = originalSpeed;
        }




    }

    void Flip()
    {
        rb.transform.Rotate(0, 180f, 0);
        facingRight = !facingRight;
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (isGrounded)
        {
            extraJumps = extraJumpValue;
        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {

            anim[CurrentSkeletonID].SetTrigger("Jump");
            rb.velocity = Vector2.up * (jumpForce * .5f);
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
            anim[CurrentSkeletonID].SetTrigger("Jump");
        }


    }


    private void OnDrawGizmos()
    {

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(frontCheck.position, checkRadius);

    }
}
