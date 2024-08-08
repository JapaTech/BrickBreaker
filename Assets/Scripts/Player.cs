using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerInput playerInput;
    
    Rigidbody2D rb;
    Transform tr;

    [SerializeField] private float speed = 5f;
    private float inputValue;
    private Vector2 movement;

    [SerializeField] private float maxBounceAngle = 75;

    private void Awake()
    {
        playerInput = new PlayerInput();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();
    }

    private void Update()
    {
        inputValue = playerInput.Player.Move.ReadValue<float>();
        
    }

    private void FixedUpdate()
    {
        movement.x = inputValue;
        movement.y = 0;
        rb.velocity = movement * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody2D ballRb = collision.rigidbody;
            Collider2D paddleCol = collision.otherCollider;

            Vector2 ballDirection = ballRb.velocity.normalized;
            Vector2 contactDistance = paddleCol.bounds.center - ballRb.transform.position;

            float bounceAngle = (contactDistance.x / paddleCol.bounds.size.x) * maxBounceAngle;
            ballDirection = Quaternion.AngleAxis(bounceAngle, Vector3.forward) * ballDirection;

            ballRb.velocity = ballRb.velocity.magnitude * ballDirection;
        }
    }
}
