using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float speed = 50f;
    Vector3 startPos;
    Transform tr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = transform;
        startPos = tr.position;
    }

    private void OnEnable()
    {
        GameManager.Instance.OnLoseHealth += ResetBall;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnLoseHealth -= ResetBall;
        
    }

    void Start()
    {
        Invoke(nameof(AddRandomTrajectory), 2f);
    }

    private void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * speed;
        //Debug.Log(rb.velocity);
    }

    public void ResetBall()
    {
        rb.velocity = Vector2.zero;
        AddRandomTrajectory();
        tr.position = startPos;
    }

    private void AddRandomTrajectory()
    {
        Vector2 force = new Vector2(Random.Range(-0.5f, 0.5f), -1f);

        rb.AddForce(force.normalized * speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(rb.velocity.y == 0)
        {
            if (collision.transform.CompareTag("Wall"))
            {
                Vector2 force = new Vector2(0, Random.Range(Random.Range(-0.5f, -0.25f), Random.Range(0.25f, 0.5f)));

                rb.AddForce(force.normalized, ForceMode2D.Impulse);
            }
        }   
    }
}
