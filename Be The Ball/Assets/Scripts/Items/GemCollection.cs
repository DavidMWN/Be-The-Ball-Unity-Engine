using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// When player collides with gem object, gem is destroyed and player's gem count increases.
public class GemCollection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerCtrl controller = collision.GetComponent<PlayerCtrl>();

        if (controller != null)
        {
            controller.collectionSoundEffect.Play();

            Destroy(gameObject);

            controller.CollectGem();
        }
    }
}
