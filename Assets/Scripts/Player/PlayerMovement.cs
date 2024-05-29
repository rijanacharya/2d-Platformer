
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;

    [Header("Movement")]    
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;

    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    [Header("Wall Jump")]
    [SerializeField] private float wallJumpX;
    [SerializeField] private float wallJumpY;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;

    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInPut;

   
    private void Awake()
    {
        //getting refrence for rigidbody and animator 
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInPut = Input.GetAxis("Horizontal");

        // flipping character when pressing left and right
        if (horizontalInPut > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInPut < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // set animator parameter
        anim.SetBool("run", horizontalInPut != 0);
        anim.SetBool("grounded", isGrounded());


        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        // Adjustable jump height
        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
            body.velocity = new Vector2(body.velocity.x, body.velocity.y * 0.5f);

        // Wall jump 
        if(onWall())
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 5;
            body.velocity = new Vector2(horizontalInPut * speed, body.velocity.y);
            if(isGrounded())
            {
                coyoteCounter = coyoteTime;  // reset coyote counter when in ground
                jumpCounter = extraJumps; // reset jump counter when in ground
            }
            else
            {
                coyoteCounter -= Time.deltaTime; // decrease coyote counter when in air
            }
        }

    }

    private void Jump()
    {
        if (coyoteCounter < 0 && !onWall() && jumpCounter <= 0)
            return;
        SoundManager.instance.PlaySound(jumpSound);
        if (onWall())
            WallJump();
        else
        {
            if(isGrounded())
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            else
            {
                if (coyoteCounter > 0)
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                else if (jumpCounter > 0)
                {
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                    jumpCounter--;
                }
            } 
                //reset coyote counter when jump
                coyoteCounter = 0;
        }
    }


    private void WallJump()
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        wallJumpCooldown = 0;
    }
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }


    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInPut == 0 && isGrounded() && !onWall();
    }

}


