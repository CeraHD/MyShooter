using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{

    private List<GameObject> spawnedEnemies;
    private List<Vector3> spawnPoints;

    [Range(1, 20)] public int numberOfEnemies;
    private int numberOfSpawnedEnemies;

    [Range(1, 5)] public float spawnInterval = 2;

    public GameObject enemyPrefab;
    public void Awake()
    {
        spawnedEnemies = new List<GameObject>();
        spawnPoints = new List<Vector3>();

        numberOfEnemies = 10;
        numberOfSpawnedEnemies = 0;

        for (int i = 0; i < numberOfEnemies; i++)
        {
            spawnPoints.Add(GetRandomLocation());
        }
    }
    void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    public IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            if (numberOfSpawnedEnemies >= numberOfEnemies)
            {
                yield break;
            }

            SpawnEnemy();

            yield return new WaitForSeconds(spawnInterval);
        }
    }
    public void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, spawnPoints.Count);
        Vector3 spawnPosition = spawnPoints[randomIndex];

        GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        spawnedEnemies.Add(spawnedEnemy);
        numberOfSpawnedEnemies++;
    }

    private Vector3 GetRandomLocation()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        // Pick the first indice of a random triangle in the nav mesh
        int t = Random.Range(0, navMeshData.indices.Length - 3);

        // Select a random point on it
        Vector3 point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t + 1]], Random.value);
        Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t + 2]], Random.value);

        return point;
    }

}
