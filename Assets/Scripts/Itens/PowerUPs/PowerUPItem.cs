using System;
using UnityEngine;

public enum PowerUP
{
    PaddleSize,
    PaddleSpeed,
    BallSize,
}

public class PowerUPItem : MonoBehaviour
{

    [SerializeField] PowerUP powerUp;
    [SerializeField] Vector2 fallSpeed;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = fallSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (powerUp)
            {
                case PowerUP.PaddleSize:
                    PowerUpManager.Instance.SizePowerUP();
                    break;
                case PowerUP.PaddleSpeed:
                    PowerUpManager.Instance.SpeedPowerUp();
                    break;
                case PowerUP.BallSize:
                    PowerUpManager.Instance.BallSizePowerUp();
                    break;
                default:
                    break;
            }
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Death"))
        {
            gameObject.SetActive(false);
        }
    }
}
