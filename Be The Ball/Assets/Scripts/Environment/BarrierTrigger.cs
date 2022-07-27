using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sets associated object to active status when player enters trigger collider.
public class BarrierTrigger : MonoBehaviour
{
    [SerializeField] GameObject _barrier;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerCtrl controller = collision.GetComponent<PlayerCtrl>();

        if (controller != null)
        {
            _barrier.SetActive(true);
        }
    }
}
