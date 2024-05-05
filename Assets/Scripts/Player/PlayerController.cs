using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private float gravity;



    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        gravity = body.gravityScale;
    }
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //Flipping the character
        if (horizontalInput > 0.1f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.1f)
            transform.localScale = new Vector3(-1,1,1);


        

        anim.SetBool("Run", horizontalInput!=0);
        anim.SetBool("Grounded", isGrounded());

        if (wallJumpCooldown > 0.2f)
        {
            

            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                body.velocity = Vector2.zero;
                body.gravityScale = 0;
            }
            else
                body.gravityScale = gravity;
            if (Input.GetKey(KeyCode.Space))
                Jump();
        }
        else
            wallJumpCooldown += Time.deltaTime;

    }

    private void Jump()
    {
        if(isGrounded())
        {

            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("Jump");
        }
        if(onWall()&& !isGrounded())
        {
            wallJumpCooldown = 0;
            if(horizontalInput==0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 6);
                transform.localScale= new Vector3(-Mathf.Sign(transform.localScale.x),transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*3,6);
        }
     }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit2D.collider!=null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit2D.collider != null;
    }

    public bool canAttack()
    {
        return  isGrounded() && !onWall();
    }
}
