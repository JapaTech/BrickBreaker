using UnityEngine;

//Enum dos powerups
public enum PowerUP
{
    PaddleSize,
    PaddleSpeed,
    BallSize,
}

//Classe que vai dentro de um item de power up
public class PowerUPItem : MonoBehaviour
{
    //Tipo do powerup que irá ativar
    [SerializeField] private PowerUP powerUp;
    //Velocidade que o item cai
    [SerializeField] private Vector2 fallSpeed;
    //Referências de componentes
    private Rigidbody2D rb;
    private AudioSource itemAudio;
    //Posição que o audio sai
    private Vector3 playAudioAtPoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        itemAudio = GetComponent<AudioSource>();
        playAudioAtPoint = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }

    //Faz o item se mover
    private void FixedUpdate()
    {
        rb.velocity = fallSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Ativa o power-up
        if (collision.gameObject.CompareTag("Player"))
        {
            itemAudio.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            AudioSource.PlayClipAtPoint(itemAudio.clip, playAudioAtPoint, 1f);
            switch (powerUp)
            {
                case PowerUP.PaddleSize:
                    PowerUpManager.Instance.PadderSizePowerUp();
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
        //Desativa o power-up se atingiu a zona da morte
        if (collision.gameObject.CompareTag("Death"))
        {
            gameObject.SetActive(false);
        }
    }
}
