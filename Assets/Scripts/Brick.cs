using System;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private int health = 1;
    [SerializeField] private int points = 100;

    private void TakeDamage()
    {
        health--;

        if(health <= 0)
        {
            health = 0;
            Death();
        }
    }

    private void Death()
    {
        GameManager.Instance.UpdateScore(points);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            TakeDamage();
        }
    }
}
