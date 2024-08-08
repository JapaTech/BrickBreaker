using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    private int score;

    [SerializeField] private int startHealth;

    private int health;

    public Action OnLoseHealth;

    private void Awake()
    {
        if(Instance !=null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        score = 0;
        health = startHealth;
    }

    public void UpdateScore(int value)
    {
        score += value;
    }

    public void TakeDamage()
    {
        health--;
        OnLoseHealth?.Invoke();

        if(health <= 0)
        {
            health = 0;
            Death();
        }
    }

    private void Death()
    {
        
    }

}
