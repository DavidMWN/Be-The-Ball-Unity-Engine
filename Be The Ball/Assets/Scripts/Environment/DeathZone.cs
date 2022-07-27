using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Kills player when then enter the trigger collider.
public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerCtrl controller = collision.GetComponent<PlayerCtrl>();

        if (controller != null)
        {
            // Checks for the Life Manager object (will only be null when testing individual levels).
            if (LifeManager.Instance != null)
            {
                if (LifeManager.Instance.lifeCount > 0)
                {
                    // If the player has extra lives, one life is subtracted and player is sent into Dead state.
                    controller.LifeCount(-1);
                    controller.TransitionToState(typeof(PlayerDeadState));
                }
                else
                {
                    // If the player has no extra lives, player is sent into Game Over state.
                    controller.TransitionToState(typeof(PlayerGameOverState));
                }
            }
            else
            {
                controller.TransitionToState(typeof(PlayerDeadState));
            }
        }
    }
}
