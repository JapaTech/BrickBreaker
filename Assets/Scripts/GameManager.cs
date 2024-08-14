using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

//Deve ser executado antes dos outros scripts para garantir que tem as ref�rencias e foi inscrito nos eventos
[DefaultExecutionOrder(-2)]
public class GameManager : MonoBehaviour
{
    //Refer�ncia do jogador
    public Player PlayerRef { get; private set; }
    
    //Refer�ncia da bola
    public Ball BallRef { get; private set; }

    //Lista para guardar blocos na cena
    private List<Brick> bricks = new List<Brick>();

    //Refer�ncia para o controlador de UI
    [SerializeField] private UIManager uiManager;
    
    //Vida inicial
    [SerializeField] private int startHealth;
    
    //Vida m�xima
    [SerializeField] private int maxHealth;

    //Boleano usado para verificar se foi inscrito no evento de trocar cena
    private bool levelRegistred = false;
    
    //Propertie do n�vel
    public int Level { get; private set; } = 1;

    //Quantidades de n�veis
    [SerializeField] private int numberOfLevels;

    //Boloena que verifica se o jogador ganhou
    public bool WinGame { get; private set; }
    
    //Armazena a pontu��o do player
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

    //M�sica de fundo
    [SerializeField] private AudioSource backgroundMusic;

    //A��p para quando come�a um novo jogo
    public Action OnInitialize;
    //A��o para quando come�a a cena � trocada
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
    
    //Comandos que devem ser executados toda vez que uma fase do jogo come�ar
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

    //Cria a refer�ncia do player
    private void SetPlayer(Player player)
    {
        PlayerRef = player;
    }

    //Cria a refer�ncia da bola
    private void SetBall(Ball ball)
    {
        BallRef = ball;
    }

    //Carrega uma fase
    public void LoadLevel(int level)
    {
        //Atribu� o n�mero do Level ao recebido
        Level = level;

        //Se o n�mero for maior que o n�mero de fases do jogo
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

        //Se o n�vel n�o estiver registrado para ouvir a a��o
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
        //Escreve o arquivo com o valor da pontua��o
        WriteScore();
        //Chama a fun��o que faz as intru��es necess�rias para o jogo funcionar
        Initialize();
    }

    //Quando um bloco for destru�do, atualiza os pontos e retira o bloco da lista de blocos
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

    //Pausa o jogo e avan�a para a pr�xima fase
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
    
    //Exporta a pontu��o em um arquivo txt;
    public string WriteScore()
    {
        string filePath = Application.persistentDataPath + "\\score.txt";
        string texto = "Score: " + Score.ToString() ;
        
        File.WriteAllText(filePath, texto);
        return filePath;
    }

    //Destro� todos os managers. Para momentos eme que eles n�o s�o mais necess�rios, tipo no menu.
    public void DestroyManagers()
    {
        Destroy(gameObject);
        Destroy(uiManager.gameObject);
        Destroy(PowerUpManager.Instance.gameObject);
    }

}
