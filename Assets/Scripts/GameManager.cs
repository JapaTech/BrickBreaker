using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{

    private int score;

    [SerializeField] private int startHealth;

    private int health;

    public Action OnLoseHealth;

    private List<Brick> bricks = new List<Brick>();
    public static GameManager Instance { get; set; }

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
        Brick[] b = FindObjectsByType<Brick>(FindObjectsSortMode.None);
        bricks.AddRange(b);
        score = 0;
        health = startHealth;
    }

    public void BrickDestroyed(Brick b)
    {
        UpdateScore(b.points);
        bricks.Remove(b);
        if(bricks.Count == 0)
        {
            Win();
        }
    }

    private void UpdateScore(int value)
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

    private void Win()
    {
        Debug.Log("Win");
    }

    private void Death()
    {
        
    }

    public void WriteScore()
    {
        string filePath = Application.persistentDataPath + "\\score.txt";
        string texto = "Score: 100";
        
        File.WriteAllText(filePath, texto);
    }

}
