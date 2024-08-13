using UnityEngine;

public class BallProjectile : Projectile
{
    [SerializeField] private SpriteRenderer actualSprite;
    [SerializeField] private Sprite[] spritesDano;

    private void Start()
    {
        ChangeSprite();
        Movement();
    }

    private void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ChangeHealth(-1);
            GameManager.Instance.ChangeHealth(-damage);
        }

        if (collision.gameObject.CompareTag("Death"))
        {
            ChangeHealth(-1);
        }

        if (Mathf.Abs(rb.velocity.y) <= 0.6f && Mathf.Abs(rb.velocity.y) > 0)
        {
            Vector2 force = new Vector2(0, rb.velocity.normalized.y * 0.3f);
            rb.AddForce(force.normalized, ForceMode2D.Impulse);
        }
        else if (rb.velocity.y == 0f)
        {
            if (transform.position.y <= 0)
            {
                Vector2 force = new Vector2(1, 3f);
                rb.AddForce(force.normalized, ForceMode2D.Impulse);
            }
            else
            {
                Vector2 force = new Vector2(1, -3f);
                rb.AddForce(force.normalized, ForceMode2D.Impulse);
            }
        }
    }

    protected override void ChangeHealth(int value)
    {
        base.ChangeHealth(value);
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        if (health == 0)
            return;

        actualSprite.sprite = spritesDano[health - 1];
    }

    public override void Movement()
    {
        Vector2 force = new Vector2(Random.Range(-0.8f, 0.8f), -1f);

        rb.AddForce(force.normalized * speed, ForceMode2D.Impulse);
    }

    public override void Movement(Transform target)
    {
        throw new System.NotImplementedException();
    }
}
