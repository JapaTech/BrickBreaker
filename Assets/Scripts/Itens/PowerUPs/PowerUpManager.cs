using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance { get; set; }

    public bool IsPowerUPActive { get; private set; }

    [SerializeField] private float powerUpDuration = 30f;

    [SerializeField] private float newWidth;
    [SerializeField] private float newPlayerSpeed;
    [SerializeField] private float newBallSize;
    [SerializeField] private int newBallDamage = 2;

    private IPowerUp actualPowerUp;

    private PaddleSizePowerUp paddleSizePowerUp;
    private PaddleSpeedPowerUP paddleSpeedPowerUp;
    private BallSizePowerUp ballSizePowerUp;

    [SerializeField] private PowerUPItem[] powerUPsToSpawn;
    [SerializeField] private RestoreLife restoreLife;

    private Coroutine deactivePowerUp;

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

    public void ConfigurePlayerPowerUps()
    {
        paddleSizePowerUp = new PaddleSizePowerUp(newWidth, GameManager.Instance.PlayerRef) ;
        paddleSpeedPowerUp = new PaddleSpeedPowerUP(newPlayerSpeed, GameManager.Instance.PlayerRef);
        
    }

    public void ConfigureBallPowerUps()
    {
        ballSizePowerUp = new BallSizePowerUp(newBallSize, GameManager.Instance.BallRef, newBallDamage);
    }

    public void SpawnPowerUp(Vector3 pos)
    {
        float rand = Random.value;
        {
            if(rand <= 0.4)
            {
                RestoreLife rl = Instantiate(restoreLife, pos, Quaternion.identity);

            }
            else
            {
                PowerUPItem newPowerUP = Instantiate(powerUPsToSpawn[Random.Range(0, powerUPsToSpawn.Length)], pos, Quaternion.identity);

            }
        }

    }

    public void SizePowerUP()
    {
        ActivePowerUp(paddleSizePowerUp);
    }

    public void SpeedPowerUp()
    {
        ActivePowerUp(paddleSpeedPowerUp);
    }

    public void BallSizePowerUp()
    {
        ActivePowerUp(ballSizePowerUp);
    }

    public void ActivePowerUp(IPowerUp powerUPCommand)
    {
        if(deactivePowerUp != null)
        {
            StopCoroutine(deactivePowerUp);
        }

        if (IsPowerUPActive)
        {
            actualPowerUp.Deactivate();
            actualPowerUp = null;
        }

        IsPowerUPActive = true;
        actualPowerUp = powerUPCommand;
        actualPowerUp.Active();
        deactivePowerUp = StartCoroutine(DeactivePowerUp());

    }

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

    public void EndExecution()
    {
        actualPowerUp = null;
        if (deactivePowerUp != null)
        {
            StopCoroutine(deactivePowerUp);
        }
    }

}
