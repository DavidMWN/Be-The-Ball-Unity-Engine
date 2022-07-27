using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defines the state of the player when multi-jump object is collected.
[CreateAssetMenu(menuName = "States/Player/MultiJump")]
public class PlayerMultiJumpState : PlayerBaseState<PlayerCtrl>
{
    private Rigidbody2D rbody;
    private CircleCollider2D coll;

    // Movement and jump vales. Assigned in Unity Inspector.
    // Both values should match Normal State values.
    public float speed;
    public float jumpForce;

    private bool canJump;
    private float horizontal;

    private LayerMask jumpableGround;
    private AudioSource jumpSoundEffect;
        
    // Counts how many times the player has jumped.
    private int timesJumped;
    // Limit of how many times the player can jump.
    private int jumpLimit = 3;

    // Called upon entering the state. Retrieves needed values from State Runner.
    public override void EnterState(PlayerCtrl player)
    {        
        base.EnterState(player);
        if (rbody == null)
        {
            rbody = player.GetComponent<Rigidbody2D>();
        }
        if (coll == null)
        {
            coll = player.GetComponent<CircleCollider2D>();
        }
        jumpableGround = player.jumpableGround;
        jumpSoundEffect = player.jumpSoundEffect;

        // Sets timesJumped to 1 to ensure player does not get a bonus jump when multi-jump object is collected in mid-air.
        timesJumped = 1;
    }

    // Gets left/right movement.
    public override void CaptureInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    // Checks if jump button is pressed.
    public override void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            // If player is on ground, allows jump and sets jump count to 0.
            if (IsGrounded())
            {
                canJump = true;
                timesJumped = 0;
            }

            // Allows mid-air jumps if jump count is below limit.
            if (timesJumped < jumpLimit)
            {
                canJump = true;
            }
        }
    }

    // Player movement.
    public override void FixedUpdate()
    {
        rbody.velocity = new Vector2(horizontal * speed, rbody.velocity.y);

        if (canJump)
        {
            jumpSoundEffect.Play();
            rbody.velocity = new Vector2(rbody.velocity.x, jumpForce);

            // Adds to jump count.
            timesJumped++;

            // Prevents additional jumps, gets set to true in Update function if jump limit not reached.
            canJump = false;
        }
    }

    // Checks if player is in contact with proper ground collider.
    private bool IsGrounded()
    {
        return Physics2D.CircleCast(coll.bounds.center, coll.radius, Vector2.down, .1f, jumpableGround);
    }
}
