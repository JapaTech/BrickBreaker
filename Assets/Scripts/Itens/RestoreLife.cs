using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreLife : MonoBehaviour
{
    [SerializeField] private int amount =1;
    [SerializeField] Vector2 fallSpeed;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = fallSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.ChangeHealth(amount);
            gameObject.SetActive(false);
        }
        if (collision.CompareTag("Death"))
        {
            gameObject.SetActive(false);
        }
    }
}
