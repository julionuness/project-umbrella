using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float moveSpeed = 6f;

    [Header("Pulo")]
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private int maxJumps = 2;

    [Header("Som de passos")]
    [SerializeField] private float footstepInterval = 1.30f;

    [Header("Componentes")]
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;
    private bool facingRight = true;
    private int jumpsUsed = 0;
    private float footstepTimer = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (animator == null) animator = GetComponent<Animator>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        bool wasGrounded = isGrounded;
        isGrounded = groundCheck != null &&
                     Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && !wasGrounded)
        {
            jumpsUsed = 0;
        }

        if (Input.GetButtonDown("Jump") && jumpsUsed < maxJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayJump();
            jumpsUsed++;
        }

        if (moveInput > 0 && !facingRight) Flip();
        else if (moveInput < 0 && facingRight) Flip();

        UpdateFootsteps();
        UpdateAnimator();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    private void UpdateFootsteps()
    {
        bool isWalking = isGrounded && Mathf.Abs(moveInput) > 0.1f;

        if (isWalking)
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0f)
            {
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayFootstep();

                footstepTimer = footstepInterval;
            }
        }
        else
        {
            footstepTimer = 0f;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void UpdateAnimator()
    {
        if (animator == null) return;

        animator.SetFloat("speed", Mathf.Abs(moveInput));

        animator.SetBool("jump", !isGrounded);
    }
}