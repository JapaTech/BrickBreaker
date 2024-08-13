using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float speed = 50f;
    Vector3 startPos;
    Transform tr;

    [SerializeField] private int startDamage;
    [field: SerializeField] public int Damage { get; set; } = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = transform;
        startPos = tr.position;
    }

    private void OnEnable()
    {
        GameEvents.BallSpawned(this);
    }

    void Start()
    {
        Damage = startDamage;
        Invoke(nameof(AddRandomTrajectory), 1.5f);
    }


    private void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * speed;
        //Debug.Log(rb.velocity);
    }

    public void SetBallDefautl()
    {
        tr.position = startPos;
        rb.velocity = Vector2.zero;
        Invoke(nameof(AddRandomTrajectory), 2f);
    }

    private void AddRandomTrajectory()
    {
        Vector2 force = new Vector2(Random.Range(-0.5f, 0.5f), -1f);

        rb.AddForce(force.normalized * speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            GameManager.Instance.ChangeHealth(-1);
            SetBallDefautl();
            return;
        }

        if (Mathf.Abs(rb.velocity.y) <= 0.6f && Mathf.Abs(rb.velocity.y) > 0)
        {
            Vector2 force = new Vector2(0, rb.velocity.normalized.y * 0.3f);
            rb.AddForce(force.normalized, ForceMode2D.Impulse);
        }
        else if (rb.velocity.y == 0f)
        {
            if(transform.position.y <= 0)
            {
                Vector2 force = new Vector2(1, 3f);
                rb.AddForce(force.normalized, ForceMode2D.Impulse);
            }
            else
            {
                Vector2 force = new Vector2(1, -3f);
                rb.AddForce(force.normalized, ForceMode2D.Impulse);
            }

            Debug.Log("Chamou");
        }
        
    }
}
