using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defines the normal state of the player.
[CreateAssetMenu(menuName = "States/Player/Normal")]
public class PlayerNormalState : PlayerBaseState<PlayerCtrl>
{
    private Rigidbody2D rbody;
    private CircleCollider2D coll;

    // Movement and jump values. Assigned in Unity Inspector.
    public float speed;
    public float jumpForce;

    private bool canJump;
    private float horizontal;

    private LayerMask jumpableGround;
    private AudioSource jumpSoundEffect;

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
}

    // Gets left/right movement.
    public override void CaptureInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    // Checks if jump button is pressed, and if player is able to jump.
    public override void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
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

            // Prevents player from jumping while in mid-air.
            canJump = false;
        }
    }

    // Checks if player is in contact with proper ground collider.
    private bool IsGrounded()
    {
        return Physics2D.CircleCast(coll.bounds.center, coll.radius, Vector2.down, .1f, jumpableGround);
    }
}
