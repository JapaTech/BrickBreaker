using System;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private int health = 1;
    public static Action OnDeath;

    private void Death()
    {
        OnDeath?.Invoke();
        gameObject.SetActive(false);
    }

    private void TakeDamage()
    {
        health--;

        if(health <= 0)
        {
            health = 0;
            Death();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            TakeDamage();
        }
    }
}
