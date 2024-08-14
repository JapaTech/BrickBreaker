using System.Collections;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class PowerUpManager : MonoBehaviour
{
    //Quanto tempo dura um power-up
    [SerializeField] private float powerUpDuration = 30f;

    //Novo tamanh da pá
    [SerializeField] private float newWidth;

    //Nova velocidade do jogador
    [SerializeField] private float newPlayerSpeed;

    //Novo tamanho da bola
    [SerializeField] private float newBallSize;
    //Novo dano da bola
    [SerializeField] private int newBallDamage = 2;

    //Objetos do power-up
    private PaddleSizePowerUp paddleSizePowerUp;
    private PaddleSpeedPowerUp paddleSpeedPowerUp;
    private BallSizePowerUp ballSizePowerUp;

    //Lista dos power-ups para dar spawn
    [SerializeField] private PowerUPItem[] powerUPsToSpawn;
    [SerializeField] private RestoreLife restoreLife;
    
    //Power-up atual
    private IPowerUp actualPowerUp;

    //Verifica se tem power-up ativo
    public bool IsPowerUPActive { get; private set; }

    //Referência da corrotina que desativa o power-up
    private Coroutine deactivePowerUp;
    
    //Singleton
    public static PowerUpManager Instance { get; set; }
    
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

    private void OnEnable()
    {
        GameManager.Instance.OnChangeScene += EndExecution;
        GameManager.Instance.OnInitialize += ConfigureBallPowerUps;
        GameManager.Instance.OnInitialize += ConfigurePlayerPowerUps;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnInitialize -= ConfigureBallPowerUps;
        GameManager.Instance.OnInitialize -= ConfigurePlayerPowerUps;
        GameManager.Instance.OnChangeScene -= EndExecution;
    }

    private void Start()
    {
        ConfigurePlayerPowerUps();
        ConfigureBallPowerUps();
    }

    //Configura os power-ups que afetam o jogador
    public void ConfigurePlayerPowerUps()
    {
        paddleSizePowerUp = new PaddleSizePowerUp(newWidth, GameManager.Instance.PlayerRef) ;
        paddleSpeedPowerUp = new PaddleSpeedPowerUp(newPlayerSpeed, GameManager.Instance.PlayerRef);
        
    }

    //Configura os power-ups que afetam a bola
    public void ConfigureBallPowerUps()
    {
        ballSizePowerUp = new BallSizePowerUp(newBallSize, GameManager.Instance.BallRef, newBallDamage);
    }

    //Spawna o item, recebe a posição do bloco destruído
    public void SpawnPowerUp(Vector3 pos)
    {
        //Sorteia um valor
        float rand = Random.value;
        {
            //40% de chance de ser vida
            if(rand <= 0.4)
            {
                RestoreLife rl = Instantiate(restoreLife, pos, Quaternion.identity);

            }
            //Se não vai criar sortear um power-up aleatório do array
            else
            {
                PowerUPItem newPowerUP = Instantiate(powerUPsToSpawn[Random.Range(0, powerUPsToSpawn.Length)], pos, Quaternion.identity);
            }
        }

    }

    //Ativa o power-up do tamanho da pá
    public void PadderSizePowerUp()
    {
        ActivePowerUp(paddleSizePowerUp);
    }

    //Ativa o power-up do da velocidade da pá
    public void SpeedPowerUp()
    {
        ActivePowerUp(paddleSpeedPowerUp);
    }

    //Ativa o power-up do tamanho da bola
    public void BallSizePowerUp()
    {
        ActivePowerUp(ballSizePowerUp);
    }

    //Ativa o power recebido
    public void ActivePowerUp(IPowerUp powerUPCommand)
    {
        //Desativa a corrotina de desativar power-up
        if(deactivePowerUp != null)
        {
            StopCoroutine(deactivePowerUp);
        }

        //Se tiver um power-up ativo, desativa
        if (IsPowerUPActive)
        {
            actualPowerUp.Deactivate();
            actualPowerUp = null;
        }

        //Ativa o novo power-up
        IsPowerUPActive = true;
        actualPowerUp = powerUPCommand;
        actualPowerUp.Active();

        //Começa a corrotiva para para oo power-up
        deactivePowerUp = StartCoroutine(DeactivePowerUp());

    }

    //É o cooldown que o power-up fica ativo
    public IEnumerator DeactivePowerUp()
    {
        yield return new WaitForSeconds(powerUpDuration);
        if(actualPowerUp != null)
        {
            actualPowerUp.Deactivate();
            actualPowerUp = null;
        }
        IsPowerUPActive = false;
    }

    //Para a execução desse código ao trocar de cena
    public void EndExecution()
    {
        actualPowerUp = null;
        if (deactivePowerUp != null)
        {
            StopCoroutine(deactivePowerUp);
        }
    }

}
