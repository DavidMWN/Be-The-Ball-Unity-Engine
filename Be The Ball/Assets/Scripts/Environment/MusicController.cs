using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    // Ensures a single instance of this object, that persists across scenes.
    public void Awake()
    {
        GameObject[] musicObject = GameObject.FindGameObjectsWithTag("Music");
        if (musicObject.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
