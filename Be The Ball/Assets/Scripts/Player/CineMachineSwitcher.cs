using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls slight zoom-in of camera when player enters debuffed state.
public class CineMachineSwitcher : MonoBehaviour
{
    private Animator animator;

    private bool mainCamera = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        SwitchState();
    }

    public void SwitchState()
    {
        if (mainCamera)
        {
            animator.Play("Main Camera");
        }
        else
        {
            animator.Play("Debuff Camera");
        }

        mainCamera = !mainCamera;
    }
}
