using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

//Deve ser executado antes dos outros scripts para garantir que tem as refêrencias e foi inscrito nos eventos
[DefaultExecutionOrder(-2)]
public class GameManager : MonoBehaviour
{
    //Referência do jogador
    public Player PlayerRef { get; private set; }
    
    //Referência da bola
    public Ball BallRef { get; private set; }

    //Lista para guardar blocos na cena
    private List<Brick> bricks = new List<Brick>();

    //Referência para o controlador de UI
    [SerializeField] private UIManager uiManager;
    
    //Vida inicial
    [SerializeField] private int startHealth;
    
    //Vida máxima
    [SerializeField] private int maxHealth;

    //Boleano usado para verificar se foi inscrito no evento de trocar cena
    private bool levelRegistred = false;
    
    //Propertie do nível
    public int Level { get; private set; } = 1;

    //Quantidades de níveis
    [SerializeField] private int numberOfLevels;

    //Boloena que verifica se o jogador ganhou
    public bool WinGame { get; private set; }
    
    //Armazena a pontução do player
    public int Score { get; private set; }

    //Propriedade da vida do player
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
            //Impede que a vida seja maior que vida inicial ou menor que 0
            health = UnityEngine.Mathf.Clamp(value, 0, maxHealth);
        }
    }

    //Música de fundo
    [SerializeField] private AudioSource backgroundMusic;

    //Açãp para quando começa um novo jogo
    public Action OnInitialize;
    //Ação para quando começa a cena é trocada
    public Action OnChangeScene;

    //Singleton
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
    
    //Comandos que devem ser executados toda vez que uma fase do jogo começar
    private void Initialize()
    {
        WinGame = false;
        Health = maxHealth;
        uiManager.NextLevelPannel.SetActive(false);
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

    //Cria a referência do player
    private void SetPlayer(Player player)
    {
        PlayerRef = player;
    }

    //Cria a referência da bola
    private void SetBall(Ball ball)
    {
        BallRef = ball;
    }

    //Carrega uma fase
    public void LoadLevel(int level)
    {
        //Atribuí o número do Level ao recebido
        Level = level;

        //Se o número for maior que o número de fases do jogo
        if (level > numberOfLevels)
        {
            //Jogador ganha
            WinGame = true;
            //Desativa a UI
            uiManager.gameObject.SetActive(false);
            //Carrega a tela de game oveer
            SceneManager.LoadScene("GameOver");
            return;
        }

        //Se o nível não estiver registrado para ouvir a ação
        if (!levelRegistred)
        {
            //Faz o registro
            SceneManager.sceneLoaded += OnLevelLoaded;
            levelRegistred = true;
        }

        //Carrega uma nova cena
        SceneManager.LoadScene($"Level{level}");
        //Tira o jogo do pause
        PauseManager.Instance.Unpause();
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        //Invoca que a cena foi carregada
        OnChangeScene?.Invoke();
        //Tira o registro cena como ouvinte
        SceneManager.sceneLoaded -= OnLevelLoaded;
        levelRegistred = false;
        //Escreve o arquivo com o valor da pontuação
        WriteScore();
        //Chama a função que faz as intruções necessárias para o jogo funcionar
        Initialize();
    }

    //Quando um bloco for destruído, atualiza os pontos e retira o bloco da lista de blocos
    public void BrickDestroyed(Brick b)
    {
        UpdateScore(b.Points);
        bricks.Remove(b);

        //Se a listas estiver vazia, ganha o jogo
        if(bricks.Count == 0)
        {
            Win();
        }
    }

    //Altera o score
    private void UpdateScore(int value)
    {
        Score += value;
        uiManager.UpdateScore(Score);
    }

    //Altera o valor da vida
    public void ChangeHealth(int damage)
    {
        Health += damage;
        uiManager.UpdateHealth(Health);    

        //Se avida for menor ou igual a 0 game over.
        if(Health <= 0)
        {
            Death();
        }
    }

    //Pausa o jogo e avança para a próxima fase
    private void Win()
    {
        Debug.Log("win");
        uiManager.NextLevelPannel.SetActive(true);
        PauseManager.Instance.Pause();
    }

    //Vai apra a cena de game over
    private void Death()
    {
        SceneManager.LoadScene("GameOver");
    }

    //Vai para a cena do menu inicial
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        DestroyManagers();
    }
    
    //Exporta a pontução em um arquivo txt;
    public string WriteScore()
    {
        string filePath = Application.persistentDataPath + "\\score.txt";
        string texto = "Score: " + Score.ToString() ;
        
        File.WriteAllText(filePath, texto);
        return filePath;
    }

    //Destroí todos os managers. Para momentos eme que eles não são mais necessários, tipo no menu.
    public void DestroyManagers()
    {
        Destroy(gameObject);
        Destroy(uiManager.gameObject);
        Destroy(PowerUpManager.Instance.gameObject);
    }

}
