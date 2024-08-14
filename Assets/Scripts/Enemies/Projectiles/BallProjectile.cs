using UnityEngine;

public class BallProjectile : Projectile
{
    //Referência de componentes
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
        //Se atingir o jogador causa dano no jogador e no projétil
        if (collision.gameObject.CompareTag("Player"))
        {
            ChangeHealth(-1);
            GameManager.Instance.ChangeHealth(-damage);
        }
        //Se atingir a zona da morte causa dano no projétil
        if (collision.gameObject.CompareTag("Death"))
        {
            ChangeHealth(-1);
        }

        //Evita que o projétil viaje muito na horizontal
        if (Mathf.Abs(rb.velocity.y) <= 0.6f && Mathf.Abs(rb.velocity.y) > 0)
        {
            Vector2 force = new Vector2(0, rb.velocity.normalized.y * 0.3f);
            rb.AddForce(force.normalized, ForceMode2D.Impulse);
        }
        //Evita que o projétil viaje com o em algum dos seu componentes velocidade
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
        else if (rb.velocity.x == 0f)
        {
            Vector2 force = new Vector2(Random.Range(-3, 3), 0);
            rb.AddForce(force.normalized, ForceMode2D.Impulse);
        }
    }

    protected override void ChangeHealth(int value)
    {
        base.ChangeHealth(value);
        ChangeSprite();
    }

    //O sprite do jogador muda conforme a quantidade de vida do jogador
    private void ChangeSprite()
    {
        if (health <= 0)
            return;

        actualSprite.sprite = spritesDano[health - 1];
    }

    //Adiciona um movimento inicial ao projétil
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
