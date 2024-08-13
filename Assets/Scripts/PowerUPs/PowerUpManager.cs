using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class PowerUpManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Ball ball;

    public static PowerUpManager Instance { get; set; }

    public bool IsPowerUPActive { get; private set; }

    [SerializeField] private float powerUpDuration = 30f;

    [SerializeField] private float newWidth;
    [SerializeField] private float newPlayerSpeed;
    [SerializeField] private float newBallSize;

    IPowerUp actualPowerUp;

    PaddleSizePowerUp paddleSizePowerUp;
    PaddleSpeedPowerUP paddleSpeedPowerUp;
    BallSizePowerUp ballSizePowerUp;

    [SerializeField] PowerUPItem[] powerUPsToSpawn;

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

    private void OnEnable()
    {
        GameEvents.OnPlayerSpawn += ConfigurePlayerPowerUps;
        GameEvents.OnBallSpawn += ConfigureBallPowerUps;
    }

    private void Start()
    {
        
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerSpawn -= ConfigurePlayerPowerUps;
        GameEvents.OnBallSpawn -= ConfigureBallPowerUps;
        
    }

    public void ConfigurePlayerPowerUps(Player player)
    {
        this.player = player;
        paddleSizePowerUp = new PaddleSizePowerUp(newWidth, this.player);
        paddleSpeedPowerUp = new PaddleSpeedPowerUP(newPlayerSpeed, this.player);
        
    }

    public void ConfigureBallPowerUps(Ball ball)
    {
        this.ball = ball;
        ballSizePowerUp = new BallSizePowerUp(newBallSize, this.ball, 2);
    }

    public void SpawnPowerUp(Vector3 pos)
    {       
        PowerUPItem newPowerUP = Instantiate(powerUPsToSpawn[Random.Range(0, powerUPsToSpawn.Length)], pos, Quaternion.identity);
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
        if (IsPowerUPActive)
        {
            StopCoroutine(DeactivePowerUp());
            actualPowerUp.Deactivate();
        }

        IsPowerUPActive = true;
        actualPowerUp = powerUPCommand;
        actualPowerUp.Active();
        StartCoroutine(DeactivePowerUp());
    }

    public IEnumerator DeactivePowerUp()
    {
        yield return new WaitForSeconds(powerUpDuration);
        actualPowerUp.Deactivate();
        IsPowerUPActive = false;
    }

}
