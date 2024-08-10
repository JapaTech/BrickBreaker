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
            Destroy(this);
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
        Time.timeScale = 1;
    }

    public void Unpause(bool paused)
    {
        Paused = false;
        Time.timeScale = 0;
    }
}
