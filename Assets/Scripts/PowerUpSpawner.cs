using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public List<Transform> spawnPoints = new List<Transform>(); // Assign your 2 empty GameObjects here in the Inspector

    public void SpawnPowerUp()
    {
        if (spawnPoints.Count == 0 || powerUpPrefab == null)
        {
            Debug.LogWarning("PowerUpSpawner: Missing spawn points or prefab!");
            return;
        }

        // Choose one of the two points randomly
        int randomIndex = Random.Range(0, spawnPoints.Count);
        Instantiate(powerUpPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
    }
}