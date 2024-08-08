using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    private int score;

    [SerializeField] private int startHealth;
    [SerializeField] private Ball ball;
    [SerializeField] private Player player;

    private int health;

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

        if(health <= 0)
        {
            health = 0;
            Death();
        }
        else
        {
            player.ResetPlayer();
            ball.ResetBall();
        }
    }

    private void Death()
    {
        
    }

}
