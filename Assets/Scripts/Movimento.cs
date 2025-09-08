using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D corpo;
    private Animator playerAnim;

    [Header("Movimento")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float acel = 15f;
    [SerializeField] private float desacel = 20f;
    private float moveInput;

    [Header("Pulo")]
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float jumpCut = 0.5f;
    [SerializeField] private float coyoteTime = 0.1f;
    [SerializeField] private float jumpBufferTime = 0.1f;

    [Header("Deslizamento")]
    private bool isClimbing;
    [SerializeField] private float wallSlideSpeed = 2f;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance = 0.5f;

    private bool touchingWall;
    private bool isWallSliding;

    private float coyoteCounter;
    private float jumpBufferCounter;
    private bool noChao;

    void Start()
    {
        corpo = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        // Coyote Time
        coyoteCounter = noChao ? coyoteTime : coyoteCounter - Time.deltaTime;

        // Jump Buffer
        jumpBufferCounter = Input.GetKeyDown(KeyCode.Space) ? jumpBufferTime : jumpBufferCounter - Time.deltaTime;

        // Pular com buffer e coyote time
        if (jumpBufferCounter > 0 && coyoteCounter > 0)
        {
            Jump();
            jumpBufferCounter = 0;
        }

        // Pulo variável (jump cut)
        if (Input.GetKeyUp(KeyCode.Space) && corpo.linearVelocity.y > 0)
        {
            corpo.linearVelocity = new Vector2(corpo.linearVelocity.x, corpo.linearVelocity.y * jumpCut);
        }

        // Virar sprite
        if (moveInput > 0.01f) transform.localScale = Vector3.one;
        else if (moveInput < -0.01f) transform.localScale = new Vector3(-1, 1, 1);

        // Checagem de parede
        touchingWall = Physics2D.Raycast(wallCheck.position, new Vector2(transform.localScale.x, 0), wallCheckDistance, LayerMask.GetMask("Ground"));

        // Determinar se está deslizando na parede
        if (touchingWall && !noChao && !isClimbing)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        // Climbing logic: only allow if touching wall and pressing Z
        if (touchingWall && Input.GetKeyDown(KeyCode.Z) && !noChao)
        {
            isClimbing = true;
        }
        else if (!Input.GetKey(KeyCode.Z))
        {
            isClimbing = false;
        }

        // Animações
        playerAnim.SetFloat("xVelocity", Mathf.Abs(corpo.linearVelocity.x));
        playerAnim.SetFloat("yVelocity", corpo.linearVelocity.y);
        playerAnim.SetBool("Pulando", !noChao);
        playerAnim.SetBool("IsClimbing", isClimbing);
        playerAnim.SetBool("WallSlide", isWallSliding);
    }

    void FixedUpdate()
    {
        // Movimento horizontal suave
        float targetSpeed = moveInput * moveSpeed;
        float speedDif = targetSpeed - corpo.linearVelocity.x;
        float acelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acel : desacel;
        float movement = speedDif * acelRate * Time.fixedDeltaTime;
        corpo.linearVelocity = new Vector2(corpo.linearVelocity.x + movement, corpo.linearVelocity.y);

        // Wall slide: apply vertical velocity only if sliding
        if (isWallSliding)
        {
            corpo.linearVelocity = new Vector2(corpo.linearVelocity.x, -wallSlideSpeed);
        }
    }

    private void Jump()
    {
        corpo.linearVelocity = new Vector2(corpo.linearVelocity.x, jumpForce);
        coyoteCounter = 0;
        noChao = false;
        playerAnim.SetBool("Pulando", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            noChao = true;
            playerAnim.SetBool("Pulando", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            noChao = false;
        }
    }
}