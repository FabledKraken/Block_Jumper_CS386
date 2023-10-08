using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator anim;         // used for the animations of the player
    private BoxCollider2D coll;    // used for boxcollider2d of player
    private Rigidbody2D rb;        // used to connect to player rigidbody2d
    private SpriteRenderer sprite; // used to render the player sprite

    // Direction of x-axis (movement left and right)
    private float dirX;
    
    // SerializedField allows for Inspector modification of values
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private LayerMask jumpableGround;
    
    // Enum to control animation state {0, 1, 2, 3} 
    private enum MovementState { idle, running, jumping, falling }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement in the X-axis (stops immediately and keeps velocity in the y
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        
        // Uses Unity presets to jump and keeps velocity in the x
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        
        UpdateAnimationState();
    }

    // Updates the animations of the player
    private void UpdateAnimationState()
    {
        MovementState state;
        // If else if block for movement
        // Moving to the right and showing the sprite to show right movement
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        // Moving to the left and showing the sprite to show left movement 
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        // Standing still
        else
        {
            state = MovementState.idle;
        }
        
        // if block to check for jumping and ensuring animation stays while in the air
        // uses .1 instead of 0 as these values are not precise
        if (rb.velocity.y > .01f)
        {
            state = MovementState.jumping;
        }
        // Moving in the downward direction
        else if (rb.velocity.y < -.01f)
        {
            state = MovementState.falling;
        }
        
        // Sets the state of the animation to the type casted state of my enum
        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        // Creates a box that is the same size as the characters box collider then 
        // shifts the box down .1f so that we can check with jumpableGround (the ground terrain)
        // if the player is on the ground in order to reset jump
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f,
            Vector2.down, .1f, jumpableGround);
    }
}
