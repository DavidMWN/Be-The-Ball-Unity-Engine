using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract class for player states.
public abstract class PlayerBaseState<T> : ScriptableObject where T: MonoBehaviour
{
    protected T _active;

    public virtual void EnterState(T player)
    {
        _active = player;
    }

    public abstract void CaptureInput();

    public abstract void Update();

    public abstract void FixedUpdate();
}
