using UnityEngine;

public class KnightController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public float moveSpeed = 10f;
    public float jumpHeight = 3f;
    private bool isGrounded = true;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        // Lật hướng nhân vật khi di chuyển trái/phải
        if (move > 0) spriteRenderer.flipX = false;
        else if (move < 0) spriteRenderer.flipX = true;

        // Animation chạy
        animator.SetBool("isRunning", move != 0);

        // Animation idle: chỉ bật khi không di chuyển, không nhảy, không tấn công
        bool isIdle = (move == 0 && isGrounded && !animator.GetBool("isAttacking"));
        animator.SetBool("isIdle", isIdle);

        // Nhảy
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            animator.SetBool("isJumping", true);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Sqrt(2 * Mathf.Abs(Physics2D.gravity.y) * jumpHeight));
            isGrounded = false;
        }

        // Tấn công (phím A)
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetBool("isAttacking", true);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("isAttacking", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Khi chạm đất
        if (collision.contacts.Length > 0 && collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }
}
