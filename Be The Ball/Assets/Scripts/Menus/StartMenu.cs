using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Loads first level and sets starting amount of player's lives.
public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        LifeManager.Instance.lifeCount = 5;
        SceneManager.LoadScene(2);
    }
}
