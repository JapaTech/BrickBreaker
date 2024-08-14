using UnityEngine;

public class Ball : MonoBehaviour
{
    //Referencias de componetes
    private Transform tr;
    private Rigidbody2D rb;
    
    //C�lcalos de f�sica e posicionamento
    [SerializeField] private float speed = 50f;
    private Vector3 startPos;

    //Dano inicial da bola nos bricks
    [SerializeField] private int startDamage;
    
    //Propiedade do dano da bola;
    [field: SerializeField] public int Damage { get; set; } = 1;

    //Audio
    private AudioSource ballAudio;

    private void Awake()
    {
        //Refer�ncias dos componentes
        rb = GetComponent<Rigidbody2D>();
        tr = transform;
        ballAudio = GetComponent<AudioSource>();

        //Definir posi��o inicial
        startPos = tr.position;
    }

    private void OnEnable()
    {
        //Quando a bola for instanciada, dispara um evento passando si mesmo
        GameEvents.BallSpawned(this);
    }

    void Start()
    {
        //Definir dano com que a bola come�a
        Damage = startDamage;
        
        //Adicionar uma for�a no come�o do jogo
        Invoke(nameof(AddRandomTrajectory), 1.5f);
    }


    private void FixedUpdate()
    {
        //Normaliza a velocidade da bola para ela andar sempre com velocidade constante
        rb.velocity = rb.velocity.normalized * speed;
    }

    //Coloca a bola na posi��ao inicial e adiciona uma for�a para baixo, como acontece no come�o
    public void SetBallToDefautl()
    {
        tr.position = startPos;
        rb.velocity = Vector2.zero;
        Invoke(nameof(AddRandomTrajectory), 2f);
    }

    //Adiciona uma trajetoria para bola para baixo, podendo ser para a esquerda ou direita
    private void AddRandomTrajectory()
    {
        Vector2 force = new Vector2(Random.Range(-0.5f, 0.5f), -1f);

        rb.AddForce(force.normalized * speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Instancia o audio sempre que a bola bater em alguma coisa
        ballAudio.pitch = Random.Range(0.8f, 1.21f);
        ballAudio.Play();

        //Se a bola bater na zona da morte, ela volta para a posi��o inicial e uma vida � perdida
        if (collision.gameObject.CompareTag("Death"))
        {
            GameManager.Instance.ChangeHealth(-1);
            SetBallToDefautl();
            rb.velocity = Vector2.zero;
            return;
        }

        //Caso a velocidade vertical da bola seja muito baixa, adiciona uma for�a
        if (Mathf.Abs(rb.velocity.y) <= 0.6f && Mathf.Abs(rb.velocity.y) > 0)
        {
            Vector2 force = new Vector2(0, rb.velocity.normalized.y * 0.3f);
            rb.AddForce(force.normalized, ForceMode2D.Impulse);
        }
        //Agora nos raros casos que a n�o h� velocidade vertical (o que resultaria em softlock se n�o fosse tratado)
        else if (rb.velocity.y == 0f)
        {
            //Se for na metade de baixo da tela, adiciona uma for�a para cima
            if(transform.position.y <= 0)
            {
                Vector2 force = new Vector2(1, 3f);
                rb.AddForce(force.normalized, ForceMode2D.Impulse);
            }
            //Caso contr�rio, adiciona uma for�a para baixo
            else
            {
                Vector2 force = new Vector2(1, -3f);
                rb.AddForce(force.normalized, ForceMode2D.Impulse);
            }
        }
    }
}
