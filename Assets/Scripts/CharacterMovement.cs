
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    [Header("HorizontalMovement")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    private float horizontalInput = 0;
    [SerializeField] private SpriteRenderer sprite;
    public bool isFreezed = false;

    [Header("Jump")]
    [SerializeField] private float gravityScale = 10;
    [SerializeField] private float fallingGravityScale = 40;
    [SerializeField] private int jumpHeight = 2;
    private float jumpForce;
    private CustomGravity gravityScript;
    private bool isJumping = false;
    private float distToGround;
    [SerializeField] private float coyoteTime;
    private float coyoteTimeCounter;
    [SerializeField] private float ratioSmallJump;



    public static CharacterMovement instance;


    private void Awake()
    {
        gravityScript = gameObject.GetComponent<CustomGravity>();
        distToGround = gameObject.GetComponent<Collider>().bounds.extents.y;

        if (instance != null)
        {
            Debug.Log("Plus d'une instance de CharacterMovement dans la scène !!");
        }

        else
        {
            instance = this;
        }
     
                
    }



    private void Start()
    {
        jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * gravityScript.gravityScale));
    }



    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");


        if (isGrounded())

        {
            coyoteTimeCounter = coyoteTime;

        }

        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeCounter > 0f && !isFreezed)
        {
            isJumping = true;
        }
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0f && !isFreezed)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * ratioSmallJump, rb.velocity.z);
            coyoteTimeCounter = 0f;
        }




    }

    private void FixedUpdate()
    {

        Vector3 direction = new Vector3(horizontalInput, 0f, 0f);
        direction.Normalize();

        if (direction != Vector3.zero)
        {
            sprite.flipX = direction.x < 0.1f;

        }

        if (!isFreezed)

        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
        //transform.Translate(direction * speed * Time.deltaTime);



        if (isJumping)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector2.up * jumpForce, ForceMode.VelocityChange);
            isJumping = false;
        }

        if (rb.velocity.y >= 0.1f)
        {
            gravityScript.gravityScale = gravityScale;
        }
        else if (rb.velocity.y < 0.1f)
        {
            gravityScript.gravityScale = fallingGravityScale;
        }
    }



    private bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

}
