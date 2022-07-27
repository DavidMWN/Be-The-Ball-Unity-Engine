using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Controls what text the player sees upon completing the game.
public class GameCompleteStatus : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statusText;
        
    void Start()
    {
        if (LifeManager.Instance.lifeCount == 9)
        {
            // Player completes the game with the maximum number of lives (no deaths, collected all gems).
            statusText.text = "Flawless Completion!";
        }
        else if (LifeManager.Instance.lifeCount == 0)
        {
            // Player completes lives with no extra lives remaining.
            statusText.text = "Clutch Victory!";
        }
        else if (LifeManager.Instance.lifeCount < 0)
        {
            // Player completes the game with less than 0 lives (which should not be possible).
            statusText.text = "Wait, how'd you do that?";
        }
        else
        {
            // Player completes the game with any other normal amount of lives.
            statusText.text = "Game Complete!";
        }
    }

    
}
