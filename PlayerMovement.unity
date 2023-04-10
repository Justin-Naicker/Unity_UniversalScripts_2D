
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header("Controllers")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animatorController;

    [Header("Colliders")]
    public CapsuleCollider2D ceilingCollider;
    public CircleCollider2D crouchingCollider;

    [Header("Direction Input")]
    private bool facingRight = true;
    public float dirX;

    [Header("Player Variables")]
    [Range(0, 1000)][SerializeField] private float playerSpeed = 250f;
    [Range(0, 1000)][SerializeField] private float playerJumpVelocity = 75f;
    [Range(0, 1000)][SerializeField] private float crouchSpeed = 0.5f;
    public float jumpAmount = 1;

    [Header("Boolean")]
    /*    private bool isRunning = false;*/
    public bool isDead = false; //Keep public for AudioDeath Script 
    private bool isGrounded = false;
    private bool isJumping = false;
    public bool isCrouching = false;
    private bool spacePressed = false;
    private bool inAir = false;
    private bool powerUpUsed = false;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckObject;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundRadius = 0.3f;

    [Header("Ceiling Check")]
    [SerializeField] private Transform ceilingCheckObject;
    [SerializeField] private Transform middleCheckObject;
    [SerializeField] private float ceilingRadius = 0.3f;


    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public HealthBar healthBarScript;

    public float xSpeed;




    #endregion

    #region Unity Events

    private void Reset()
    {
        FindObjectOfType<GameManager>().deathBackground.SetActive(false);
    }



    void Awake()
    {
        animatorController = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }



    void Update()
    {

        AnimatorController();

        #region InputController

        dirX = Input.GetAxisRaw("Horizontal");

        if (rb.position.y < -10)
        {
            healthBarScript.health = 0;
            healthBarScript.healthBar.fillAmount = healthBarScript.health / 100;
            Die();
        }

        //Jumping Input
        if (isGrounded && !isCrouching && !isJumping)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                spacePressed = true;
                isJumping = true;
                SoundManager.instance.PlaySFX("jump");
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            spacePressed = false;
            isJumping = false;
        }


        //Crouch Input
        if (!isJumping && isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                isCrouching = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            isCrouching = false;
        }


        //Flag Checks
        if (isCrouching && isGrounded)
        {
            isJumping = false;
        }

        if (isJumping || !isGrounded)
        {
            isCrouching = false;
        }


        if (!spacePressed && isGrounded)
        {
            isJumping = false;
        }
        #endregion InputController
    }



    private void FixedUpdate()
    {

        GroundCheck();
        Move(dirX, isCrouching);
        Jump();
    }
    #endregion


    #region Custom Events



    public void GroundCheck()
    {

        bool wasGrounded = isGrounded;
        inAir = !isGrounded;
        isGrounded = true;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckObject.position, groundRadius, groundMask);

        if (colliders.Length > 0)
        {
            isGrounded = true;
            if (!wasGrounded)
            {
                SoundManager.instance.PlaySFX("landing");
            }
        }
        else
        {
            isGrounded = false;
        }
    }


    void Move(float _dirX, bool _isCrouching)
    {
        #region Crouching
        //Ceiling Check
        if (!_isCrouching)
        {
            if (Physics2D.OverlapCircle(ceilingCheckObject.position, ceilingRadius, groundMask))
            {
                isJumping = false;
            }
        }

        if (_isCrouching)
        {
            //Top Collider
            if (Physics2D.OverlapCircle(ceilingCheckObject.position, ceilingRadius, groundMask))
            {
                isJumping = false;
            }
            //Middle Collider
            if (Physics2D.OverlapCircle(middleCheckObject.position, ceilingRadius, groundMask) && isGrounded)
            {
                isJumping = false;
            }
        }

        ceilingCollider.enabled = !_isCrouching;
        crouchingCollider.enabled = _isCrouching;
        #endregion

        #region Movement
        float _xSpeed = _dirX * playerSpeed * Time.fixedDeltaTime;
        xSpeed = _xSpeed;


        if (_isCrouching)
        {
            _xSpeed *= crouchSpeed;
        }

        Vector2 targetVelocity = new Vector2(_xSpeed, rb.velocity.y);
        rb.velocity = targetVelocity;

        #endregion

        #region MovementAnimator
        if (facingRight && _dirX < 0)
        {
            transform.Rotate(0, 180, 0);
            facingRight = false;
        }
        else if (!facingRight && _dirX > 0)
        {
            transform.Rotate(0, 180, 0);
            facingRight = true;
        }
        else
        {
            /*isRunning = false;*/
            animatorController.SetBool("isRunning", false);
        }
        #endregion
    }


    void Jump()
    {
        if (isGrounded && isJumping && !isCrouching)
        {
            if (jumpAmount == 1)
            {
                rb.AddForce(new Vector2(0f, playerJumpVelocity));
                if (inAir)
                {

                    spacePressed = false;
                    isJumping = false;
                }
            }

            //Super Jump
            if (jumpAmount == 2)
            {
                rb.AddForce(new Vector2(0f, playerJumpVelocity * 6f));
                if (inAir)
                {
                    spacePressed = false;
                    isJumping = false;
                    powerUpUsed = false;
                }

                if (powerUpUsed == false)
                {
                    jumpAmount = 1;
                }
            }
        }
    }



    public void Die()
    {
        isDead = true;
        FindObjectOfType<GameManager>().deathBackground.SetActive(true);
        Debug.Log("You died!");
    }

    public void AnimatorController()
    {
        //Running
        if (dirX > 0)
        {
            animatorController.SetBool("isRunning", true);
        }
        else if (dirX < 0)
        {
            animatorController.SetBool("isRunning", true);
        }
        else
        {
            animatorController.SetBool("isRunning", false);
        }

        //Crouching
        if (isCrouching)
        {
            animatorController.SetBool("isCrouching", true);
        }
        else if (!isCrouching)
        {
            animatorController.SetBool("isCrouching", false);
        }

        if (!ceilingCollider.enabled)
        {
            animatorController.SetBool("isCrouching", true);
        }
        else if (ceilingCollider.enabled)
        {
            animatorController.SetBool("isCrouching", false);
        }

        //JumpCheck
        if (inAir)
        {
            animatorController.SetBool("isRunning", false);
            animatorController.SetBool("isJumping", true);
        }

        if (isGrounded)
        {
            animatorController.SetBool("isJumping", false);
        }
    }
}


#endregion
