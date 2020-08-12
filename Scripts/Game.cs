// Main game functionality - keeps track of scores, lives, respawning, endgame logic, and save/load functionality

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Game : MonoBehaviour
{
    public int lives, score, highscore; 
    [SerializeField] private Player player;
    [SerializeField] private Transform playerSpawnPoint;

    [SerializeField] private Spawner[] spawners;   // reference to spawners

    private int level;
   
    [SerializeField] private TextMeshProUGUI scoreText, livesText, bestText;  // references to TextMeshPro texts

    // Initialize scores and load the first level
    private void Start()
    {
        highscore = PlayerPrefs.GetInt("Highscore", 0);
        Load();
        bestText.text = "Best: " + highscore;
        UpdateHUD();
    }

    // Check to see if player has enough lives to respawn, then either respawn player and deduct a life or end the game
    public void LoseLife()
    {
        if (lives > 0)
        {
            StopAllCoroutines();
            StartCoroutine(Respawn());
        }

        else
        {
            EndGame();
        }
    }

    // Update the highscore if necessary and start a new game
    void EndGame()
    {
        if (score > highscore) 
        {
            PlayerPrefs.SetInt("Highscore", score);
        }

        StartNewGame();
    }

    // Coroutine to create spawn delay before respawning
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        lives--;
        Instantiate(player.gameObject, playerSpawnPoint.position, Quaternion.identity);

        UpdateHUD();   // update HUd to show life count
    }

    // Increment score, update HUD, and check for level completion
    public void AddPoints(int points)
    {
        score += points;
        UpdateHUD();        
        CheckForLevelCompletion();
    }

    // Increment lives and update HUD
    public void AddLife()
    {
        lives++;
        UpdateHUD();
    }
    
    // If all enemies have finished spawning and are dead, complete the level
    private void CheckForLevelCompletion()
    {
        if (!FindObjectOfType<Enemy>())
        {
            foreach (Spawner spawner in spawners)
            {
                if (!spawner.completed)
                {
                    return;
                }
            }
            CompleteLevel();
        }
    }

    // Increase level, load the new level (unless it is the last level, in which case the game is won)
    private void CompleteLevel()
    {
        level++;
        Save();

        if (level <= SceneManager.sceneCountInBuildSettings-1)
        {
            SceneManager.LoadScene(level);
        }

        else
        {
            Debug.Log("Game won! Nice.");
            EndGame();
        }
    }

    // Save data with a key and value using PlayerPrefs
    private void Save()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Lives", lives); 
        PlayerPrefs.SetInt("Level", level); 
    }

    private void Load()
    {
        // Default values 
        score = PlayerPrefs.GetInt("Score", 0);
        lives = PlayerPrefs.GetInt("Lives", 3);    
        level = PlayerPrefs.GetInt("Level", 0);    
    }

    void StartNewGame()
    {
        level = 0;
        SceneManager.LoadScene(level);
        PlayerPrefs.DeleteKey("Score");
        PlayerPrefs.DeleteKey("Lives");
        PlayerPrefs.DeleteKey("Level");
    }

    void UpdateHUD()
    {
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
    }
}
