using UnityEngine;

public class Player : MonoBehaviour
{
    //Referência do input system
    private PlayerInput playerInput;
    
    //Componentes
    private Rigidbody2D rb;
    private Transform tr;

    //Inutilizada, iria servir para trocar os controles do jogador
    private int invertControls = 1;

    //Velocidade do jogador
    [field: SerializeField] public float Speed { get; set; } = 5f;

    //Movimentos e posições
    private Vector3 posInicial;
    private float inputValue;
    private Vector2 movement;

    //Ângulo aplicado para a bola ir
    [SerializeField] private float maxBounceAngle = 75;

    private void Awake()
    {
        //Pega componentes
        playerInput = new PlayerInput();
        rb = GetComponent<Rigidbody2D>();
        tr = transform;

        //Guarda posição inicial
        posInicial = tr.position;
    }

    //Ativa os controles e dispara que o jogador foi criado
    private void OnEnable()
    {
        playerInput.Player.Enable();
        GameEvents.PlayerSpawned(this);
    }


    private void Update()
    {
        //Lê o valor do input do jogador (recebe -1, 0 ou 1);
        inputValue = playerInput.Player.Move.ReadValue<float>();
    }

    //Calcula o movimento a partir dos controles e aplica na velocidade
    private void FixedUpdate()
    {
        movement.x = inputValue;
        movement.y = 0;
        rb.velocity = movement * Speed * invertControls;
    }

    //Não utilizado (servia para voltar o jogador ir para a posição que foi iniciado)
    public void ResetPlayer()
    {
        tr.position = posInicial;
    }

    //Não utilizado
    public void InvertControls(bool invert)
    {
        invertControls = invert ? 1 : -1;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            //Pega os componentes
            Rigidbody2D ballRb = collision.rigidbody;
            Collider2D paddleCol = collision.otherCollider;

            //Guarda a velocidade da bola
            Vector2 ballDirection = ballRb.velocity.normalized;
            //Verifica onde tocou o jogador na pá
            Vector2 contactDistance = paddleCol.bounds.center - ballRb.transform.position;

            //Calculo o ângulo que a bola será impulsinada
            float bounceAngle = (contactDistance.x / paddleCol.bounds.size.x) * maxBounceAngle;
            //Rotaciona o angulo da bola para a direão calculada acima
            ballDirection = Quaternion.AngleAxis(bounceAngle, Vector3.forward) * ballDirection;
            
            //Aplica a velocidade para bola ir para na direção encontrada
            ballRb.velocity = ballRb.velocity.magnitude * ballDirection;
        }
    }
}
