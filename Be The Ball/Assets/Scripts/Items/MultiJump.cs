using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// When player collides with multi-jump object, object is destroyed and player enter multi-jump state.
public class MultiJump : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerCtrl controller = collision.GetComponent<PlayerCtrl>();

        if (controller != null)
        {
            controller.buffSoundEffect.Play();

            Destroy(gameObject);

            controller.TransitionToState(typeof(PlayerMultiJumpState));
        }
    }
}
