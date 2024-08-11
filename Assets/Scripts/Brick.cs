using System;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private int health = 1;
    [SerializeField] private SpriteRenderer actualSprite;
    [SerializeField] private Sprite[] spritesHealth;

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
        }
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        actualSprite.sprite = spritesHealth[health - 1];
    }

    private void Death()
    {
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
