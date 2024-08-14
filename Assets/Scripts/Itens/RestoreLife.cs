using UnityEngine;

public class RestoreLife : MonoBehaviour
{
    //Quantidae de vida que restaura
    [SerializeField] private int amount =1;
    
    //Velocida que o item cai
    [SerializeField] private Vector2 fallSpeed;

    //Referências dos compoentes
    private Rigidbody2D rb;
    private AudioSource restoreAudio;

    //Onde o áudio vai sair
    private Vector3 playAudioAtPoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        restoreAudio = GetComponent<AudioSource>();
        playAudioAtPoint = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }

    private void FixedUpdate()
    {
        rb.velocity = fallSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Se atingir o jogador restaura vida dele
        if (collision.CompareTag("Player"))
        {
            restoreAudio.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            AudioSource.PlayClipAtPoint(restoreAudio.clip, playAudioAtPoint, 1f);
            GameManager.Instance.ChangeHealth(amount);
            gameObject.SetActive(false);
        }
        //Se atigir a zona da morte desartiva
        if (collision.CompareTag("Death"))
        {
            gameObject.SetActive(false);
        }
    }
}
