using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// When player collides with debuff object, object is destroyed and player enters debuffed state.
public class Debuff : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerCtrl controller = collision.GetComponent<PlayerCtrl>();

        if (controller != null)
        {
            controller.debuffSoundEffect.Play();

            Destroy(gameObject);

            controller.TransitionToState(typeof(PlayerDebuffState));
        }
    }
}
