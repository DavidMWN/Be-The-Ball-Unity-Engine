using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Finishes level when the player enters the trigger area.
public class FinishZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerCtrl controller = collision.GetComponent<PlayerCtrl>();

        if (controller != null)
        {
            if (controller.Gems == 5)
            {
                controller.LifeCount(1);
            }
            controller.TransitionToState(typeof(PlayerFinishedState));
        }
    }
}