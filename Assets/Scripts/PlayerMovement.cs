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
    [SerializeField] private int maxJumps = 2; // 2 = permite o double jump

    [Header("Componentes")]
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;
    private bool facingRight = true;
    private int jumpsUsed = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Pega automaticamente se você não arrastar no Inspector
        if (animator == null) animator = GetComponent<Animator>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // --- Leitura de input ---
        moveInput = Input.GetAxisRaw("Horizontal");

        // --- Checagem de chão ---
        bool wasGrounded = isGrounded;
        isGrounded = groundCheck != null &&
                     Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Zera o contador de pulos assim que toca o chão (permite pular de novo, inclusive o duplo)
        if (isGrounded && !wasGrounded)
        {
            jumpsUsed = 0;
        }

        // --- Pulo (com double jump) ---
        if (Input.GetButtonDown("Jump") && jumpsUsed < maxJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpsUsed++;
        }

        // --- Flip do sprite ---
        if (moveInput > 0 && !facingRight) Flip();
        else if (moveInput < 0 && facingRight) Flip();

        // --- Atualiza Animator ---
        UpdateAnimator();
    }

    void FixedUpdate()
    {
        // Movimento horizontal via física
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
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

        // Parametro "speed" usado nas transições Idle <-> Walk
        animator.SetFloat("speed", Mathf.Abs(moveInput));

        // Parametro "jump" usado nas transições para/de Player_Jump
        // true = esta no ar (pulando ou caindo), false = esta no chao
        animator.SetBool("jump", !isGrounded);
    }
}