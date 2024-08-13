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

    [SerializeField] private PowerUP powerUp;
    [SerializeField] private Vector2 fallSpeed;
    private Rigidbody2D rb;
    private AudioSource itemAudio;
    private Vector3 playAudioAtPoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        itemAudio = GetComponent<AudioSource>();
        playAudioAtPoint = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }

    private void FixedUpdate()
    {
        rb.velocity = fallSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            itemAudio.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            AudioSource.PlayClipAtPoint(itemAudio.clip, playAudioAtPoint);
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
