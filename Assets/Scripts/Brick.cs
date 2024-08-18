using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private Brick_GO brickData;   

    //Vida do bloco
    private int health = 1;
    [SerializeField] private SpriteRenderer actualSprite;
    public int Points { get; private set; }


    private void Start()
    {
        health = brickData.healthStart;
        Points = brickData.points;
        ChangeSprite();
    }

    //Função que alterar a vida do bloco, chamada quando ele recebe dano. (Pode ser chamada para receber vida)
    private void ChangeHealth(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            health = 0;
            Death();
            return;
        }
        ChangeSprite();
    }

    //A aparência do bloco está ligada a sua vida. Dependendo da quantidade de vida do bloco, o sprite renderer
    //renderiza o sprite apropriado
    private void ChangeSprite()
    {
        if (health <= 0)
            return;

        actualSprite.sprite = brickData.sprites[health - 1];
    }

    //Quando o bloco morre ele avisa ao manager que ele morreu ao manager e tem chance de dropar um powerup da sua posição
    private void Death()
    {
        float dropItem = UnityEngine.Random.value;
        if(dropItem <= brickData.dropChance)
        {
            PowerUpManager.Instance.SpawnPowerUp(transform.position) ;
        }

        GameManager.Instance.BrickDestroyed(this);
        gameObject.SetActive(false);
    }

    //Se o bloco bater em uma bola, ele toma o dano da bola.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            ChangeHealth(collision.gameObject.GetComponent<Ball>().Damage);
        }
    }
}
