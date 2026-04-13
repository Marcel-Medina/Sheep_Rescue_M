using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    public bool canSpawn = true; 

    public GameObject sheepPrefab; 
    public List<Transform> sheepSpawnPositions = new List<Transform>();
    [Header("Spawn Timing")]
    public float initialTimeBetweenSpawns;
    public float minTimeBetweenSpawns;
    public float timeToReachMaxDifficulty; // Seconds until it hits the minimum interval

    private List<GameObject> sheepList = new List<GameObject>(); 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SpawnSheep()
    {
        Vector3 randomPosition = sheepSpawnPositions[Random.Range(0, sheepSpawnPositions.Count)].position; 
        GameObject sheep = Instantiate(sheepPrefab, randomPosition, sheepPrefab.transform.rotation); 
        sheepList.Add(sheep);
        sheep.GetComponent<Sheep>().SetSpawner(this); 
    }
    private IEnumerator SpawnRoutine() 
    {
        while (canSpawn) 
        {
            SpawnSheep();
            float progress = Mathf.Clamp01(Time.timeSinceLevelLoad / timeToReachMaxDifficulty);

            // Gradually move from initial time to minimum time
            float currentWaitTime = Mathf.Lerp(initialTimeBetweenSpawns, minTimeBetweenSpawns, progress);

            yield return new WaitForSeconds(currentWaitTime);
        }
    }
    public void RemoveSheepFromList(GameObject sheep)
    {
        sheepList.Remove(sheep);
    }
    public void DestroyAllSheep()
    {
        foreach (GameObject sheep in sheepList) 
        {
            Destroy(sheep); 
        }

        sheepList.Clear();
    }

}
