using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Ball ball;

    public static PowerUpManager Instance { get; set; }

    public bool IsPowerUPActive { get; private set; }

    [SerializeField] private float powerUpDuration = 30f;

    IPowerUp actualPowerUp;

    PaddleSizePowerUp paddleSizePowerUp;
    PaddleSpeedCommand paddleSpeedPowerUp;
    BallSizePowerUp ballSizePowerUp;

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
    private void Start()
    {
        paddleSizePowerUp = new PaddleSizePowerUp(1.2f, player);
        paddleSpeedPowerUp = new PaddleSpeedCommand(8f, player);
        ballSizePowerUp = new BallSizePowerUp(1.5f, ball);
    }

    public void SizePowerUP()
    {
        ActivePowerUp(paddleSpeedPowerUp);
    }

    public void SpeedPowerUp()
    {
        ActivePowerUp(paddleSpeedPowerUp);
    }

    public void BallSizePowerUp()
    {
        ActivePowerUp(ballSizePowerUp);
    }

    private void ActivePowerUp(IPowerUp powerUPCommand)
    {
        if (IsPowerUPActive)
            return;

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
