using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Defines state of player when a level is finished.
[CreateAssetMenu(menuName = "States/Player/Finished")]
public class PlayerFinishedState : PlayerBaseState<PlayerCtrl>
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

        player.finishSoundEffect.Play();

        // Activates game over text on canvas.
        player.finishedText.SetActive(true);
    }

    // No movement is allowed while player is in this state.
    public override void CaptureInput()
    {

    }

    // Player can only go to the next level while in this state.
    public override void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            NextLevel();
        }
    }

    public override void FixedUpdate()
    {

    }

    // Loads next level.
    private void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
