using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{

    public int Score { get; private set; }

    [SerializeField] private UIManager uiManager;
    [SerializeField] private AudioSource backgroundMusic;

    [SerializeField] private int startHealth;
    [SerializeField] private int maxHealth;
    
    [field: SerializeField]
    private int health;
    public int Health
    {
        get 
        {
            return health;
        }
        set
        {
            health = UnityEngine.Mathf.Clamp(value, 0, maxHealth);
        }
    }

    public Action OnLoseHealth;

    private List<Brick> bricks = new List<Brick>();

    public int Level { get; private set; } = 1;

    [SerializeField] private int NumberOfLevels;
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
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        Initialize();
        Score = 0;
    }

    private void Initialize()
    {
        Health = maxHealth;
        uiManager.UpdateHealth(maxHealth);
        uiManager.UpdateScore(Score);
        Brick[] b = FindObjectsByType<Brick>(FindObjectsSortMode.None);
        bricks.Clear();
        bricks.AddRange(b);
    }

    public void LoadLevel(int level)
    {
        Level = level;

        if (level > NumberOfLevels)
        {

            SceneManager.LoadScene("GameOver");
        }

        SceneManager.sceneLoaded += OnLevelLoaded;
        SceneManager.LoadScene($"Level{level}");
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
        Initialize();
    }

    public void BrickDestroyed(Brick b)
    {
        UpdateScore(b.Points);
        bricks.Remove(b);
        if(bricks.Count == 0)
        {
            Win();
        }
    }

    private void UpdateScore(int value)
    {
        Score += value;
        uiManager.UpdateScore(Score);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        OnLoseHealth?.Invoke();

        if(Health <= 0)
        {
            Death();
            return;
        }
        uiManager.UpdateHealth(Health);
        
    }

    private void Win()
    {
        uiManager.nextLevelPannel.SetActive(true);
    }

    private void Death()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Destroy(gameObject);
    }

    public void WriteScore()
    {
        string filePath = Application.persistentDataPath + "\\score.txt";
        string texto = "Score: 100";
        
        File.WriteAllText(filePath, texto);
    }

}
