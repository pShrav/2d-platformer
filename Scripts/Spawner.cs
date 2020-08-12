// Spawner that spawns enemies after a delay time

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy[] enemies;
    [SerializeField] private float SpawnTimeDelay, startSpawnDelay;
    public bool completed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn()); 
    }
    
     // Wait for a delay time, then spawn each enemy in array after spawn delay time, and check to see if spawning is complete
    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(startSpawnDelay);

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemyInstance = Instantiate(enemies[i], transform.position, Quaternion.identity);
            enemyInstance.Move(transform.right);  // move enemy after spawning

            completed = i >= enemies.Length - 1; 
            
            yield return new WaitForSeconds(SpawnTimeDelay);
        }
    }
}
