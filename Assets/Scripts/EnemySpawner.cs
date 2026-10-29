using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private int minEnemies = 10;
    [SerializeField] private int maxEnemies = 20;

    [SerializeField] private Vector2 spawnAreaMin = new Vector2(-30, -30);
    [SerializeField] private Vector2 spawnAreaMax = new Vector2(30, 30);

    private void Start()
    {
       SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        DestroyAllEnemies();
        int enemyCount = Random.Range(minEnemies, maxEnemies + 1);

        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 randomPosition = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y)
            );

            Debug.Log("min spawn area: " + spawnAreaMin.x + ", " + spawnAreaMin.y);
            Debug.Log("max spawn area: " + spawnAreaMax.x + ", " + spawnAreaMax.y);

            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);;
        }

        Debug.Log($"Enemy number: {enemyCount}");
    }

    private void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); 
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");
        foreach (GameObject weapon in weapons)
        {
            Destroy(weapon);
        }
    }
}
