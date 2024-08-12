using System;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private int health = 1;
    [SerializeField] private SpriteRenderer actualSprite;
    [SerializeField] private Sprite[] spritesHealth;
    [SerializeField] private float chanceDropPowerUp;

    [field: SerializeField] public int Points { get; private set; } = 100;


    private void Start()
    {
        ChangeSprite();
    }

    private void TakeDamage(int damage)
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

    private void ChangeSprite()
    {
        actualSprite.sprite = spritesHealth[health - 1];
    }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            TakeDamage(1);
        }
    }
}
