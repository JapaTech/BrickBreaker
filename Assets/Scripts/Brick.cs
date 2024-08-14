using UnityEngine;

public class Brick : MonoBehaviour
{
    //Referências de componentes
    [SerializeField] private SpriteRenderer actualSprite;
    [SerializeField] private Sprite[] spritesHealth;
    
    //Vida do bloco
    [SerializeField] private int health = 1;

    //Chace que o bloco tem de de dropar um power up ao ser destruído
    [SerializeField] private float chanceDropPowerUp;

    //Propriedade dos pontos que o bloco irá adioncar a pontuação do jogador
    [field: SerializeField] public int Points { get; private set; } = 100;


    private void Start()
    {
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

        actualSprite.sprite = spritesHealth[health - 1];
    }

    //Quando o bloco morre ele avisa ao manager que ele morreu ao manager e tem chance de dropar um powerup da sua posição
    private void Death()
    {
        float dropItem = UnityEngine.Random.value;
        if(dropItem <= chanceDropPowerUp)
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
