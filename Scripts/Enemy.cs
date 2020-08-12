// Enemy that deals damage to the player upon contact

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Points that the player scores on defeating the enemy, speed, and movement direction 
    public int pointValue;
    private Rigidbody2D body;
    [SerializeField] private float Speed;
    private Vector2 MovementDirection;
    
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(MovementDirection);
    }

    // Move enemy in a direction based on a speed
    public void Move(Vector2 direction)
    {
        MovementDirection = direction;
        body.velocity = new Vector2(MovementDirection.x * Speed, body.velocity.y);
    }

    // Reverse direction upon collision with another enemy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            MovementDirection *= -1f;
        }
    }
}
