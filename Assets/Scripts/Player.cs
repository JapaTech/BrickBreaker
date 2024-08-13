using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerInput playerInput;
    
    Rigidbody2D rb;
    Transform tr;
    Vector3 posInicial;

    private int invertControls = 1;

    [field: SerializeField] public float Speed { get; set; } = 5f;
    private float inputValue;
    private Vector2 movement;

    [SerializeField] private float maxBounceAngle = 75;

    private void Awake()
    {
        playerInput = new PlayerInput();
        rb = GetComponent<Rigidbody2D>();
        tr = transform;
        posInicial = tr.position;
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();
        GameEvents.PlayerSpawned(this);
    }


    private void Update()
    {
        inputValue = playerInput.Player.Move.ReadValue<float>();
        
    }

    private void FixedUpdate()
    {
        movement.x = inputValue;
        movement.y = 0;
        rb.velocity = movement * Speed * invertControls;
    }

    public void ResetPlayer()
    {
        tr.position = posInicial;
    }

    public void InvertControls(bool invert)
    {
        invertControls = invert ? 1 : -1;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody2D ballRb = collision.rigidbody;
            Collider2D paddleCol = collision.otherCollider;

            Vector2 ballDirection = ballRb.velocity.normalized;
            Vector2 contactDistance = paddleCol.bounds.center - ballRb.transform.position;

            float bounceAngle = (contactDistance.x / paddleCol.bounds.size.x) * maxBounceAngle;
            ballDirection = Quaternion.AngleAxis(bounceAngle, Vector3.forward) * ballDirection;

            ballRb.velocity = ballRb.velocity.magnitude * ballDirection;
        }
    }
}
