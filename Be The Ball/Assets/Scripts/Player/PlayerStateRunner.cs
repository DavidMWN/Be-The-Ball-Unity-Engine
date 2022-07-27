using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

// This class manages the player's state and common properties.
public abstract class PlayerStateRunner<T> : MonoBehaviour where T: MonoBehaviour
{
    // List of states in Unity Inspector
    [SerializeField] private List<PlayerBaseState<T>> _states;
    private readonly Dictionary<Type, PlayerBaseState<T>> _stateByType = new ();
    private PlayerBaseState<T> _currentState;
    
    // Gem count and UI.
    private int gems;
    public int Gems
    { get { return gems; } }
    [SerializeField] private TextMeshProUGUI gemsText;

    // Life-count UI.
    [SerializeField] private TextMeshProUGUI livesText;

    // Texts for player death, game over, and level finish.
    [SerializeField] public GameObject deadText;
    [SerializeField] public GameObject gameOverText;
    [SerializeField] public GameObject finishedText;

    // Defines tiles player can jump from.
    [SerializeField] public LayerMask jumpableGround;

    // Sound effects.
    [SerializeField] public AudioSource jumpSoundEffect;
    [SerializeField] public AudioSource collectionSoundEffect;
    [SerializeField] public AudioSource buffSoundEffect;
    [SerializeField] public AudioSource debuffSoundEffect;
    [SerializeField] public AudioSource finishSoundEffect;
    [SerializeField] public AudioSource deathSoundEffect;

    // Bloom/vignette effects for debuff.
    public DebuffCameraEffectApplier debuffVFX;

    protected virtual void Awake()
    {
        // Adds states to dictionary of states.
        _states.ForEach(s => _stateByType.Add(s.GetType(), s));

        // Upon level load, sets player to first state in state dictionary.
        // (Normal state should be at index 0 in the Unity Inspector.)
        TransitionToState(_states[0].GetType());
    }

    public void TransitionToState(Type newStateType)
    {
        // Changes player's current state.
        _currentState = _stateByType[newStateType];

        // Calls first function to run upon entering a new state.
        _currentState.EnterState(player:GetComponent<T>());       
    }

    void Start()
    {        
        // Resets gem count to 0 upon loading a new level.
        gems = 0;

        // Checks for LifeManager. Used for testing individual levels.
        if (LifeManager.Instance != null)
        {
            LifeCount(0);
        }
    }

    // Runs CaptureInput and Update functions of player's current state.
    void Update()
    {        
        _currentState.CaptureInput();
        _currentState.Update();
    }

    // Runs FixedUpdate function of player's current state.
    void FixedUpdate()
    {
        _currentState.FixedUpdate();
    }
    
    // Gem collection and UI update.
    public void CollectGem()
    {
        gems++;
        gemsText.text = "Gems: " + gems;
    }

    // Adjusts player life-count and Life UI.
    public void LifeCount(int lifeModifier)
    {
        LifeManager.Instance.lifeCount = LifeManager.Instance.lifeCount + lifeModifier;
        livesText.text = "Lives: " + LifeManager.Instance.lifeCount;
    }
}
