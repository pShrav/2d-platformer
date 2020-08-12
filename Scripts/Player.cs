// Player script that deals damage to the enemies or the player, gains/loses lives and score, and dies/respawns the player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Game game;

    private void Awake()
    {
        game = FindObjectOfType<Game>();  // reference to the game object
    }

    // If the player collides with an enemy, deal damage to the player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Hurt();    
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check to see what the player collided with and either deal damage to an enemy, gain a life, or increase score
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                StartCoroutine(HurtEnemy(collision.gameObject.GetComponent<Enemy>()));
                break;

            case "Gem":
                game.AddLife();
                Destroy(collision.gameObject);
                break;

            case "Coin":
                game.AddPoints(100);
                Destroy(collision.gameObject);
                break;

            default:
                break;
        }
    }

    // Coroutine to avoid the issue of enemies dying before the frame ends
    IEnumerator HurtEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
        yield return new WaitForEndOfFrame(); 
        game.AddPoints(enemy.pointValue); // update points at the end of the frame
    }

    // Lose a life and destroy the player object
    void Hurt()
    {        
        game.LoseLife();
        Destroy(this.gameObject);
    }
}
