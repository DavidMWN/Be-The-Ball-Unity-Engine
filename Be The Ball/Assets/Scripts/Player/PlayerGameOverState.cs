using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Defines the Game Over state.
[CreateAssetMenu(menuName = "States/Player/GameOver")]
public class PlayerGameOverState : PlayerBaseState<PlayerCtrl>
{
    private Rigidbody2D rbody;

    // Called upon entering the state. Retrieves needed values from State Runner.
    public override void EnterState(PlayerCtrl player)
    {
        base.EnterState(player);

        if (rbody == null)
        {
            rbody = player.GetComponent<Rigidbody2D>();
        }

        // Freezes player in place.
        rbody.bodyType = RigidbodyType2D.Static;

        player.deathSoundEffect.Play();

        // Activates game over text on canvas.
        player.gameOverText.SetActive(true);
    }

    // No movement is allowed while player is in this state.
    public override void CaptureInput()
    {

    }

    // Player can only restart the game while in this state.
    public override void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            GameOver();
        }
    }

    public override void FixedUpdate()
    {

    }

    // Returns player to starting screen.
    private void GameOver()
    {
        SceneManager.LoadScene(0);
    }
}
