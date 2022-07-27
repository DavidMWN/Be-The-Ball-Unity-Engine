using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Defines the player's death state.
[CreateAssetMenu(menuName = "States/Player/Dead")]
public class PlayerDeadState : PlayerBaseState<PlayerCtrl>
{
    private Rigidbody2D rbody;

    // Called upon entering the state. Retrieves needed values from State Runner.
    public override void EnterState(PlayerCtrl player)
    {
        base.EnterState(player);

        if(rbody == null)
        {
            rbody = player.GetComponent<Rigidbody2D>();
        }

        // Freezes player in place.
        rbody.bodyType = RigidbodyType2D.Static;

        player.deathSoundEffect.Play();

        // Activates death text on the canvas.
        player.deadText.SetActive(true);
    }

    // No movement allowed in this state.
    public override void CaptureInput()
    {

    }

    // Restarting the level is the only option available to the player.
    public override void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            RestartLevel();
        }
    }

    public override void FixedUpdate()
    {

    }

    // Restarts the level.
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
