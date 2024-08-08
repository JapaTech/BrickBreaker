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
}
