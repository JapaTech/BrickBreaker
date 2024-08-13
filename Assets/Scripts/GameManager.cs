using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

[DefaultExecutionOrder(-2)]
public class GameManager : MonoBehaviour
{
    public Player PlayerRef { get; private set; }
    public Ball BallRef { get; private set; }

    public int Score { get; private set; }

    [SerializeField] private UIManager uiManager;
    [SerializeField] private AudioSource backgroundMusic;

    [SerializeField] private int startHealth;
    [SerializeField] private int maxHealth;

    private bool levelRegistred = false;
    public bool WinGame { get; private set; }

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

    public Action OnInitialize;
    public Action OnChangeScene;

    private List<Brick> bricks = new List<Brick>();

    public int Level { get; private set; } = 1;

    [SerializeField] private int NumberOfLevels;
    public static GameManager Instance { get; set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
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
        WinGame = false;
        Health = maxHealth;
        uiManager.nextLevelPannel.SetActive(false);
        uiManager.UpdateHealth(maxHealth);
        uiManager.UpdateScore(Score);
        Brick[] b = FindObjectsByType<Brick>(FindObjectsSortMode.None);
        bricks.Clear();
        bricks.AddRange(b);
        OnInitialize?.Invoke();
        Debug.Log("Iniciou" + bricks.Count);
    }

    private void OnEnable()
    {
        GameEvents.OnPlayerSpawn += SetPlayer;
        GameEvents.OnBallSpawn += SetBall;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerSpawn -= SetPlayer;
        GameEvents.OnBallSpawn -= SetBall;
    }

    private void SetPlayer(Player player)
    {
        PlayerRef = player;
    }

    private void SetBall(Ball ball)
    {
        BallRef = ball;
    }

    public void LoadLevel(int level)
    {
        Level = level;

        if (level > NumberOfLevels)
        {
            WinGame = true;
            uiManager.gameObject.SetActive(false);
            SceneManager.LoadScene("GameOver");
            return;
        }

        if (!levelRegistred)
        {
            SceneManager.sceneLoaded += OnLevelLoaded;
            levelRegistred = true;
        }

        SceneManager.LoadScene($"Level{level}");
        PauseManager.Instance.Unpause();
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        OnChangeScene?.Invoke();
        SceneManager.sceneLoaded -= OnLevelLoaded;
        levelRegistred = false;
        WriteScore();
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

    public void ChangeHealth(int damage)
    {
        Health += damage;
        uiManager.UpdateHealth(Health);    

        if(Health <= 0)
        {
            Death();
            
        }
    }

    private void Win()
    {
        Debug.Log("win");
        uiManager.nextLevelPannel.SetActive(true);
        PauseManager.Instance.Pause();
    }

    private void Death()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        DestroyManagers();
    }

    public string WriteScore()
    {
        string filePath = Application.persistentDataPath + "\\score.txt";
        string texto = "Score: " + Score.ToString() ;
        
        File.WriteAllText(filePath, texto);
        return filePath;
    }

    public void DestroyManagers()
    {
        Destroy(gameObject);
        Destroy(uiManager.gameObject);
        Destroy(PowerUpManager.Instance.gameObject);
    }

}
