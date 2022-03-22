using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{

    private List<GameObject> spawnedEnemies;
    private List<Vector3> spawnPoints;
    [Range(1, 20)] public uint numberOfEnemies;

    public GameObject enemyPrefab;
    public void Awake()
    {
        spawnedEnemies = new List<GameObject>();
        spawnPoints = new List<Vector3>();
    }
    void Start()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            spawnPoints.Add(GetRandomLocation());
        }

        for(int i=0; i<numberOfEnemies; i++)
        {
            spawnedEnemies.Add(SpawnEnemy());
        }
    }
    void Update()
    {
        
    }

    public GameObject SpawnEnemy()
    {
        int randomIndex = Random.Range(0, spawnPoints.ToArray().Length);

        GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPoints[randomIndex], new Quaternion(0f, 0f, 0f, 0f));

        return spawnedEnemy;
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
