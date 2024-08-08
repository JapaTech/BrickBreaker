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

    void Start()
    {
        Invoke(nameof(AddRandomTrajectory), 1f);
    }

    private void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * speed;
        Debug.Log(rb.velocity);
    }

    public void ResetBall()
    {
        tr.position = startPos;
    }

    private void AddRandomTrajectory()
    {
        Vector2 force = new Vector2(Random.Range(-1f, 1f), -1f);

        rb.AddForce(force.normalized * speed, ForceMode2D.Impulse);
    }
}
