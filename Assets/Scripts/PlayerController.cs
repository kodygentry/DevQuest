using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // OBJECTS
    public Rigidbody2D rb;
    public Animator animator;

    // MOVEMENT
    public float moveSpeed;
    Vector2 movement;
    Vector3 moveDir;
    bool canMove = true;
    private bool facingRight = true;

    // DASHING
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [SerializeField] private TrailRenderer tr;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        this.moveDir = new Vector3(movement.x, movement.y).normalized;

        if (canMove && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartCoroutine(Dash());
        } 
        
        mouseFollow();
    }

    void FixedUpdate()
    {
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    public bool FacingRight { get { return facingRight; } }

    void mouseFollow()
    {
        // Get the mouse position in screen coordinates
        Vector2 screenPosition = Mouse.current.position.ReadValue();

        // Convert screen position to world coordinates at the player's z-plane
        // The z-value is adjusted to project the mouse position correctly onto the player's plane
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, Camera.main.nearClipPlane - Camera.main.transform.position.z));

        mouseWorldPosition.z = 0f; 

        // Check if mouse is to the left or right of the player
        bool isMouseOnTheLeft = mouseWorldPosition.x < transform.position.x;

        if ((isMouseOnTheLeft && facingRight) || (!isMouseOnTheLeft && !facingRight))
        {
            // Flip the sprite
            facingRight = !facingRight;
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        }
    }

    
    private IEnumerator Dash()
    {
        if (canDash && movement.sqrMagnitude > 0) // Only dash if there's input movement
        {
            canDash = false;
            isDashing = true;
            float originalGravity = rb.gravityScale;

            // Use the current movement direction for the dash direction
            Vector2 dashDirection = movement.normalized;

            rb.gravityScale = 0f;

            // Apply dash velocity in the direction of movement
            rb.velocity = dashDirection * dashingPower;

            tr.emitting = true;
            yield return new WaitForSeconds(dashingTime);

            tr.emitting = false;

            // Reset the rigidbody's velocity to stop the dash movement
            rb.velocity = Vector2.zero;

            rb.gravityScale = originalGravity;
            isDashing = false;
            yield return new WaitForSeconds(dashingCooldown);
            canDash = true;
        }
    }
}