using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defines the debuffed state of the player.
[CreateAssetMenu(menuName = "States/Player/Debuff")]
public class PlayerDebuffState : PlayerBaseState<PlayerCtrl>
{
    private Rigidbody2D rbody;
    private CircleCollider2D coll;

    // Movement and jump vales. Assigned in Unity Inspector.
    // Both values should be half Normal State values.
    public float speed;
    public float jumpForce;

    private bool canJump;
    private float horizontal;

    private LayerMask jumpableGround;
    private AudioSource jumpSoundEffect;
    
    // Number of seconds debuff will last.
    // Should be 3 seconds.
    public float debuffTimer;

    // Accesses VFX and camera effects.
    private DebuffCameraEffectApplier debuffVFX;
    private Animator animator;

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

        debuffVFX = player.debuffVFX;
        animator = player.GetComponent<Animator>();
        
        // Sets duration of debuffed state.
        debuffTimer = 3.0f;
        // Triggers camera zoom.
        animator.SetTrigger("Debuffed");
    }

    // Gets left/right movement.
    public override void CaptureInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    public override void Update()
    {
        // Enables VFX.
        debuffVFX.Debuffed = true;

        // Checks if jump button is pressed, and if player is able to jump.
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                canJump = true;
            }
        }
              
        // Countdown for duration of debuffed state.
        if (debuffTimer > 0)
        {
            debuffTimer -= Time.deltaTime;
        }
        else
        {
            debuffTimer = 0;

            // Returns player to Normal state.
            // Intentionally does not restore Multi-Jump state if player was previously in that state.
            _active.TransitionToState(typeof(PlayerNormalState));
        }            
    }

    // Player Movement.
    public override void FixedUpdate()
    {
        rbody.velocity = new Vector2(horizontal * speed, rbody.velocity.y);

        if (canJump)
        {
            jumpSoundEffect.Play();
            rbody.velocity = new Vector2(rbody.velocity.x, jumpForce);

            // Prevents mid-air jumps.
            canJump = false;
        }
    }

    // Checks if player is in contact with proper ground collider.
    private bool IsGrounded()
    {
        return Physics2D.CircleCast(coll.bounds.center, coll.radius, Vector2.down, .1f, jumpableGround);
    }
}
