using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public bool Paused { get; private set; }

    public void Pause()
    {
        Paused = true;
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        Paused = false;
        Time.timeScale = 1;
    }
}
